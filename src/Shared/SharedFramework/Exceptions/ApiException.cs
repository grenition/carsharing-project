using System.Net;

namespace SharedFramework.Exceptions;

public class ApiException : Exception
{
    public virtual HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
    
    protected ApiException() : base()
    {
    }
    protected ApiException(string? message) : base(message)
    {
    }
}
