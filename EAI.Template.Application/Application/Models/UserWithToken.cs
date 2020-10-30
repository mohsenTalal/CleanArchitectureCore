using System;

namespace $safeprojectname$.Application.Models
{
    public class UserWithToken
    {
        public DateTime ExpiresAt { get; set; }
        public string Token { get; set; }
    }
}