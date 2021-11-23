using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet.Contracts;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportStatementUseCase : IExportStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;
        private readonly IConverter _converter;

        public ExportStatementUseCase(IFinanceSummaryGateway financeSummaryGateway, IConverter converter)
        {
            _financeSummaryGateway = financeSummaryGateway;
            _converter = converter;
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


            var result = request?.FileType switch
            {
                "csv" => FileGenerator.WriteCSVFile(response, name, period),
                "pdf" => FileGenerator.WritePdfFile(response, name, period, _converter),
                _ => null
            };
            return result;
        }
    }
}
