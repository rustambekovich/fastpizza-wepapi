using FastPizza.Domain.Enums;

namespace FastPizza.Domain.Entities.Users
{
    public class User : Auditable
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string MiddleName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string PassposrSeriaNumber { get; set; } = string.Empty;
        public bool IsMale { get; set; }
        public DateTime BithdayDate { get; set; }
        public string WasBorn { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Salt { get; set; } = string.Empty;
        public string ImagePath { get; set; } = string.Empty;
        public UserRole Role { get; set; }
    }
}
