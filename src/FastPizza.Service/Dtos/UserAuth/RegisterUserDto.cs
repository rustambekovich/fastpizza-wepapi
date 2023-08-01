using FastPizza.Domain.Enums;

namespace FastPizza.Service.Dtos.UserAuth
{
    public class RegisterUserDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PassportSeriaNumber { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public DateTime BithdayDate { get; set; }
        public string WasBorn { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        // public string ImagePath { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
