using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Domain.Exceptions.Users
{
    public class UserNotFoundException : NotFoundException
    {
        public UserNotFoundException()
        {
            this.TitleMessage = "User not found";
        }
    }
}
