using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class InvalidDataExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is InvalidDataException invalidDataException )
    {
      context.Result = new ObjectResult(invalidDataException.Value)
      {
        StatusCode = invalidDataException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}