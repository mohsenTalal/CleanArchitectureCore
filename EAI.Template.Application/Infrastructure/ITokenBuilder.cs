using System;

namespace $safeprojectname$.Infrastructure
{
    public interface ITokenBuilder
    {
        string Build(string name, string[] roles, DateTime expireDate);
    }
}