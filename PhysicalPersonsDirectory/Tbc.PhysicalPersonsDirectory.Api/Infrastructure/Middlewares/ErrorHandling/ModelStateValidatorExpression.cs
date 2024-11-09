using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;

namespace Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.ErrorHandling
{
    //• შევქმნათ საერთო Action Filter რომელიც გადაამოწმებს მოთხოვნის მონაცემებს და თუ
    //არ არის ვალიდური დააბრუნებს შესაბამის შეცდომას
    public static class ModelStateValidatorExpression
    {
        static ModelStateValidatorExpression()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;
        }

        public static IActionResult ProcessValidationError(ActionContext actionContext)
        {
            var errors = string.Join("; ", actionContext.ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage));

            // Log validation errors
            var logger = actionContext.HttpContext.RequestServices.GetRequiredService<ILogger<Program>>();
            logger.LogError("Validation failed: {@Errors}", errors);

            var problemDetails = new CustomProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = ExceptionLocalizeConstants.ValidationExceptionTitle,
                ErrorCode = ExceptionLocalizeConstants.ValidationExceptionCode,
                Error = errors,
                TraceId = Activity.Current?.RootId ?? actionContext.HttpContext.TraceIdentifier
            };

            return new BadRequestObjectResult(problemDetails);
        }
    }
}