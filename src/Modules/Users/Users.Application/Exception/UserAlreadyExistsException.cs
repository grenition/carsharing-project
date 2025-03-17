using System.Net;
using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class UserAlreadyExistsException : ApiException
{
    public override HttpStatusCode StatusCode => HttpStatusCode.BadRequest;
}
