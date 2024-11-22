using System.ComponentModel.DataAnnotations;

public class User
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Range(0, 120)]
    public int Age { get; set; }

    public string Address { get; set; }

    [Required]
    public DateTime DateOfBirth { get; set; }

    public string Ethnicity { get; set; }
}
