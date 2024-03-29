﻿using ArtyfyBackend.Core.Models.Login;
using ArtyfyBackend.Core.Models.Register;
using ArtyfyBackend.Core.Models.UserApp;
using ArtyfyBackend.Core.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace ArtyfyBackend.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : BaseController
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpGet("getall")]
		public async Task<IActionResult> GetAll()
		{
			return CreateActionResult(await _authService.GetAllUsersAsync());

		}

		[HttpGet("getByUserId")]
		public async Task<IActionResult> GetByUserId(string userId)
		{
			return CreateActionResult(await _authService.GetUserByIdAsync(userId));
		}

		[HttpPost("register")]
		public async Task<IActionResult> Register(RegisterModel registerModel)
		{
			return CreateActionResult(await _authService.RegisterAsync(registerModel));
		}

		[HttpPost("login")]
		public async Task<IActionResult> Login(LoginModel loginModel)
		{
			return CreateActionResult(await _authService.LoginAsync(loginModel));
		}

		[HttpPost("sendVerificationCode/{userId}")]
		public async Task<IActionResult> SendVerificationCode(string userId)
		{
			return CreateActionResult(await _authService.SendVerificationCode(userId));
		}

		[HttpPost("confirmVerificationCode")]
		public async Task<IActionResult> ConfirmVerificationCode(ConfirmVerificationCodeModel model)
		{
			return CreateActionResult(await _authService.ConfirmVerificationCode(model));
		}

		[HttpPost("resetPassword")]
		public async Task<IActionResult> ResetPassword(ResetPasswordModel model)
		{
			return CreateActionResult(await _authService.ResetPasswordAsync(model));
		}

		[HttpPost("updatePassword")]
		public async Task<IActionResult> UpdatePassword(UpdatePasswordModel updatePasswordModel)
		{
			return CreateActionResult(await _authService.UpdatePasswordAsync(updatePasswordModel));
		}

		[HttpPost("updateUserProfile")]
		public async Task<IActionResult> UpdateUser(UserAppUpdateModel userAppUpdateModel)
		{
			return CreateActionResult(await _authService.UpdateUserAsync(userAppUpdateModel));
		}
	}
}