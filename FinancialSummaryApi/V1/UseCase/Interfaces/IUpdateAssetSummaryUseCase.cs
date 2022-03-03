using FinancialSummaryApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IUpdateAssetSummaryUseCase
    {
        Task ExecuteAsync(AssetSummaryResponse response);
    }
}
