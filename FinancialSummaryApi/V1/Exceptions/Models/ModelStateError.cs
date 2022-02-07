namespace FinancialSummaryApi.V1.Exceptions.Models
{
    public class ModelStateError
    {
        public ModelStateError(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
