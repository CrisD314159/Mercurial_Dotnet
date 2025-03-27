using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class NotVerifiedExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is NotVerifiedException notVerifiedException )
    {
      context.Result = new ObjectResult(notVerifiedException.Value)
      {
        StatusCode = notVerifiedException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}