namespace MercurialBackendDotnet.Exceptions;


public class EntityAlreadyExistsException(string message) : Exception
{
  public int StatusCode {get; } = 400;
  public override string Message { get; } = message;
}