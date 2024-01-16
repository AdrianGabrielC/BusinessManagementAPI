using BusinessManagementAPI.Utilities.ErrorHandling.UnauthorizedExceptions;
using GlanzCleanAPI.Utilities.ErrorHandling;
using GlanzCleanAPI.Utilities.ErrorHandling.NotFoundExceptions;
using GlanzCleanAPI.Utilities.Logging;
using Microsoft.AspNetCore.Diagnostics;

namespace GlanzCleanAPI.Utilities.Extensions
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void ConfigureExceptionHandler(this WebApplication app, ILoggerManager logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        context.Response.StatusCode = contextFeature.Error switch
                        {
                            BadHttpRequestException => StatusCodes.Status400BadRequest,
                            NotFoundException => StatusCodes.Status404NotFound,
                            UnauthorizedException => StatusCodes.Status401Unauthorized,
                            _ => StatusCodes.Status500InternalServerError
                        };

                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        await context.Response.WriteAsync(new ErrorDetails()
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = contextFeature.Error.Message
                        }.ToString());
                    }
                });
            });
        }

    }
}
