using FinancialSummaryApi.V1.Boundary.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IAddBatchUseCase
    {
        public Task<int> ExecuteAsync(IEnumerable<AddAssetSummaryRequest> assetSummaryRequests);
    }
}
