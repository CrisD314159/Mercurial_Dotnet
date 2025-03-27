namespace MercurialBackendDotnet.Exceptions;


public class EntityAlreadyExistsException(object? value = null) : Exception
{
  public int StatusCode {get; } = 400;
  public object? Value { get; } = value;
}