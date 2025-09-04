namespace MercurialBackendDotnet.Application.ApplicationExceptions;


public class ValidationException(string message) : Exception
{
  public int StatusCode {get;} = 400;
  public override string Message {get;} = message;
}