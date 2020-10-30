using System;

namespace $safeprojectname$
{
    public partial class Logs
    {
        public int Id { get; set; }
        public int? ApplicationId { get; set; }
        public DateTime? CreationDate { get; set; }
        public int? HttpStatus { get; set; }
        public string Token { get; set; }
        public string MethodName { get; set; }
        public string ErrorDescription { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public double? ResponseTime { get; set; }
        public string Ostype { get; set; }
        public string ApplicationVersion { get; set; }
        public string RequestHeaders { get; set; }
        public string ReferenceNumber { get; set; }
        public string DeviceInfo { get; set; }
        public string DeviceIp { get; set; }
        public string RequestUrl { get; set; }
        public string RequestMethod { get; set; }
        public string ProviderResponse { get; set; }
        public string HostIp { get; set; }
    }
}