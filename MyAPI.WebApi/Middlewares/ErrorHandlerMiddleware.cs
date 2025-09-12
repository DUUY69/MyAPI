using MyAPI.Services.Exceptions;
using System.Net;
using System.Text.Json;

namespace MyAPI.WebApi.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            object response;
            int statusCode;

            switch (exception)
            {
                case AppException appException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        Success = false,
                        Message = appException.Message
                    };
                    break;
                case ArgumentException:
                    statusCode = (int)HttpStatusCode.BadRequest;
                    response = new
                    {
                        Success = false,
                        Message = "Invalid argument provided.",
                        Details = exception.Message
                    };
                    break;
                case UnauthorizedAccessException:
                    statusCode = (int)HttpStatusCode.Unauthorized;
                    response = new
                    {
                        Success = false,
                        Message = "Unauthorized access.",
                        Details = exception.Message
                    };
                    break;
                default:
                    statusCode = (int)HttpStatusCode.InternalServerError;
                    response = new
                    {
                        Success = false,
                        Message = "An error occurred while processing your request.",
                        Details = exception.Message
                    };
                    break;
            }

            context.Response.StatusCode = statusCode;
            var jsonResponse = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
