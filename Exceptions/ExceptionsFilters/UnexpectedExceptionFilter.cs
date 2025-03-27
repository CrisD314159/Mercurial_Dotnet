using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class UnexpectedExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is UnexpectedException unexpectedException )
    {
      context.Result = new ObjectResult(unexpectedException.Value)
      {
        StatusCode = unexpectedException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}