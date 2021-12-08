using FinancialSummaryApi.V1.Boundary.Request;
using System.IO;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Interfaces
{
    public interface IExportFinishPdfStatementUseCase
    {
        Task<Stream> ExecuteAsync(ExportStatementRequest request);
    }
}
