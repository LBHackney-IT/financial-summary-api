using FinancialSummaryApi.V1.Boundary.Request;
using FluentValidation;

namespace FinancialSummaryApi.V1.Infrastructure.Validators
{
    public class ExportSelectedStatementRequestValidator : AbstractValidator<ExportSelectedStatementRequest>
    {
        public ExportSelectedStatementRequestValidator()
        {
            When(x => x.StartDate.HasValue && x.EndDate.HasValue, () =>
              {
                  RuleFor(x => x.StatementIdsToExport).Must(s => s?.Count < 0 || s == null);
              });

            When(x => x.StatementIdsToExport?.Count > 0, () =>
            {
                RuleFor(x => x.StartDate).Must(s => !s.HasValue || s == null);
                RuleFor(x => x.EndDate).Must(s => !s.HasValue || s == null);
            });
        }
    }
}
