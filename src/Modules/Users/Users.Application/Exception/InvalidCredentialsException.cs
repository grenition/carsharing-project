using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class InvalidCredentialsException(string message) : ApiException(message);
