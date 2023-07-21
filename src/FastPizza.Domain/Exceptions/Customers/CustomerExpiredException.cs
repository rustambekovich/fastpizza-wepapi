using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Domain.Exceptions.Customers
{
    public class CustomerExpiredException : ExpiredException
    {
        public CustomerExpiredException()
        {
            TitleMessage = "Customer data has expired!";
        }
    }
}
