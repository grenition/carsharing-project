using System.Collections.Concurrent;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedFramework.Extensions;

namespace SharedFramework.Exceptions;

public class ErrorHandlerMiddleware : IMiddleware
{
    private static readonly ConcurrentDictionary<Type, string> _titles = new();

    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly IProblemDetailsService _problemDetailsService;

    public ErrorHandlerMiddleware(
        ILogger<ErrorHandlerMiddleware> logger,
        IProblemDetailsService problemDetailsService)
    {
        _logger = logger;
        _problemDetailsService = problemDetailsService;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (exception is ApiException apiEx)
        {
            var title = _titles.GetOrAdd(exception.GetType(), exType =>
                exType.Name
                    .RemovePostfix("Exception")
                    .AddSpacesBetweenWords()
            );

            await WriteProblemAsync(context, apiEx.StatusCode, apiEx.Message, title);
        }
        else
        {
            _logger.LogError(exception, "Internal Server Error");
            await WriteProblemAsync(context, HttpStatusCode.InternalServerError, "An error happened on server side.");
        }
    }

    private async Task WriteProblemAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string detail,
        string? customTitle = null)
    {
        context.Response.StatusCode = (int)statusCode;

        var problemDetails = new ProblemDetails
        {
            Title = customTitle ?? GetTitle(statusCode),
            Detail = detail
        };

        await _problemDetailsService.WriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = context,
                ProblemDetails = problemDetails
            }
        );
    }

    private static string GetTitle(HttpStatusCode statusCode) => statusCode switch
    {
        HttpStatusCode.BadRequest => "Bad Request",
        HttpStatusCode.NotFound => "Not Found",
        HttpStatusCode.Unauthorized => "Unauthorized",
        HttpStatusCode.Forbidden => "Forbidden",
        HttpStatusCode.Conflict => "Conflict",
        HttpStatusCode.InternalServerError => "Internal Server Error",
        _ => "Error"
    };
}
