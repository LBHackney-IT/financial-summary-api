using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace FinancialSummaryApi.V1.Infrastructure.Converters
{
    // TODO: This should go in a common NuGet package...

    /// <summary>
    /// Converter for sub-objects
    /// Treats a custom sub-objects as straight Json, meaning any DynamoDb attributes it may have are not applied
    /// Will (de)serialise any enum properties as the name value (not the numeric value)
    /// </summary>
    public class DynamoDbObjectConverter<T> : IPropertyConverter
    {
        private static JsonSerializerOptions CreateJsonOptions()
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
            options.Converters.Add(new JsonStringEnumConverter());
            return options;
        }

        public DynamoDBEntry ToEntry(object value)
        {
            if (null == value) return new DynamoDBNull();

            return Document.FromJson(JsonSerializer.Serialize(value, CreateJsonOptions()));
        }

        public object FromEntry(DynamoDBEntry entry)
        {
            if ((null == entry) || (null != entry.AsDynamoDBNull())) return null;

            var doc = entry.AsDocument();
            if (null == doc)
                throw new ArgumentException("Field value is not a Document. This attribute has been used on a property that is not a custom object.");

            return JsonSerializer.Deserialize<T>(doc.ToJson(), CreateJsonOptions());
        }
    }
}
