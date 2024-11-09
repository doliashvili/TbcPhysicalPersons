using Microsoft.Extensions.Options;
using System.Globalization;

namespace Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.LocalizationLanguage
{
    //• API middleware-ის შექმნა მოთხოვნის Accept-Language HTTP header პარამეტრის
    public class LocalizationMiddleware
    {
        private readonly RequestDelegate _next;

        public LocalizationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IOptions<RequestLocalizationOptions> localizationOptions)
        {
            var languageHeader = context.Request.Headers["Accept-Language"].ToString();

            // If the header is present, set the culture
            if (!string.IsNullOrEmpty(languageHeader))
            {
                var supportedCultures = localizationOptions.Value.SupportedCultures;
                var defaultCulture = localizationOptions.Value.DefaultRequestCulture.Culture;

                var culture = supportedCultures.FirstOrDefault(c => c.Name.StartsWith(languageHeader, StringComparison.InvariantCultureIgnoreCase)) ?? defaultCulture;
                var cultureInfo = new CultureInfo(culture.Name);

                CultureInfo.CurrentCulture = cultureInfo;
                CultureInfo.CurrentUICulture = cultureInfo;
            }

            await _next(context);
        }
    }
}