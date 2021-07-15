using FinancialSummaryApi.V1.Boundary.Response;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetAllWeeklySummariesUseCase
    {
        Task<List<WeeklySummaryResponse>> ExecuteAsync(Guid targetId, string startDate, string endDate);
    }
}
