using $ext_safeprojectname$.Application.Application.Models;
using $ext_safeprojectname$.Application.Application.Services;
using $ext_safeprojectname$.Application.Dtos;
using $safeprojectname$.Models;
using Microsoft.AspNetCore.Mvc;
using LoginModel = $safeprojectname$.Applications.LoginModel;
using Swashbuckle.AspNetCore.Filters;

namespace $safeprojectname$.Account
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private IApplicationService _applicationService;

        public AccountController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }


        /// <summary>
        /// Login
        /// </summary>
        /// <param name="loginModel"></param>
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Permission</response>  
        /// <response code="500">Internal Error</response>
        [HttpPost]
        [ProducesResponseType(typeof(UserWithToken), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 401)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [SwaggerResponseExample(400, typeof(BadRequestGetExample))]
        [SwaggerResponseExample(401, typeof(PermissionGetExample))]
        [SwaggerResponseExample(500, typeof(InternalErrorGetExample))]

        public ActionResult<UserWithToken> Login([FromBody] LoginModel loginModel)
        {
            return _applicationService.Login(loginModel.UserName, loginModel.Password);
        }
    }
}