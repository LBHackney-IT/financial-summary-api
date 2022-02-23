using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetAssetSummaryByIdAndYearUseCase
    {
        Task<AssetSummaryResponse> ExecuteAsync(Guid assetId, short summaryYear, ValuesType valuesType);
    }
}
