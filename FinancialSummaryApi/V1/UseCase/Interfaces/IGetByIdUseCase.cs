using FinancialSummaryApi.V1.Boundary.Response;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IGetByIdUseCase
    {
        ResponseObject Execute(int id);
    }
}
