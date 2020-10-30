using $ext_safeprojectname$.Application.Dtos;
using $ext_safeprojectname$.Application.Logging;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.Features;

namespace $safeprojectname$.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        // private readonly ILoggingBuilder loggingBuilder;

        public LoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILoggerBuilder loggingBuilder)
        {
            try
            {
                var request = await FormatRequest(context.Request);

                loggingBuilder.AddRequest(request);
                loggingBuilder.SetProperty(nameof(LoggingModel.RequestUrl),
                    $"{context.Request.Scheme}://{context.Request.Host}{context.Request.Path}{context.Request.QueryString}");
                loggingBuilder.SetProperty(nameof(LoggingModel.RequestMethod), context.Request.Method);
                loggingBuilder.SetProperty(nameof(LoggingModel.RequestHeaders), JsonConvert.SerializeObject(context.Request.Headers));

                var originalBodyStream = context.Response.Body;

                using (var responseBody = new MemoryStream())
                {
                    context.Response.Body = responseBody;

                    await _next(context);

                    var response = await FormatResponse(context.Response);
                    await responseBody.CopyToAsync(originalBodyStream);
                    loggingBuilder.AddResponse(response);
                    loggingBuilder.SetProperty(nameof(LoggingModel.HttpStatus), context.Response.StatusCode);
                    loggingBuilder.SetProperty(nameof(LoggingModel.HostIp), GetHostIp());
                    loggingBuilder.SetProperty(nameof(LoggingModel.ClientIp), context.Features.Get<IHttpConnectionFeature>()?.RemoteIpAddress?.ToString());
                    var loggingModel = loggingBuilder.GetLogging();

                    //TODO:Save API request and response in DB or File
                }
            }
            catch (Exception ex)
            {
                //TODO: file log in case of logging failure or invalidate the request based on your business
            }
        }

        private async Task<string> FormatRequest(HttpRequest request)
        {
            request.EnableBuffering();
            int bufferSize = 1024;

            if (request.ContentLength != null)
            {
                bufferSize = (int)request.ContentLength.Value;
            }

            using (var reader = new StreamReader(request.Body, Encoding.UTF8, true, bufferSize, true))
            {
                var bodyAsText = reader.ReadToEnd();
                request.Body.Position = 0;
                return bodyAsText;
            }
        }

        private async Task<string> FormatResponse(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);

            string text = await new StreamReader(response.Body).ReadToEndAsync();

            response.Body.Seek(0, SeekOrigin.Begin);

            return $"{response.StatusCode}: {text}";
        }

        private static string GetHostIp()
        {
            try

            {
                var host = Dns.GetHostEntry(Dns.GetHostName());

                foreach (var ip in host.AddressList)
                {
                    if (ip.AddressFamily == AddressFamily.InterNetwork)
                    {
                        return ip.ToString();
                    }
                }
                return "";
            }
            catch
            {
                return "";
            }
        }
    }
}