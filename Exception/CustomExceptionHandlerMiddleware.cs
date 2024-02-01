using FluentValidation;
using karavancidan.Model.Middleware.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace karavancidan.Model.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
        private readonly CustomErrorService _customErrorService;

        public CustomExceptionHandlerMiddleware(RequestDelegate next, ILogger<CustomExceptionHandlerMiddleware> logger, CustomErrorService customErrorService)
        {
            _next = next;
            _logger = logger;
            _customErrorService = customErrorService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
               context.Response.ContentType = "application/json";

                if (ex is ValidationException validationException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var errorResult = _customErrorService.CreateErrorResult(validationException);
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResult));
                }
                else if (ex is CustomException || (ex.InnerException != null && ex.InnerException is CustomException))
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict; 
                    var errorMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;
                    var errorResult = _customErrorService.CreateErrorResult(errorMessage);
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResult));
                }
                else if (ex is ConflictException)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Conflict; 
                    var errorResult = new ConflictErrorResultModel();
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResult));
                }
                else
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    var errorResult = _customErrorService.CreateErrorResult("An unexpected error occurred.");
                    await context.Response.WriteAsync(System.Text.Json.JsonSerializer.Serialize(errorResult));
                }
            }
        }

    }

}

