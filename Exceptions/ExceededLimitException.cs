namespace MercurialBackendDotnet.Exceptions;

public class ExceededLimitException(object? value = null): Exception
{
  public int StatusCode{get; set;} = 409;
  public object? Value = value;
}