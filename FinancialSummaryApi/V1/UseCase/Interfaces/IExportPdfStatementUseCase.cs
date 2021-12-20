using FinancialSummaryApi.V1.Boundary.Request;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IExportPdfStatementUseCase
    {
        Task<string> ExecuteAsync(ExportStatementRequest request);
    }
}
