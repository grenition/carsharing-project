using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class UserInvalidConfigurationException(string message) : ApiException(message);