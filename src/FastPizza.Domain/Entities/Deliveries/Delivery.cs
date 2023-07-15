using FastPizza.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Domain.Entities.Deliveries
{
    public class Delivery : Auditable
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
    }
}
