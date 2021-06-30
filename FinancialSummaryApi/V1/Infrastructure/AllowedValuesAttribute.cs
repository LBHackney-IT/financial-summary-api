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

        public override bool IsValid(object value)
        {
            if(value == null)
            {
                return false;
            }

            var valueType = value.GetType();
            if(!valueType.IsEnum || !Enum.IsDefined(typeof(TargetType), value))
            {
                ErrorMessage = "Provided value should be a type of TargetType enum.";
                return false;
            }

            var isValid = _allowedEnumItems.Contains((TargetType)value);

            if(!isValid)
            {
                ErrorMessage = $"Provided value should be in a range: [{string.Join(", ", _allowedEnumItems.Select(a => $"{(int)a}({a})"))}].";
            }

            return isValid;
        }
    }
}
