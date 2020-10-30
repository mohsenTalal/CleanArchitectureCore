namespace $safeprojectname$.Models
{
    public class EAICustomError
    {
        public string id { get; set; }
        public int HttpStatus { get; set; }

        public string Code { get; set; }
        public string ErrorType { get; set; }
        public string Message { get; set; }
        public string BusinessMessageAr { get; set; }
        public string BusinessMessageEn { get; set; }
    }
}