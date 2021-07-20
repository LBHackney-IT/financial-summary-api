using FinancialSummaryApi.V1.Boundary;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.Extensions.HealthChecks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class DbHealthCheckUseCase : IDbHealthCheckUseCase
    {
        private readonly IHealthCheckService _healthCheckService;

        public DbHealthCheckUseCase(IHealthCheckService healthCheckService)
        {
            _healthCheckService = healthCheckService;
        }

        public HealthCheckResponse Execute()
        {
            var result = _healthCheckService.CheckHealthAsync().Result;

            var success = result.CheckStatus == CheckStatus.Healthy;
            return new HealthCheckResponse(success, result.Description);
        }
    }
}
