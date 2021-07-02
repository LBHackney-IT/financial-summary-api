using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public class AllowedValuesAttribute : ValidationAttribute
    {
        private readonly List<TargetType> _allowedEnumItems;

        public AllowedValuesAttribute(params TargetType[] allowedEnumItems)
        {
            _allowedEnumItems = allowedEnumItems.ToList();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return new ValidationResult($"{validationContext.MemberName} is required.");
            }

            var valueType = value.GetType();
            if(!valueType.IsEnum || !Enum.IsDefined(typeof(TargetType), value))
            {
                return new ValidationResult($"{validationContext.MemberName} should be a type of TargetType enum.");
            }

            var isValid = _allowedEnumItems.Contains((TargetType)value);

            if(isValid)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"{validationContext.MemberName} should be in a range: [{string.Join(", ", _allowedEnumItems.Select(a => $"{(int) a}({a})"))}].");
            }
        }
    }
}
