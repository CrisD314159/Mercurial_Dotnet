namespace MercurialBackendDotnet.Application.ApplicationExceptions;


public class UnauthorizedException(string value) : Exception
{
  public int StatusCode {get;} = 401;
  public override string Message {get;} = value;
}