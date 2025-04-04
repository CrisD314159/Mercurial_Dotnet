namespace MercurialBackendDotnet.Exceptions;

public class ExceededLimitException: Exception
{

  public ExceededLimitException(string value)
  {
    Message = value;
  }
  public int StatusCode{get;} = 409;
  public override string Message {get;}
}