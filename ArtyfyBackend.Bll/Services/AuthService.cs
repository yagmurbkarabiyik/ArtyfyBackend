﻿using ArtyfyBackend.Core.Models.Common;
using ArtyfyBackend.Core.Models.Login;
using ArtyfyBackend.Core.Models.Register;
using ArtyfyBackend.Core.Models.Token;
using ArtyfyBackend.Core.Models.UserApp;
using ArtyfyBackend.Core.Responses;
using ArtyfyBackend.Core.Services;
using ArtyfyBackend.Domain.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text;
using static ArtyfyBackend.Core.Settings.EmailSettings;

namespace ArtyfyBackend.Bll.Services
{
	public class AuthService : IAuthService
	{
		private readonly IMapper _mapper;
		private readonly UserManager<UserApp> _userManager;
		private readonly ITokenService _tokenService;
		private readonly IEmailService _emailService;
		private readonly IConfiguration _configuration;

		public AuthService(IMapper mapper,
			ITokenService tokenService, UserManager<UserApp> userManager, IEmailService emailService, IConfiguration configuration)
		{
			_mapper = mapper;
			_tokenService = tokenService;
			_userManager = userManager;
			_emailService = emailService;
			_configuration = configuration;
		}

		/// <summary>
		/// This method used for register to the portal
		/// </summary>
		/// <param name="registerModel"></param>
		/// <returns></returns>
		public async Task<Response<UserAppModel>> RegisterAsync(RegisterModel registerModel)
		{
			var verificationCode = GenerateCode();

			var user = new UserApp
			{
				Email = registerModel.Email,
				FullName = registerModel.FullName,
				PhoneNumber = registerModel.PhoneNumber,
				UserName = registerModel.Email,
				VerificationCode = verificationCode
			};

			var result = await _userManager.CreateAsync(user, registerModel.Password);

			if (!result.Succeeded)
			{
				var errors = result.Errors.Select(x => x.Description).ToList();

				return Response<UserAppModel>.Fail(new ErrorModel(errors), 400);
			}

			_emailService.SendEmail(
					_configuration[Settings.SenderEmail],
					user.Email,
					"Doğrulama Kodu",
					"Doğrulama Kodunuz: " + verificationCode
				);

			return Response<UserAppModel>.Success(_mapper.Map<UserAppModel>(user), 200);
		}

		/// <summary>
		/// This method used for login to the portal
		/// </summary>
		/// <param name="loginModel"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<Response<TokenModel>> LoginAsync(LoginModel loginModel)
		{
			if (loginModel == null) throw new ArgumentNullException(nameof(loginModel));

			var user = await _userManager.FindByEmailAsync(loginModel.Email);

			if (user == null) return Response<TokenModel>.Fail("User not found", 404, true);

			if (!await _userManager.CheckPasswordAsync(user, loginModel.Password))
			{
				return Response<TokenModel>.Fail("Wrong password", 400, true);
			}

			var userRoles = await _userManager.GetRolesAsync(user);
			var token = _tokenService.CreateToken(user, userRoles);

			return Response<TokenModel>.Success(token, 200);
		}

		/// <summary>
		/// This method used for list all users
		/// </summary>
		/// <returns></returns>
		public async Task<Response<List<UserAppModel>>> GetAllUsersAsync()
		{
			var users = await _userManager.Users.ToListAsync();
			if (users.Count == 0)
			{
				return Response<List<UserAppModel>>.Fail("Users not found.", 404, true);
			}

			return Response<List<UserAppModel>>.Success(_mapper.Map<List<UserAppModel>>(users), 200);
		}

		/// <summary>
		/// This method used for get user by id
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<UserAppModel>> GetUserByIdAsync(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			if (user is null)
			{
				return Response<UserAppModel>.Fail("User is not found.", 404, true);
			}
			return Response<UserAppModel>.Success(_mapper.Map<UserAppModel>(user), 200);
		}

		/// <summary>
		/// This method used for send verification code to user's email
		/// </summary>
		/// <param name="userId"></param>
		/// <returns></returns>
		public async Task<Response<NoDataModel>> SendVerificationCode(string userId)
		{
			var user = await _userManager.FindByIdAsync(userId);
			var verificationCode = GenerateCode();

			//update user fields
			user.VerificationCode = verificationCode;

			var result = await _userManager.UpdateAsync(user);
			//update user fields

			if (result.Succeeded)
			{
				_emailService.SendEmail(
					_configuration[Settings.SenderEmail],
					user.Email,
					"Doğrulama Kodu",
					"Doğrulama Kodunuz: " + verificationCode
				);

				return Response<NoDataModel>.Success("Verification code was sended.", 200);
			}

			var errors = result.Errors.Select(e => e.Description).ToList();
			return Response<NoDataModel>.Fail(new ErrorModel(errors), 400);
		}

		/// <summary>
		/// This method used for confirm verification code that sent to user's email
		/// </summary>
		/// <param name="confirmVerificationCodeModel"></param>
		/// <returns></returns>
		public async Task<Response<NoDataModel>> ConfirmVerificationCode(ConfirmVerificationCodeModel confirmVerificationCodeModel)
		{
			var user = await _userManager.FindByIdAsync(confirmVerificationCodeModel.UserId);

			if (user is null)
			{
				return Response<NoDataModel>.Fail("User is not found!", 404, true);
			}

			if (user.VerificationCode != confirmVerificationCodeModel.VerificationCode)
			{
				return Response<NoDataModel>.Fail("Verification codes not match!", 400, true);
			}

			//Update user fields
			user.VerificationCode = null;
			user.EmailConfirmed = true;
			//Update user fields

			var result = await _userManager.UpdateAsync(user);

			if (result.Succeeded)
			{
				_emailService.SendEmail(
					_configuration[Settings.SenderEmail],
					user.Email,
					"Hesabınız Doğrulandı",
					"Hesabınız başarıyla doğrulandı!"
				);
				return Response<NoDataModel>.Success("User is successfully verificated!", 200);
			}

			var errors = result.Errors.Select(e => e.Description).ToList();
			return Response<NoDataModel>.Fail(new ErrorModel(errors), 400);
		}

		public Task<Response<NoDataModel>> ResetPasswordAsync(ResetPasswordModel resetPasswordModel)
		{
			throw new NotImplementedException();
		}

		public Task<Response<UpdatePasswordModel>> UpdatePasswordAsync(UpdatePasswordModel updatePasswordModel)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// This method used for generate random verification code 
		/// </summary>
		/// <returns></returns>
		private static string GenerateCode()
		{
			Random random = new Random();
			StringBuilder codeBuilder = new StringBuilder();

			for (int i = 0; i < 6; i++)
			{
				int randomNumber = random.Next(0, 10);
				codeBuilder.Append(randomNumber);
			}

			return codeBuilder.ToString();
		}
	}

}