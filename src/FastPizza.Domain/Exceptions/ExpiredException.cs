using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FastPizza.Domain.Exceptions
{
    public class ExpiredException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.Gone;

        public string TitleMessage { get; protected set; } = String.Empty;
    }
}
