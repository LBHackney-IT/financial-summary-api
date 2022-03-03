using System.Text.Json.Serialization;

namespace FinancialSummaryApi.V1.Domain
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum ValuesType
    {
        Actual = 1,
        Estimate = 2
    }
}
