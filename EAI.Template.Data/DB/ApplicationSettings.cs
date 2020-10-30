using System;

namespace $safeprojectname$
{
    public partial class ApplicationSettings
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreationDate { get; set; }
    }
}