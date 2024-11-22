using Microsoft.AspNetCore.Mvc;
using WebApplication4.Dtos;
using WebApplication4.Services;

namespace WebApplication4.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AuthController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // Đăng ký
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            await _accountService.RegisterAsync(dto);
            return Ok(new { message = "Registration successful. Please verify your email." });
        }

        // Đăng nhập
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var token = await _accountService.LoginAsync(dto);
            return Ok(new { Token = token });
        }

        // Quên mật khẩu
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] string email)
        {
            await _accountService.SendOtpAsync(email);
            return Ok(new { message = "OTP has been sent to your email." });
        }

        // Xác minh OTP
        [HttpPost("verify-otp")]
        public async Task<IActionResult> VerifyOtp([FromBody] string email, string otp)
        {
            await _accountService.VerifyOtpAsync(email, otp);
            return Ok(new { message = "OTP verified successfully." });
        }

        // Đổi mật khẩu
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            await _accountService.ResetPasswordAsync(dto);
            return Ok(new { message = "Password reset successfully." });
        }
    }
}