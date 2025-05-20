using System.Net;

namespace SharedFramework.Exceptions.Common;

public class UnauthorizedException : ApiException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.Unauthorized;
    
    public UnauthorizedException() : base("Unauthorized") { }
    public UnauthorizedException(string message) : base(message) { }
}
