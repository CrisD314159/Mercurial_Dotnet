namespace MercurialBackendDotnet.Exceptions;


public class NotVerifiedException(object? value = null): Exception
{
  public int StatusCode {get;} = 401;
  public object? Value {get;} = value;
}