using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Boundary.Response;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IAddRentGroupSummaryUseCase
    {
        Task<RentGroupSummaryResponse> ExecuteAsync(AddRentGroupSummaryRequest groupSummaryRequest);
    }
}
