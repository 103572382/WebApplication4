using System.ComponentModel.DataAnnotations;

public class Account
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string PasswordHash { get; set; }

    [Required]
    public Guid UserId { get; set; }

    public bool IsVerified { get; set; } = false;

    public string Otp { get; set; }

    public DateTime? OtpExpiry { get; set; }
    public DateTime DateOfBirth { get; internal set; }
    public int Age { get; internal set; }
}
