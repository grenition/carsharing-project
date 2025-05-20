using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class EmailNotConfirmedException(string message) : ApiException(message);
