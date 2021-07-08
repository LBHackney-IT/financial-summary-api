using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        private string _name;

        public NonEmptyGuidAttribute(string name)
        {
            _name = name;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value is Guid guid) && Guid.Empty == guid ?
                new ValidationResult($"{_name} cannot be empty or default.") : null;
        }
    }
}
