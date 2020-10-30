using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using $safeprojectname$.Models;

namespace $safeprojectname$.Middlewares
{
    public class CustomValidationResult : IActionResult
    {
        public Task ExecuteResultAsync(ActionContext context)
        {
            var modelStateEntries = context.ModelState.Where(e => e.Value.Errors.Count > 0).ToArray();
            var errors = new List<ValidationError>();

            if (modelStateEntries.Any())
            {
                foreach (var modelStateEntry in modelStateEntries)
                {
                    foreach (var modelStateError in modelStateEntry.Value.Errors)
                    {
                        var error = new ValidationError
                        {
                            Name = modelStateEntry.Key,
                            Description = modelStateError.ErrorMessage
                        };

                        errors.Add(error);
                    }
                }
            }

            var result = new ValidationErrorResponse
            {
                Code = "400",
                Message = "Validation Error",
                ReferenceId = Guid.NewGuid(),
                ValidationErrors = errors
            };

            //TODO: Log Error Message

            context.HttpContext.Response.ContentType = "application/json";
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.WriteAsync(JsonConvert.SerializeObject(result));
            return Task.CompletedTask;
        }
    }
}