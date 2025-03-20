using System.Collections.Concurrent;
using System.Net;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace SharedFramework.Exceptions;

public class ErrorHandlerMiddleware : IMiddleware
{
    private static readonly ConcurrentDictionary<Type, string> Codes = new();

    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    
    public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleErrorAsync(context, exception);
        }
    }

    private async Task HandleErrorAsync(HttpContext context, Exception exception)
    {
        var errorResponse = exception switch
        {
            ApiException ex => new ExceptionResponse(
                new ErrorsResponse(new Error(GetErrorCode(ex), ex.Message)), ex.StatusCode),
            _ => new ExceptionResponse(new ErrorsResponse(
                    new Error("error", "There was an internal error.")), HttpStatusCode.InternalServerError)
        };
        
        context.Response.StatusCode = (int)(errorResponse?.StatusCode ?? HttpStatusCode.InternalServerError);
        
        if(context.Response.StatusCode == (int)HttpStatusCode.InternalServerError)
            _logger.LogError(exception, exception.Message);
        
        var responce = errorResponse?.Response;
        if (responce is null)
            return;

        await context.Response.WriteAsJsonAsync(responce);
    }

    private record Error(string Code, string Message);
    private record ErrorsResponse(params Error[] Errors);

    private static string GetErrorCode(object exception)
    {
        var type = exception.GetType();
        return Codes.GetOrAdd(type, type.Name.Underscore().Replace("_exception", string.Empty));
    }
}
