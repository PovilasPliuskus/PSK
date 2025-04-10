using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Exceptions;

namespace TaskManager.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
         
        private static readonly Dictionary<Type, int> BuiltInExceptionStatusCodeMap = new()
        {
            { typeof(ArgumentNullException), StatusCodes.Status400BadRequest },
            // Here, add more built-in exceptions as needed
        };
         
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }
         
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                 
                // Decide what HTTP status to return:
                // - If it's our custom exception (inheriting from CustomException), use the status code from that exception.
                // - If it's a built-in .NET exception (like ArgumentNullException), get the status code from the BuiltInExceptionStatusCodeMap dictionary defined above.
                // - Otherwise, return 500.
                response.StatusCode = error switch
                {
                    CustomException e => (int)e.StatusCode,
                    _ => BuiltInExceptionStatusCodeMap.GetValueOrDefault(error.GetType(), StatusCodes.Status500InternalServerError)
                };
                 
                var problemDetails = new ProblemDetails
                {
                    Status = response.StatusCode,
                    Title = error.Message,
                };
                
                _logger.LogError(error.Message);
                
                var result = JsonSerializer.Serialize(problemDetails);
                await response.WriteAsync(result);
            }
        }
    }
}