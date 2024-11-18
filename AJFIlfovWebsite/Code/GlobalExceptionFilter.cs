using AJFIlfov.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace AJFIlfovWebsite.WebApp.Code;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
public sealed class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
{
    private readonly ILogger<GlobalExceptionFilterAttribute> logger;
    private readonly IModelMetadataProvider modelMetadataProvider;


    public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger,
        IModelMetadataProvider modelMetadataProvider)
    {
        this.logger = logger;
        this.modelMetadataProvider = modelMetadataProvider;
    }

    public override void OnException(ExceptionContext context)
    {
        context.ExceptionHandled = true;
        switch (context.Exception)
        {
            case NotFoundErrorException notFound:
                context.Result = new ViewResult
                {
                    ViewName = "Views/Shared/Error_NotFound.cshtml"
                };

                break;
            case UnauthorizedAccessException unauthorizedAccess:
                context.Result = new ViewResult
                {
                    ViewName = "Views/Shared/Error_Unauthorized.cshtml"
                };
                break;
            case ValidationErrorException validationError:
                foreach (var validationResult in validationError.ValidationResult.Errors)
                    context.ModelState.AddModelError(validationResult.PropertyName, validationResult.ErrorMessage);

                var descriptor = context.ActionDescriptor as ControllerActionDescriptor;
                var hasRequestedWithHeader =
                    context.HttpContext.Request.Headers.TryGetValue("X-Requested-With",
                        out var requestedWithHeaderValue);
                if ((hasRequestedWithHeader && requestedWithHeaderValue == "XMLHttpRequest") ||
                    descriptor.ControllerName == "News")
                    //context.Result = new StatusCodeResult(StatusCodes.Status412PreconditionFailed);
                    context.Result =
                        new ObjectResult(
                            validationError.ValidationResult.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }))
                        {
                            StatusCode = StatusCodes.Status412PreconditionFailed
                        };
                else
                    context.Result = new ViewResult
                    {
                        ViewName = $"Views/{descriptor.ControllerName}/{descriptor.ActionName}.cshtml",
                        ViewData = new ViewDataDictionary(modelMetadataProvider, context.ModelState)
                        {
                            Model = validationError.Model
                        }
                    };
                break;
            default:
                context.Result = new ViewResult
                {
                    ViewName = "Views/Shared/Error_InternalServerError.cshtml"
                };
                break;
        }
    }
}