using CsvHelper;
using CsvHelper.Configuration;
using FinancialSummaryApi.V1.Boundary.Response;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Extensions;
using NodaMoney;
using Razor.Templating.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

            return null;
        }

        public static async Task<string> CreatePdfTemplate(List<Statement> response, List<string> lines)
        {
            var model = new ExportResponse();
            var data = new List<ExportTransactionResponse>();
            model.Header = lines[0];
            model.SubFooter = lines[1];
            model.SubFooter = lines[2];
            model.Footer = lines[3];
            model.BankAccountNumber = string.Join(",", response.Select(x => x.RentAccountNumber).Distinct().ToArray());
            model.Balance = Money.PoundSterling(response.LastOrDefault().FinishBalance).ToString();
            model.BalanceBroughtForward = Money.PoundSterling(response.FirstOrDefault().StartBalance).ToString();
            // model.StatementPeriod = period;
            foreach (var item in response)
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
            string template = await RazorTemplateEngine.RenderAsync("~/V1/Templates/PDFTemplate.cshtml", model).ConfigureAwait(false);

            return template.EncodeBase64();
        }

        public static byte[] WriteCSVFile(List<Statement> transactions, List<string> lines)
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
            var cc = new CsvConfiguration(new System.Globalization.CultureInfo("en-UK"));
            using (var ms = new MemoryStream())
            {
                using (var sw = new StreamWriter(stream: ms, encoding: new UTF8Encoding(true)))
                {
                    using var cw = new CsvWriter(sw, cc);
                    cw.WriteRecords(data);
                    cc.NewLine = Environment.NewLine;
                    foreach (var item in lines)
                    {
                        cw.WriteComment(item);
                    }
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
