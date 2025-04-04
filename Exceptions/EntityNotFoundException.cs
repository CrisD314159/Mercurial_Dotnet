namespace MercurialBackendDotnet.Exceptions;


public class EntityNotFoundException(string message) : Exception
{


  public int StatusCode {get;} = 404;
  public override string Message {get;} = message;
}