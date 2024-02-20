using Echooling.Aplication.DTOs.ResponseDTOs;
using Echooling.Persistance.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Newtonsoft.Json;
using System.Net;

namespace Echooling.API.Middlewares
{
    public static class ExceptionHandlerMiddleware
    {
        public static IApplicationBuilder useCustomExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(errorApp =>
            {
                errorApp.Run(async context =>
                {
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    var statusCode = (int)HttpStatusCode.InternalServerError;
                    string message = "Internal server error";

                    if (contextFeature is not null)
                    {
                        if (contextFeature.Error is IBaseException)
                        {
                            var exception = (IBaseException)contextFeature.Error;
                            statusCode = exception.StatusCode;
                            message = exception.CustomMessage;
                        }
                    }
                    context.Response.StatusCode = statusCode;
                    await context.Response.WriteAsJsonAsync(new ExceptionResponseDto(statusCode, message));
                });
            });
            return app;
        }
    }
}
