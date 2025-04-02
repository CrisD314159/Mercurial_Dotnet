using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class ExceededLimitExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is ExceededLimitException exceededLimitException )
    {
      context.Result = new ObjectResult(exceededLimitException.Value)
      {
        StatusCode = exceededLimitException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}