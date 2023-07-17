using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace CarWorkshop.Application.Filters;

public class ViewValidationAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (context.ModelState.IsValid) return;

        var command = context.ActionArguments["command"];
        var actionName = context.ActionDescriptor.RouteValues["action"];

        var modelMetadata = new EmptyModelMetadataProvider();
        
        context.Result = new ViewResult
        {
            ViewName = actionName,
            ViewData = new ViewDataDictionary(modelMetadata, context.ModelState)
            {
                Model = command
            }
        };
    }
}
