using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using NodaMoney;
using System.Collections.Generic;
using System.Linq;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public interface IPdfGenerator
    {
        byte[] BuildPdf(List<Statement> transactions, string name, string period);
    }

    public class PdfGenerator : IPdfGenerator
    {
        private readonly IConverter _pdfConverter = new SynchronizedConverter(new PdfTools());

        public byte[] BuildPdf(List<Statement> transactions, string name, string period)
        {

            var report = new ExportResponse();
            var data = new List<ExportTransactionResponse>();

            report.BankAccountNumber = string.Join(",", transactions.Select(x => x.RentAccountNumber).Distinct().ToArray());
            report.Balance = Money.PoundSterling(transactions.LastOrDefault().FinishBalance).ToString();
            report.BalanceBroughtForward = Money.PoundSterling(transactions.FirstOrDefault().StartBalance).ToString();
            report.StatementPeriod = period;
            foreach (var item in transactions)
            {

                data.Add(
                   new ExportTransactionResponse
                   {
                       Date = item.StatementPeriodEndDate.ToString("dd MMM yyyy"),
                       TransactionDetail = item.TargetType.ToString(),
                       Debit = Money.PoundSterling(item.PaidAmount).ToString(),
                       Credit = Money.PoundSterling(item.HousingBenefitAmount).ToString(),
                       Balance = Money.PoundSterling(item.FinishBalance).ToString()
                   });
            }
            report.Data = data;
            const double margin = 25;
            var test = TemplateGenerator.GetHTMLReportString(report);
            return _pdfConverter.Convert(new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    Orientation = Orientation.Portrait,
                    PaperSize = PaperKind.A4,
                    Margins = new MarginSettings(margin, margin, margin, margin),
                },
                Objects =
                {
                    new ObjectSettings
                    {
                        WebSettings = new WebSettings
                        {
                            PrintMediaType = true,
                            EnableIntelligentShrinking = true
                        },
                        HtmlContent = TemplateGenerator.GetHTMLReportString(report)
                    }
                }
            });
        }
    }
}
