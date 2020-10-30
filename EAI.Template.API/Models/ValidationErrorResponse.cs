using System.Collections.Generic;

namespace $safeprojectname$.Models
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public List<ValidationError> ValidationErrors { get; set; }
    }
}