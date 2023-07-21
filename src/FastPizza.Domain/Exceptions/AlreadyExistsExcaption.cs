using System.Net;

namespace FastPizza.Domain.Exceptions;

public class AlreadyExistsExcaption : Exception
{
    public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
    public string TitleMessage { get; protected set; } = String.Empty;
}
