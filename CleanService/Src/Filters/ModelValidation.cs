using System.Net;
using CleanService.Src.Constant;
using CleanService.Src.Modules.Auth.Mapping.DTOs;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CleanService.Src.Filters;

public class ModelValidation : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var modelState = context.ModelState;
        modelState.Clear();
        var models = context.ActionArguments.Values.ToList();

        // var model = context.ActionArguments["updateInfoDto"] as UpdateInfoDto;
        var controller = context.Controller as ControllerBase;
        var isValid = controller != null &&
                      models.TrueForAll(model => model != null && controller.TryValidateModel(model));
        if (!isValid)
        {
            var errors = modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToArray();

            throw new ExceptionResponse(HttpStatusCode.BadRequest, "Validation failed",
                ExceptionConvention.ValidationFailed, errors);
        }
    }
}