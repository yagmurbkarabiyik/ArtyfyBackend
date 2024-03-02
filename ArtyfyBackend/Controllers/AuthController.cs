using ArtyfyBackend.Core.Models.Login;
using ArtyfyBackend.Core.Models.Register;
using ArtyfyBackend.Core.Services;
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
    }
}