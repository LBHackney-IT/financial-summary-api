using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportStatementUseCase : IExportStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IPDFService _pdfService;
        public ExportStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, IPDFService pdfService)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _pdfService = pdfService;
        }

        public async Task<byte[]> ExecuteAsync(ExportStatementRequest request)
        {
            DateTime startDate;
            DateTime endDate;
            string name;
            string period;
            if (request.TypeOfStatement == TypeOfStatement.Quarterly)
            {
                startDate = DateTime.UtcNow.AddMonths(-3);
                endDate = DateTime.UtcNow;
                name = TypeOfStatement.Quarterly.ToString();
                period = $"{startDate:D} to {endDate:D}";
            }
            else
            {
                startDate = DateTime.UtcNow.AddMonths(-12);
                endDate = DateTime.UtcNow;
                name = TypeOfStatement.Yearly.ToString();
                period = $"{startDate:D} to {endDate:D}";
            }

            var response = await _financeSummaryGateway.GetStatementListAsync(request.TargetId, startDate, endDate).ConfigureAwait(false);
            if (response.Any())
            {

                var result = request?.FileType switch
                {
                    "csv" => FileGenerator.WriteCSVFile(response, name, period),
                    "pdf" => await _pdfService.Create(response, name, period).ConfigureAwait(false),
                    _ => null
                };
                return result;
            }

            return null;
        }
    }
}
