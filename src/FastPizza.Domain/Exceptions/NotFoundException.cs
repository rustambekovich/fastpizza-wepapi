using System.Net;

namespace FastPizza.Domain.Exceptions
{
    public class NotFoundException
    {
        public HttpStatusCode StatusCode { get;} = HttpStatusCode.NotFound;
        public string TitleMessage { get; protected set; } = String.Empty;
    }
}
