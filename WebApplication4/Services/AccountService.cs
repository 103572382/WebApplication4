using BCrypt.Net;
using Org.BouncyCastle.Crypto.Generators;
using WebApplication4.Dtos;
using WebApplication4.Repositories;
using WebApplication4.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IEmailService _emailService;

    public AccountService(IAccountRepository accountRepository, IEmailService emailService)
    {
        _accountRepository = accountRepository;
        _emailService = emailService;
    }

    public async Task RegisterAsync(RegisterDto dto)
    {
        var existingAccount = await _accountRepository.GetByEmailAsync(dto.Email);
        if (existingAccount != null)
            throw new Exception("Email is already registered.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
        var account = new Account
        {
            Email = dto.Email,
            PasswordHash = hashedPassword,
            Otp = GenerateOtp(),
            OtpExpiry = DateTime.UtcNow.AddMinutes(15)
        };

        await _accountRepository.CreateAsync(account);
        await _emailService.SendOtpAsync(dto.Email, account.Otp);
    }

    public async Task<string> LoginAsync(LoginDto dto)
    {
        var account = await _accountRepository.GetByEmailAsync(dto.Email);
        if (account == null || !BCrypt.Net.BCrypt.Verify(dto.Password, account.PasswordHash))
            throw new UnauthorizedAccessException("Invalid email or password.");

        if (!account.IsVerified)
            throw new UnauthorizedAccessException("Account is not verified.");

        return GenerateJwtToken(account);
    }

    public async Task SendOtpAsync(string email)
    {
        var account = await _accountRepository.GetByEmailAsync(email);
        if (account == null)
            throw new Exception("Email not found.");

        account.Otp = GenerateOtp();
        account.OtpExpiry = DateTime.UtcNow.AddMinutes(15);

        await _accountRepository.UpdateAsync(account);
        await _emailService.SendOtpAsync(email, account.Otp);
    }

    public async Task VerifyOtpAsync(string email, string otp)
    {
        var account = await _accountRepository.GetByEmailAsync(email);
        if (account == null || account.Otp != otp || account.OtpExpiry < DateTime.UtcNow)
            throw new Exception("Invalid or expired OTP.");

        account.IsVerified = true;
        account.Otp = null;
        account.OtpExpiry = null;

        await _accountRepository.UpdateAsync(account);
    }

    public async Task ResetPasswordAsync(ResetPasswordDto dto)
    {
        var account = await _accountRepository.GetByEmailAsync(dto.Email);
        if (account == null)
            throw new Exception("Email not found.");

        if (!account.IsVerified)
            throw new UnauthorizedAccessException("Account is not verified.");

        var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);
        account.PasswordHash = hashedPassword;

        await _accountRepository.UpdateAsync(account);
    }

    private string GenerateJwtToken(Account account)
    {
        // Logic tạo JWT token
        return "JWT-TOKEN";
    }

    private string GenerateOtp()
    {
        return new Random().Next(100000, 999999).ToString();
    }
}
