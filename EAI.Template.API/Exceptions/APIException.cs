using System;
using System.Net;

namespace $safeprojectname$.Exceptions
{
    public class APIException : Exception
    {
        public string Code { get; }
        public HttpStatusCode StatusCode { get; }

        public APIException(string code, string message, HttpStatusCode statusCode) : base(message)
        {
            Code = code;
            StatusCode = statusCode;
        }
    }
}