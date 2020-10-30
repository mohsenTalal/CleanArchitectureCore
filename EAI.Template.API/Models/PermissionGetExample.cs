using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace $safeprojectname$.Models
{

    // : IExamplesProvider
    public class PermissionGetExample
    {

        private readonly IConfiguration configuration;
        public PermissionGetExample(IConfiguration config)
        {
            configuration = config;
        }
        public object GetExamples()
        {


            var errors = configuration.GetSection("Errors").Get<List<EAICustomError>>();
            List<EAICustomError> ErrorList = errors.FindAll(x => x.HttpStatus == 401);

            List<ErrorResponse> ErrorResponseList = new List<ErrorResponse>();

            foreach (EAICustomError er in ErrorList)
            {
                ErrorResponse errorResponse = new ErrorResponse()
                {
                    Message = er.BusinessMessageAr,
                    Code = er.Code


                };

                ErrorResponseList.Add(errorResponse);
            }

            return ErrorResponseList;



        }
    }
}
