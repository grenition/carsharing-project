using System.Net;
using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class InvalidCredentialsException(string? message) : ApiException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
