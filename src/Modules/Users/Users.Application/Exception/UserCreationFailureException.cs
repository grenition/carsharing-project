using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class UserCreationFailureException(string message) : ApiException(message);