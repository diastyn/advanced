using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace Advanced.Attributes;

public class ErrorNotificationFilter : ActionFilterAttribute
{
    private readonly ITempDataDictionary _tempDataDictionary;

    public ErrorNotificationFilter(ITempDataDictionary tempDataDictionary)
    {
        _tempDataDictionary = tempDataDictionary;
    }

    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var executedContext = await next();
        if (executedContext.Exception != null)
        {
            _tempDataDictionary["ExceptionMessage"] = executedContext.Exception.Message;
        }
    }
}