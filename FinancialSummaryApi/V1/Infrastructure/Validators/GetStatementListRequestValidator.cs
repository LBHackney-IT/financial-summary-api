using FinancialSummaryApi.V1.Boundary.Request;
using FluentValidation;

namespace FinancialSummaryApi.V1.Infrastructure.Validators
{
    public class GetStatementListRequestValidator : AbstractValidator<GetStatementListRequest>
    {
        public GetStatementListRequestValidator()
        {
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}
