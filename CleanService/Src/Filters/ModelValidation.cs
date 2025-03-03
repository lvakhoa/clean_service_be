using System.Net;

using CleanService.Src.Common;
using CleanService.Src.Constant;
using CleanService.Src.Exceptions;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Utils;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

namespace CleanService.Src.Filters;

public class ModelValidation : Attribute, IAsyncResultFilter
{
    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(modelState => modelState.Errors)
                .Select(modelError => modelError.ErrorMessage);

            context.Result = new BadRequestObjectResult(new ApiErrorResult(StatusCodes.Status400BadRequest,
                "Validation failed", ExceptionConvention.ValidationFailed, errors));
        }

        await next();
    }
}
