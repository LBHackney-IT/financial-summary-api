using CsvHelper;
using CsvHelper.Configuration;
using DinkToPdf;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using NodaMoney;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FinancialSummaryApi.V1.UseCase.Helpers
{
    public static class FileGenerator
    {
        public static byte[] WritePdfFile(List<Statement> transactions, string name, string period)
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
            var globalSettings = new GlobalSettings
            {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DocumentTitle = $"{name} Statement Report"
            };
            var objectSettings = new ObjectSettings
            {
                PagesCount = true,
                HtmlContent = TemplateGenerator.GetHTMLReportString(report),
                WebSettings = { DefaultEncoding = "utf-8", UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "assets", "styles.css"), LoadImages = true },
                HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Page [page] of [toPage]", Line = true },
                FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = $"{name} Statement Report" }
            };
            var pdfDocument = new HtmlToPdfDocument()
            {
                GlobalSettings = globalSettings,
                Objects = { objectSettings }
            };
#pragma warning disable CA2000 // Dispose objects before losing scope
            var converter = new BasicConverter(new PdfTools());
#pragma warning restore CA2000 // Dispose objects before losing scope
            var pdfBuf = converter.Convert(pdfDocument);
            return pdfBuf;

        }
        public static byte[] WriteCSVFile(List<Statement> transactions, string name, string period)
        {
            var data = new List<ExportTransactionResponse>();
            foreach (var item in transactions)
            {

                data.Add(
                   new ExportTransactionResponse
                   {
                       Date = item.StatementPeriodEndDate.ToString("dd MMM yyyy"),
                       TransactionDetail = item.TargetType.ToString(),
                       Debit = item.PaidAmount.ToString(),
                       Credit = item.HousingBenefitAmount.ToString(),
                       Balance = item.FinishBalance.ToString()
                   });
            }

            byte[] result;
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                {
                    using var cw = new CsvWriter(sw, cc);
                    cw.WriteRecords(data);
                    cw.WriteComment($"{name} STATEMENT OF YOUR ACCOUNT");
                    cw.WriteComment($"for the period {period}");
                    cw.WriteComment($"As of {DateTime.Today:D} your account balance was {transactions.LastOrDefault().FinishBalance} in arrears.");
                    cw.WriteComment("As your landlord, the council has a duty to make sure all charges are paid up to date. This is because the housing income goes toward the upkeep of council housing and providing services for residents. You must make weekly charges payment a priority. If you don’t pay, you risk losing your home.");
                }
                result = ms.ToArray();
            }
            return result;
        }

        public static byte[] WriteManualCSVFile(IEnumerable<Statement> transactions)
        {
            var data = transactions.Select(_ => new
            {
                Date = _.StatementPeriodEndDate.ToString("dd/MM/yyyy"),
                _.RentAccountNumber,
                Type = _.TargetType.ToString(),
                Charge = _.ChargedAmount,
                Paid = _.PaidAmount,
                HBCont = _.HousingBenefitAmount,
                Balence = _.FinishBalance
            });

            byte[] result;
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-US"));
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                {
                    using var cw = new CsvWriter(sw, cc);
                    cw.WriteRecords(data);
                }
                result = ms.ToArray();
            }
            return result;
        }
    }
}
