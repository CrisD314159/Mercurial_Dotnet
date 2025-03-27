namespace MercurialBackendDotnet.Exceptions;


public class EntityNotFoundException(object? value = null) : Exception
{


  public int StatusCode {get;} = 404;
  public object? Value {get;} = value;
}