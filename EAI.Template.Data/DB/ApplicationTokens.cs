using System;

namespace $safeprojectname$
{
    public partial class ApplicationTokens
    {
        public int Id { get; set; }
        public string ClientId { get; set; }
        public Guid SessionId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime CreationDate { get; set; }
        public string DeviceName { get; set; }
        public int? OstypeId { get; set; }
        public int ApplicationId { get; set; }
    }
}