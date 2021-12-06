using DinkToPdf;
using DinkToPdf.Contracts;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using NodaMoney;
using RazorLight;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public interface IPDFService
    {
        Task<byte[]> Create(List<Statement> transactions, string name, string period);
    }
    public class PDFService : IPDFService
    {
        private readonly IRazorLightEngine _razorEngine;
        private readonly IConverter _pdfConverter;
        public PDFService(IRazorLightEngine razorEngine, IConverter pdfConverter)
        {
            _razorEngine = razorEngine;
            _pdfConverter = pdfConverter;
        }
        public async Task<byte[]> Create(List<Statement> transactions, string name, string period)
        {

            var model = new ExportResponse();
            var data = new List<ExportTransactionResponse>();

            model.BankAccountNumber = string.Join(",", transactions.Select(x => x.RentAccountNumber).Distinct().ToArray());
            model.Balance = Money.PoundSterling(transactions.LastOrDefault().FinishBalance).ToString();
            model.BalanceBroughtForward = Money.PoundSterling(transactions.FirstOrDefault().StartBalance).ToString();
            model.StatementPeriod = period;
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
            model.Data = data;
            //var templatePath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), $"V1/Templates/PDFTemplate.cshtml");
            var templatePath = Path.Combine(Directory.GetCurrentDirectory(), $"V1/Templates/PDFTemplate.cshtml");
            string template = await _razorEngine.CompileRenderAsync(templatePath, model).ConfigureAwait(false);
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings() { Top = 10, Bottom = 10, Left = 10, Right = 10 },
                DocumentTitle = "Simple PDF document",
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = template,
                WebSettings = { DefaultEncoding = "utf-8" },
                HeaderSettings = { FontName = "Arial", FontSize = 12, Line = true, Center = "Fun pdf document" },
                FooterSettings = { FontName = "Arial", FontSize = 12, Line = true, Right = "Page [page] of [toPage]" }
            };
            var pdf = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
            byte[] file = _pdfConverter.Convert(pdf);
            return file;
        }
    }
}
