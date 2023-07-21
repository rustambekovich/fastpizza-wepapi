﻿using System.Net;

namespace FastPizza.Domain.Exceptions
{
    public class NotFoundException : Exception
    {
        public HttpStatusCode StatusCode { get; } = HttpStatusCode.NotFound;
        public string TitleMessage { get; protected set; } = String.Empty;
    }
}
