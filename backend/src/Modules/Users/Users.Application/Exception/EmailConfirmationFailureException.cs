using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class EmailConfirmationFailureException(string message) : ApiException(message);
