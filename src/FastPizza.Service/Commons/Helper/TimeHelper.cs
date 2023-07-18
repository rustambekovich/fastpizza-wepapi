using FastPizza.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Service.Commons.Helper
{
    public class TimeHelper
    {
        public static DateTime GetDateTime()
        {
            var time = DateTime.UtcNow;
            //time = time.AddHours(TimeConstan.UTC);
            return time;
        }
    }
}
