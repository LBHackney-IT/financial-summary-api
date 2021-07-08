using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public class NonEmptyGuidAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return (value is Guid guid) && Guid.Empty == guid ?
                new ValidationResult($"{validationContext.MemberName} cannot be empty or default.") : null;
        }
    }
}
