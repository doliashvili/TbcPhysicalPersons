using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Swagger;

public class AddAcceptLanguageHeaderParameter : IOperationFilter
{
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        // If the operation parameters list doesn't exist, create it
        if (operation.Parameters == null)
        {
            operation.Parameters = new List<OpenApiParameter>();
        }

        // Add the Accept-Language header parameter to the operation
        operation.Parameters.Add(new OpenApiParameter
        {
            Name = "Accept-Language", // Name of the header parameter
            In = ParameterLocation.Header, // This means it's a header parameter
            Required = false, // Make this true if you want it to be required
            Description = "Specify the language of the response", // Description for the parameter
            Schema = new OpenApiSchema
            {
                Type = "string", // The type of the parameter
                Default = new OpenApiString("en-US") // Optional default value
            }
        });
    }
}