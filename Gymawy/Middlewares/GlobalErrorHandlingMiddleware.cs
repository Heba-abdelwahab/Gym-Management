using Domain.Exceptions;
using Gymawy.ErrorModels;
using Gymawy.Factories;
using System.Net;

namespace Gymawy.Middlewares;

public class GlobalErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger;

    public GlobalErrorHandlingMiddleware(RequestDelegate next,
        ILogger<GlobalErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }


    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next?.Invoke(httpContext)!;

            if (httpContext.Response.StatusCode == (int)HttpStatusCode.NotFound)
                await HandleNotFoundEndPointAsync(httpContext);

        }
        catch (Exception exception)
        {
            _logger.LogError($"something went wrong {exception}");


            await HandelExceptionAsync(httpContext, exception);
        }
    }

    private async Task HandleNotFoundEndPointAsync(HttpContext httpContext)
    {
        httpContext.Response.ContentType = "application/json";
        var response = new ErrorDetails
        {
            StatusCode = (int)HttpStatusCode.NotFound,
            Message = $"The End Point {httpContext.Request.Path} Not Found"
        }.ToString();

        await httpContext.Response.WriteAsync(response);
    }

    private async Task HandelExceptionAsync(HttpContext httpContext, Exception exception)
    {


        #region Header
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        httpContext.Response.ContentType = "application/json";
        #endregion

        var response = new ApiValidationErrorResponse
        {
            Message = exception.Message
        };

        httpContext.Response.StatusCode = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            UnAuthorizedException => (int)HttpStatusCode.Unauthorized,
            ValidationException validationException => HandleValidationException(validationException, response),
            _ => (int)HttpStatusCode.InternalServerError
        };


        response.StatusCode = httpContext.Response.StatusCode;


        await httpContext.Response.WriteAsync(response.ToString());
    }

    private int HandleValidationException(ValidationException validationException, ApiValidationErrorResponse response)
    {
        response.Errors = validationException.Errors;
        return (int)HttpStatusCode.BadRequest;
    }
}