using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class VerificationExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is VerificationException verificationException )
    {
      context.Result = new ObjectResult(verificationException.Value)
      {
        StatusCode = verificationException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}