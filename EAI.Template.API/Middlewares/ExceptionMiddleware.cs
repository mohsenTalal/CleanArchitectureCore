using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;
using $safeprojectname$.Exceptions;
using $safeprojectname$.Models;

namespace $safeprojectname$.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
                //  httpContext.Response.Headers.Clear();
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            string code, message;
            HttpStatusCode statusCode;

            switch (exception)
            {
                case UnauthorizedAccessException ex:
                    code = "401";
                    message = ex.Message;
                    statusCode = HttpStatusCode.Unauthorized;
                    break;

                case APIException ex:
                    code = ex.Code;
                    message = ex.Message;
                    statusCode = ex.StatusCode;
                    break;

                default:
                    code = "500";
                    message = exception.Message;
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            var result = JsonConvert.SerializeObject(new ErrorResponse()
            {
                Code = code,
                Message = message,
                ReferenceId = Guid.NewGuid()
            });



            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(result);
        }
    }
}