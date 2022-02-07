namespace FinancialSummaryApi.V1.Exceptions.Models
{
    /// <summary>
    /// Object that holds a single Validation Error for the business object
    /// </summary>
    public class ValidationError
    {
        /// <summary>The error message for this validation error.</summary>
        public string Message { get; set; } = "";

        /// <summary>The name of the field that this error relates to.</summary>
        public string ControlId { get; set; } = "";

        /// <summary>
        /// An ID set for the Error. This ID can be used as a correlation between bus object and UI code.
        /// </summary>
        public string Id { get; set; } = "";
    }
}
