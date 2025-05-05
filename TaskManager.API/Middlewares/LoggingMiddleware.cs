using System.Data;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Exceptions;

namespace TaskManager.API.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            await _next(context);

            var controllerName = context.GetRouteData().Values["controller"];
            var actionName = context.GetRouteData().Values["action"];
            
            _logger.LogInformation("API action: {ActionName}, controller: {ControllerName}", actionName, controllerName);
        }
    }
}