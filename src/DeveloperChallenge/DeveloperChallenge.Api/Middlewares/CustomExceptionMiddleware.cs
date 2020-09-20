using DeveloperChallenge.Api.DTO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DeveloperChallenge.Api.Middlewares
{
    public class CustomExceptionMiddleware : IMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly IWebHostEnvironment _env;

        public CustomExceptionMiddleware(RequestDelegate next, ILogger logger, IWebHostEnvironment env)
        {
            _env = env;
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                _logger.Log(LogLevel.Debug, "api - ok");
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                _logger.Log(LogLevel.Error, ex.Message + " - " + ex.StackTrace, ex);
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var message = _env.IsDevelopment() ?
                          exception.Message :
                          "Unexpected error ocurred. Contact the system's adminstrator";

            await response.WriteAsync(JsonConvert.SerializeObject(
                new DefaultResponse(
                   HttpStatusCode.InternalServerError,
                    message)
                ));
        }
    }
}
