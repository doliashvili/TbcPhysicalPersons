using System.Diagnostics;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Extensions;

namespace Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.ErrorHandling
{
    //• API middleware-ის შექმნა დაუმუშავებელი შეცდომების ლოგირებისთვის
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private ILogger<GlobalErrorHandlingMiddleware> _logger;

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                _logger = context.RequestServices.GetRequiredService<ILogger<GlobalErrorHandlingMiddleware>>();
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            int status;
            string message;
            string stackTrace;
            string title = ExceptionLocalizeConstants.GlobalExceptionTitle;
            int code = ExceptionLocalizeConstants.GlobalExceptionCode;
            string traceId = Activity.Current?.RootId ?? context?.TraceIdentifier ?? Guid.NewGuid().ToString();

            if (exception is ObjectNotFoundException notFoundException)
            {
                status = StatusCodes.Status400BadRequest;
                message = notFoundException.Message;
                title = notFoundException.Title;
                stackTrace = notFoundException.StackTrace;
                code = notFoundException.Code;

                _logger.LogError(notFoundException, notFoundException.Title);
            }
            else if (exception is ConflictException conflictException)
            {
                status = StatusCodes.Status400BadRequest;
                message = conflictException.Message;
                title = conflictException.Title;
                stackTrace = conflictException.StackTrace;
                code = conflictException.Code;

                _logger.LogError(conflictException, conflictException.Title);
            }
            else if (exception is ArgumentNullException attemptException)
            {
                status = StatusCodes.Status400BadRequest;
                message = attemptException.Message;
                title = ExceptionLocalizeConstants.ArgumentNullExceptionTitle;
                stackTrace = attemptException.StackTrace;
                code = ExceptionLocalizeConstants.ArgumentNullExceptionCode;

                _logger.LogError(attemptException, attemptException.Message);
            }
            else
            {
                status = StatusCodes.Status500InternalServerError;
                message = exception.Message; //todo
                stackTrace = exception.StackTrace;
                _logger.LogError(exception, exception.Message);
            }

            var exceptionResult = new CustomProblemDetails
            {
                Title = title,
                Error = message,
                ErrorCode = code,
                Status = status,
                TraceId = traceId,
                StackTrace = stackTrace
            }.ToJson();

            context!.Response.ContentType = "application/json";
            context.Response.StatusCode = status;

            await context.Response.WriteAsync(exceptionResult);
        }
    }
}