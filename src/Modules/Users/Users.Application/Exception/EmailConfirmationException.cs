using System.Net;
using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class EmailConfirmationException(string? message) : ApiException(message)
{
    public override HttpStatusCode StatusCode => HttpStatusCode.InternalServerError;
}
