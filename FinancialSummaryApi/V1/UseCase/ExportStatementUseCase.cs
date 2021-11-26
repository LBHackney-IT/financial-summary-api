
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportStatementUseCase : IExportStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        //private readonly PdfGenerator _pdfGenerator;
        private readonly IGeneratePdf _generatePdf;
        private readonly ILogger<ExportStatementUseCase> _logger;
        public ExportStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, IGeneratePdf generatePdf, ILogger<ExportStatementUseCase> logger)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _generatePdf = generatePdf;
            _logger = logger;
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

            var html = TemplateGenerator.GetHTMLReportString1();
            var result = request?.FileType switch
            {
                "csv" => FileGenerator.WriteCSVFile(response, name, period),
                "pdf" => _generatePdf.GetPDF(html),//FileGenerator.WritePdfFile(response, name, period),
                _ => null
            };
            _logger.LogInformation("File successfully geneated");
            return result;
        }
    }
}
