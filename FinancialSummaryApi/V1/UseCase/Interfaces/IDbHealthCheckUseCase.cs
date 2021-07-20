using FinancialSummaryApi.V1.Boundary;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IDbHealthCheckUseCase
    {
        HealthCheckResponse Execute();
    }
}
