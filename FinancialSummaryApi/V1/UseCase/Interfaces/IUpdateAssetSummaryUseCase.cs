using FinancialSummaryApi.V1.Boundary.Request;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IUpdateAssetSummaryUseCase
    {
        Task ExecuteAsync(UpdateAssetSummaryRequest assetSummary);
    }
}
