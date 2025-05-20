using SharedFramework.Exceptions;

namespace Users.Application.Exception;

public class NumericCodeIsInvalidException() : ApiException("Numeric code is invalid");