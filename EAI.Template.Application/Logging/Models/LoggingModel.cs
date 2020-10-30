using System;

namespace $safeprojectname$.Dtos
{
    public class LoggingModel
    {
        public int? ApplicationId { get; set; }
        public DateTime? CreationDate { get; set; }
        public string HostIp { get; set; }
        public string ClientIp { get; set; }
        public int? HttpStatus { get; set; }
        public string MethodName { get; set; }
        public string ProviderResponse { get; set; }
        public string ReferenceNumber { get; set; }
        public string Request { get; set; }
        public string RequestHeaders { get; set; }
        public string RequestMethod { get; set; }
        public string RequestUrl { get; set; }
        public string Response { get; set; }
        public float? ResponseTime { get; set; }
    }
}