namespace WebApplication4.Services
{
    public interface IEmailService
    {
        Task SendOtpAsync(string email, string otp);
    }

    public class EmailService : IEmailService
    {
        public async Task SendOtpAsync(string email, string otp)
        {
            // Logic gửi email (sử dụng SMTP hoặc dịch vụ thứ ba như SendGrid)
            await Task.CompletedTask;
        }
    }

}
