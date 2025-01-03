﻿using API.DTOs;
using API.Exceptions;
using System.Text.Json;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _env;

        public ExceptionMiddleware(
            RequestDelegate next,
            ILogger<ExceptionMiddleware> logger,
            IHostEnvironment env
            )
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = 500;

                var errorResponse = _env.IsDevelopment() ?
                    new ApiException((short)context.Response.StatusCode, ex.Message, ex.StackTrace)
                    :
                    new ApiException((short)context.Response.StatusCode, ex.Message, "a server error has occured.");

                await context.Response.WriteAsJsonAsync(errorResponse);
            }
        }
    }
}
