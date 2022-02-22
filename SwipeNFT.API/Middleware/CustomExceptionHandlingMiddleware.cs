using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SwipeNFT.Shared.Infrastructure.Exceptions;

namespace SwipeNFT.API.Middleware
{
    /// <summary>
    /// Middleware for handling exceptions
    /// </summary>
    public class CustomExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlingMiddleware(RequestDelegate next)
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
                await HandleGlobalExceptionAsync(context, ex);
            }
        }

        private Task HandleGlobalExceptionAsync(HttpContext context, Exception exception)
        {
            if (exception is InputValidationException validationException)
            {
                Log.ForContext("ValidationError", validationException.Message)
                    .Warning("Validation error occurred in API.");
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                if (validationException.Message.IsNullOrEmpty())
                    return context.Response.WriteAsJsonAsync(new { validationException.Messages });
                return context.Response.WriteAsJsonAsync(new { validationException.Message });
            }
            else
            {
                var errorId = Guid.NewGuid();
                Log.ForContext("ErrorId", errorId)
                    .Error(exception, "Error occured in API");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return context.Response.WriteAsJsonAsync(new
                {
                    ErrorId = errorId,
                    Message = "Something bad happened in API. " +
                              "Contact admin if the issue persists."
                });
            }
        }
    }
}
