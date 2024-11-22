using Umbraco.Core.Models.Membership;
using Umbraco.Core.Persistence.Repositories;

namespace WebApplication4.Dtos
{
    public class RegisterDto
    {
        public string ?Email { get; set; }
        public string ?Password { get; set; }
        public string ?FirstName { get; set; }
        public string ?LastName { get; set; }
        public int Age { get; set; }
        public string ?Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ?Ethnicity { get; set; }
    }
    
    }
