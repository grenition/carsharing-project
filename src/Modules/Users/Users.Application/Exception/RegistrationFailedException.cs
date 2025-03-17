using System.Net;
using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class RegistrationFailedException(string? message = null) : ApiException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}
