using WebApplication4.Dtos;

namespace WebApplication4.Services
{
    public interface IAccountService
    {
        Task RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
        Task SendOtpAsync(string email);
        Task VerifyOtpAsync(string email, string otp);
        Task ResetPasswordAsync(ResetPasswordDto dto);
    }

}
