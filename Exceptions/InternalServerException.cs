namespace MercurialBackendDotnet.Exceptions;

public class InternalServerException(string message) : Exception
{
    public int StatusCode{get;} = 500;
  public override string Message { get; } = message;
}