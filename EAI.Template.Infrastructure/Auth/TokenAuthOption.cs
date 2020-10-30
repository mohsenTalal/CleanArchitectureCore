using System;
using Microsoft.IdentityModel.Tokens;

namespace $safeprojectname$.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "EAI";
        public static string Issuer { get; } = "EAI";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(30);
        public static string TokenType { get; } = "Bearer";
    }
}