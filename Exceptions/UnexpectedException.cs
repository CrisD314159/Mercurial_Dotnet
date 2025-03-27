namespace MercurialBackendDotnet.Exceptions;


public class UnexpectedException(object? value = null) : Exception
{
  public int StatusCode {get;} = 500;

  public object? Value {get;} = value;
}