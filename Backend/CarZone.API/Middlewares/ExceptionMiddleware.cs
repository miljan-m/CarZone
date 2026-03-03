using System.Text.Json;
using CarZone.Application.Exceptions;

namespace CarZone.API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleAsync(context, ex);
            }
        }

        private static Task HandleAsync(HttpContext context, Exception ex)
        {
            context.Response.ContentType = "application/json";

            if (ex is CustomException exception)
            {
                context.Response.StatusCode = exception.StatusCode;

                return context.Response.WriteAsync(
                    JsonSerializer.Serialize(new
                    {
                        message = exception.Message
                    })
                );
            }

            // fallback
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(new
                {
                    message = "Internal server error"
                })
            );
        }
    }
}