using System.Net;

namespace SharedFramework.Exceptions;

public sealed record ExceptionResponse(object Response, HttpStatusCode StatusCode);
