using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class InvalidTokenException(string message) : ApiException(message);
