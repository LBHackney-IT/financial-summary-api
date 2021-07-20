using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Factories;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class GetAllWeeklySummariesUseCase : IGetAllWeeklySummariesUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public GetAllWeeklySummariesUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<List<WeeklySummaryResponse>> ExecuteAsync(Guid targetId, string startDate, string endDate)
        {

            return (await _financeSummaryGateway.GetAllWeeklySummaryAsync(targetId, startDate.CheckAndConvertDateTime(), endDate.CheckAndConvertDateTime()).ConfigureAwait(false)).ToResponse();
        }
    }
}
