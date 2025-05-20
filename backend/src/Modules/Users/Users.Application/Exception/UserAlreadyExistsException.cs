using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class UserAlreadyExistsException() : ApiException("User already exists");
