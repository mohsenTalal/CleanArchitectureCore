using System.Collections.Generic;
using System.Threading.Tasks;
using $safeprojectname$.Application.Models;

namespace $safeprojectname$.Application.Services
{
    public interface IApplicationService
    {
        Task<List<ApplicationsDTO>> GetAll();

        UserWithToken Login(string userName, string Password);

        bool IsAuthorized(string userName, string Password);

        Client Authenticate(string userName, string Password);

        Task<Client> AuthenticateAsync(string userName, string Password);
    }
}