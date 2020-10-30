using AutoMapper;
using $safeprojectname$.Application.Models;
using $safeprojectname$.Infrastructure;
using $safeprojectname$.Logging;
using $ext_safeprojectname$.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace $safeprojectname$.Application.Services
{
    public class ApplicationService : IApplicationService
    {
        private IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private ITokenBuilder _tokenBuilder;
        private readonly ICachingHandler _cachingHandler;

        private readonly ILoggerBuilder loggingBuilder;

        public ApplicationService(IUnitOfWork unitOfWork, IMapper mapper, ITokenBuilder tokenBuilder, ICachingHandler cachingHandler, ILoggerBuilder loggingBuilder)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _tokenBuilder = tokenBuilder;
            _cachingHandler = cachingHandler;

            this.loggingBuilder = loggingBuilder;
        }

        public async Task<List<ApplicationsDTO>> GetAll()
        {
            loggingBuilder.AddApplicationId(1);
            string key = "GetAllApplications";
            return await _cachingHandler.GetOrAddWithMappingAsync<List<Applications>, List<ApplicationsDTO>>(key, () => _unitOfWork.GetRepository<Applications>().GetAll().ToList(), DateTime.Now.AddMinutes(30));

        }

        public UserWithToken Login(string userName, string Password)
        {
            var user = _unitOfWork.GetRepository<Applications>().FirstOrDefault(x => x.UserName == userName, x => x.Scopes);

            if (user == null)
            {
                throw new UnauthorizedAccessException("username/password aren't right");
            }

            var expiresIn = DateTime.Now.AddMinutes(30);
            var token = _tokenBuilder.Build(user.Name, user.Scopes.Select(x => x.ScopeName).ToArray(), expiresIn);

            return new UserWithToken
            {
                ExpiresAt = expiresIn,
                Token = token
            };
        }

        public bool IsAuthorized(string userName, string Password)
        {
            var user = _unitOfWork.GetRepository<Applications>().FirstOrDefault(x => x.UserName == userName);

            if (user == null)
            {
                throw new UnauthorizedAccessException("username/password aren't right");
            }

            return true;
        }

        public Client Authenticate(string username, string password)
        {
            // To replace strings values by real attributes
            var users = _unitOfWork.GetRepository<Clients>().GetAllIncluding(c => c.ClientScopes);

            var userDto = from u in users
                          where "u.Username" == username && "u.Password" == password
                          select new Client
                          {
                              Id = u.Id,
                              Name = "u.Username",
                              Methods = (from m in u.ClientScopes
                                         select new Scope { Id = m.Id, Name = "m.Name" }).Distinct().ToList()
                          };

            return userDto.FirstOrDefault();
        }

        public async Task<Client> AuthenticateAsync(string username, string password)
        {
            // To replace strings values by real attributes
            return await Task.Run(() =>
            {
                // GetAllIncluding method should be Async instate of "await Task.Run()"
                var users = _unitOfWork.GetRepository<Clients>().GetAllIncluding(c => c.ClientScopes);

                var userDto = from u in users
                              where "u.Username" == username && "u.Password" == password
                              select new Client
                              {
                                  Id = u.Id,
                                  Name = "u.Username",
                                  Methods = (from m in u.ClientScopes
                                             select new Scope { Id = m.Id, Name = "m.Name" }).Distinct().ToList()
                              };

                return userDto.FirstOrDefault();
            });
        }
    }
}