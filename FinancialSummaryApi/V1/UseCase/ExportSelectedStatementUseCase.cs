using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportSelectedStatementUseCase : IExportSelectedStatementUseCase
    {
        private readonly IFinanceSummaryGateway _financeSummaryGateway;

        public ExportSelectedStatementUseCase(IFinanceSummaryGateway financeSummaryGateway)
        {
            _financeSummaryGateway = financeSummaryGateway;
        }

        public async Task<byte[]> ExecuteAsync(ExportSelectedStatementRequest request)
        {

            List<Statement> response = new List<Statement>();
            if (request.SelectedItems.Count > 0)
            {
                foreach (var item in request.SelectedItems)
                {
                    var rId = await _financeSummaryGateway.GetStatementByIdAsync(item).ConfigureAwait(false);
                    response.Add(rId);
                };
            }
            else
            {
                var startDate = request.StartDate.HasValue ? request.StartDate.Value : DateTime.UtcNow;
                var endDate = request.StartDate.HasValue ? request.StartDate.Value : DateTime.UtcNow;
                response = await _financeSummaryGateway.GetStatementListAsync(request.TargetId, startDate, endDate).ConfigureAwait(false);
            }


            var result = FileGenerator.WriteManualCSVFile(response);
            return result;
        }
    }
}
