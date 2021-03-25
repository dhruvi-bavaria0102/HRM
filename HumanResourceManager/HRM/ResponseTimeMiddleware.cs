using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace HRM
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ResponseTimeMiddleware
    {
        // Name of the Response Header, Custom Headers starts with "X-"  
        private const string RESPONSE_HEADER_RESPONSE_TIME = "X-Response-Time-ms";
        // Handle to the next Middleware in the pipeline  
        private readonly RequestDelegate _next;
        private readonly ILogger<ResponseTimeMiddleware> _logger;
        public ResponseTimeMiddleware(RequestDelegate next, ILoggerFactory loggerFactory)
        {
            _next = next;
            _logger = loggerFactory.CreateLogger<ResponseTimeMiddleware>();
        }
        public Task InvokeAsync(HttpContext context)
        {
            // Start the Timer using Stopwatch  
            var watch = new Stopwatch();
            watch.Start();
            context.Response.OnStarting(() => {
                // Stop the timer information and calculate the time   
                watch.Stop();
                var responseTimeForCompleteRequest = watch.ElapsedMilliseconds;
                var stringLog = $"Response time for the action {context.Request.Path} took {responseTimeForCompleteRequest} ms";

                _logger.LogInformation(stringLog);
                // Add the Response time information in the Response headers.   
                context.Response.Headers[RESPONSE_HEADER_RESPONSE_TIME] = responseTimeForCompleteRequest.ToString();
                return Task.CompletedTask;
            });
            // Call the next delegate/middleware in the pipeline   
            return this._next(context);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ResponseTimeMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseTimeMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseTimeMiddleware>();
        }
    }
}
