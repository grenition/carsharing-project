using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class UserNotFoundException() : ApiException("User not found");
