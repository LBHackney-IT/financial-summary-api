
using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportStatementUseCase : IExportStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly PdfGenerator _pdfGenerator;

        public ExportStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, PdfGenerator pdfGenerator)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _pdfGenerator = pdfGenerator;
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


            //var result = request?.FileType switch
            //{
            //    "csv" => FileGenerator.WriteCSVFile(response, name, period),
            //    "pdf" => _pdfGenerator.BuildPdf(response, name, period),//FileGenerator.WritePdfFile(response, name, period),
            //    _ => null
            //};
            //return result;

            var a2pClient = new Api2Pdf.Api2Pdf("32560233-0606-489d-a44a-b512e82ef922");
            var request1 = new Api2Pdf.ChromeHtmlToPdfRequest
            {
                Html = "<p>Hello World</p>"
            };
            var apiResponse = a2pClient.Chrome.HtmlToPdf(request1);
            var resultAsBytes = apiResponse.GetFileBytes();
            return resultAsBytes;
        }
    }
}
