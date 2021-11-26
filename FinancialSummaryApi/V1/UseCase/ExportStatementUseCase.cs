
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;
using Wkhtmltopdf.NetCore;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportStatementUseCase : IExportStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        //private readonly PdfGenerator _pdfGenerator;
        readonly IGeneratePdf _generatePdf;
        public ExportStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, IGeneratePdf generatePdf)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _generatePdf = generatePdf;
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
            return result;

            //var a2pClient = new Api2Pdf.Api2Pdf("32560233-0606-489d-a44a-b512e82ef922");
            //var request1 = new Api2Pdf.ChromeHtmlToPdfRequest
            //{
            //    Html = "<p>Hello World</p>"
            //};
            //var apiResponse = a2pClient.Chrome.HtmlToPdf(request1);
            //var resultAsBytes = apiResponse.GetFileBytes();
            //return resultAsBytes;
        }
    }
}
