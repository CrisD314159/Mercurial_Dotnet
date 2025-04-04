using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MercurialBackendDotnet.Exceptions.ExceptionsFilters;

public class GlobalExceptionFilter : IExceptionFilter
{
    private readonly ILogger<GlobalExceptionFilter> _logger;

    // Mapeo de excepciones a códigos de estado HTTP
    private static readonly Dictionary<Type, int> ExceptionStatusCodes = new()
    {
        { typeof(EntityNotFoundException), StatusCodes.Status404NotFound },
        { typeof(UnauthorizedException), StatusCodes.Status401Unauthorized },
        { typeof(ValidationException), StatusCodes.Status400BadRequest },
        { typeof(EntityAlreadyExistsException), StatusCodes.Status400BadRequest },
        { typeof(ExceededLimitException), StatusCodes.Status409Conflict },
        { typeof(VerificationException), StatusCodes.Status400BadRequest}
        // Agrega aquí más excepciones personalizadas según sea necesario
    };

    public GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger)
    {
        _logger = logger;
    }

    public void OnException(ExceptionContext context)
    {
        var exceptionType = context.Exception.GetType();

        // Verifica si la excepción está en el diccionario
        if (ExceptionStatusCodes.TryGetValue(exceptionType, out int statusCode))
        {
            context.Result = new ObjectResult(new { message = context.Exception.Message, success = false })
            {
                StatusCode = statusCode
            };
        }
        else
        {
            // Si la excepción no está en la lista, se maneja como un error 500
            _logger.LogError(context.Exception, "Unhandled exception");
            context.Result = new ObjectResult(new { message = "An unexpected error occured", success = false })
            {
                StatusCode = StatusCodes.Status500InternalServerError
            };
        }

        context.ExceptionHandled = true;
    }
}