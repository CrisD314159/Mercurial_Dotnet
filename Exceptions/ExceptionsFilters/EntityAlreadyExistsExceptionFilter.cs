using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class EntityAlreadyExistsExceptionFilter: IActionFilter, IOrderedFilter
{
  public int Order => int.MaxValue-10;

  public void OnActionExecuting(ActionExecutingContext context){}

  public void OnActionExecuted(ActionExecutedContext context)
  {
    if(context.Exception is EntityAlreadyExistsException entityAlreadyExistsException )
    {
      context.Result = new ObjectResult(entityAlreadyExistsException.Value)
      {
        StatusCode = entityAlreadyExistsException.StatusCode
      };
      context.ExceptionHandled = true;
    }
    
  }
}