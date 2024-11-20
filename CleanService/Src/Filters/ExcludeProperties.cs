using System.Reflection;
using System.Security.Claims;
using CleanService.Src.Attributes;
using CleanService.Src.Models;
using CleanService.Src.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Pagination.EntityFrameworkCore.Extensions;

namespace CleanService.Src.Filters;

public class ExcludeProperties : ActionFilterAttribute
{
    private void SetNullForUnauthorizedProperties(object obj, UserType currentUserRole)
    {
        if (obj == null) return;

        var type = obj.GetType();
        var properties = type.GetProperties()
            .Where(x => x.GetCustomAttributes<RoleExposeAttribute>().Any())
            .ToArray();

        foreach (var property in properties)
        {
            var attributes = property.GetCustomAttributes<RoleExposeAttribute>();
            var roles = attributes.ElementAt(0).Roles;
            if (!roles.Contains(currentUserRole))
            {
                property.SetValue(obj, null);
            }
            else
            {
                var propertyValue = property.GetValue(obj);
                if (propertyValue != null && property.PropertyType.IsClass && property.PropertyType != typeof(string))
                {
                    SetNullForUnauthorizedProperties(propertyValue, currentUserRole);
                }
            }
        }
    }

    public override void OnActionExecuted(ActionExecutedContext context)
    {
        var roleClaim = context.HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
        if (roleClaim == null)
            return;
        var currentUserRole = Enum.Parse<UserType>(roleClaim);

        var result = context.Result;
        if (result is ObjectResult json)
        {
            if (json.Value is SuccessResponse body)
            {
                var type = body.Data?.GetType();
                var isGenericType = type?.IsGenericType;
                if (isGenericType == true)
                {
                    var paginationType = type?.GetGenericTypeDefinition();
                    if (paginationType == typeof(Pagination<>))
                    {
                        var resultsProperty = type?.GetProperty("Results");
                        if (resultsProperty != null)
                        {
                            type = resultsProperty.PropertyType.GetGenericArguments()[0];
                        }

                        var resultsValue = resultsProperty?.GetValue(body.Data) as Array;
                        if (resultsValue == null)
                            return;

                        foreach (var item in resultsValue)
                        {
                            SetNullForUnauthorizedProperties(item, currentUserRole);
                        }
                    }
                }
                else
                {
                    SetNullForUnauthorizedProperties(body.Data, currentUserRole);
                }
            }
        }
    }
}