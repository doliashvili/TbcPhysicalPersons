using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;
using System.Globalization;
using System.Reflection;
using Tbc.PhysicalPersonsDirectory.Application.Exceptions;
using Tbc.PhysicalPersonsDirectory.Application.Options;
using Tbc.PhysicalPersonsDirectory.Application.Resources;

namespace Tbc.PhysicalPersonsDirectory.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            // Configs
            services.Configure<ImagesOptions>(configuration.GetSection(nameof(ImagesOptions)));
            services.Configure<PageOptions>(configuration.GetSection(nameof(PageOptions)));

            // Resources
            AddLocalizationResource(services, configuration);

            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();

            AddMediator(services, assemblies);
            AddFluentValidation(services, assemblies);

            return services;
        }

        private static void AddFluentValidation(IServiceCollection services, Assembly[] assemblies)
        {
            var validatorTypes = assemblies
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.BaseType?.IsGenericType == true &&
                               type.BaseType.GetGenericTypeDefinition() == typeof(AbstractValidator<>));

            foreach (var validatorType in validatorTypes)
            {
                services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters()
                    .AddValidatorsFromAssemblyContaining(validatorType);
            }
        }

        private static void AddMediator(IServiceCollection services, Assembly[] assemblies)
        {
            foreach (var item in assemblies)
            {
                services.AddMediatR(item);
            }
        }

        private static void AddLocalizationResource(IServiceCollection services, IConfiguration configuration)
        {
            // Add localization services
            services.AddLocalization();
            services.AddSingleton<ExceptionLocalizeConstants>();

            var serviceProvider = services.BuildServiceProvider();
            var requiredService = serviceProvider.GetRequiredService<IStringLocalizer<ExceptionResources>>();

            // Initialize static properties with the
            ExceptionLocalizeConstants.Initialize(requiredService);

            // Configure LocalizationOptions
            var localSection = configuration.GetSection(nameof(LocalizationCustomOptions));
            services.Configure<LocalizationCustomOptions>(localSection);

            // Configure request localization
            var localizationOptions = localSection.Get<LocalizationCustomOptions>();
            var supportedCultures = localizationOptions.SupportedCultures.Select(c => new CultureInfo(c)).ToArray();

            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new RequestCulture(localizationOptions.DefaultCulture);
                options.SupportedCultures = supportedCultures;
                options.SupportedUICultures = supportedCultures;
            });
        }
    }
}