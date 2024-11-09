using System.Text.Json.Serialization;

namespace Tbc.PhysicalPersonsDirectory.Api.Infrastructure.Middlewares.ErrorHandling;

public class CustomProblemDetails
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("errorCode")]
    public int ErrorCode { get; set; }

    [JsonPropertyName("error")]
    public string Error { get; set; }

    [JsonPropertyName("traceId")]
    public string TraceId { get; set; }

    [JsonPropertyName("stackTrace")]
    public string StackTrace { get; set; }
}