using $safeprojectname$.Exceptions;
using $safeprojectname$.Models;
using $ext_safeprojectname$.Application.Application.Models;
using $ext_safeprojectname$.Application.Application.Services;
using $ext_safeprojectname$.Application.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace $safeprojectname$.Applications
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Project")]
    public class ApplicationsController : ControllerBase
    {
        private IApplicationService _applicationService;
        private readonly ILoggerBuilder loggingBuilder;

        public ApplicationsController(IApplicationService applicationService, ILoggerBuilder loggingBuilder)
        {
            _applicationService = applicationService;
            this.loggingBuilder = loggingBuilder;
        }

        /// <summary>
        ///Get
        /// </summary>

        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Permission</response>  
        /// <response code="500">Internal Error</response>

        [HttpGet]
        [ProducesResponseType(typeof(ApplicationsDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 401)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [SwaggerResponseExample(500, typeof(InternalErrorGetExample))]
        [SwaggerResponseExample(401, typeof(PermissionGetExample))]
        [SwaggerResponseExample(400, typeof(BadRequestGetExample))]
        [ResponseCache(Duration = 120)]
        public async Task<ActionResult<IEnumerable<ApplicationsDTO>>> Get()
        {
            return await _applicationService.GetAll();
        }


        /// <summary>
        ///GetById
        /// </summary>
        /// <param name="id">id </param> 
        /// <response code="200">Success</response>
        /// <response code="400">Bad Request</response> 
        /// <response code="401">Permission</response>  
        [HttpGet("{id}")]
      /*  [ProducesResponseType(typeof(ApplicationsDTO), 200)]
        [ProducesResponseType(typeof(ErrorResponse), 401)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        [ProducesResponseType(typeof(ErrorResponse), 500)]
        [SwaggerResponseExample(500, typeof(InternalErrorGetExample))]
        [SwaggerResponseExample(401, typeof(PermissionGetExample))]
        [SwaggerResponseExample(400, typeof(BadRequestGetExample))]*/

        public ApplicationsDTO GetById(int id)
        {
            throw new APIException("1001", "API Exception", HttpStatusCode.BadRequest);
        }
    }
}