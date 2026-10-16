
using Microsoft.Data.SqlClient;

namespace EmployeeManagementSystem.Middleware
{
    public class ExceptionHandelingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandelingMiddleware> _logger;

        public ExceptionHandelingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandelingMiddleware> logger)
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
            catch (SqlException ex)
            {
                _logger.LogError(
                    ex,
                    "Database error occurred for path {Path}",
                    context.Request.Path);

                await HandleExceptionAsync(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "An unexpected error occurred for path {Path}",
                    context.Request.Path);

                await HandleExceptionAsync(context);
            }
        }

        private static async Task HandleExceptionAsync(
            HttpContext context)
        {
            if (context.Response.HasStarted)
            {
                throw new InvalidOperationException(
                    "The response has already started.");
            }

            context.Response.Clear();
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            await context.Response.WriteAsJsonAsync(new
            {
                statusCode = 500,
                message = "An internal server error occurred."
            });
        }
    }
}
