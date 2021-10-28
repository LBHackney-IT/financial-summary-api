using FinancialSummaryApi.V1.Boundary.Request;
using FluentValidation;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Infrastructure.Validators
{
    public class AddStatementRequestListValidator : AbstractValidator<List<AddStatementRequest>>
    {
        public AddStatementRequestListValidator()
        {
            RuleForEach(x => x).SetValidator(new AddStatementRequestValidator());
        }
    }
}
