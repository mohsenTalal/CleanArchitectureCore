using System;

namespace $safeprojectname$.Models
{
    public class ErrorResponse
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public Guid ReferenceId { get; set; }
    }
}