using System;
using System.Net;

namespace Application.Exceptions
{
    public class RestException : Exception
    {
        public RestException(object errors, HttpStatusCode code = HttpStatusCode.OK)
        {
            Code = code;
            Errors = errors;
        }

        public HttpStatusCode Code { get; }

        public object Errors { get; set; }
    }
}
