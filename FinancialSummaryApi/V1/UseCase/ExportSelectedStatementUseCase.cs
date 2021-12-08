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
            if (request.StatementIdsToExport.Count > 0)
            {
                foreach (var item in request.StatementIdsToExport)
                {
                    var rId = await _financeSummaryGateway.GetStatementByIdAsync(item, request.TargetId).ConfigureAwait(false);
                    if (rId != null)
                        response.Add(rId);
                };
            }
            else
            {
                var startDate = request.StartDate ?? DateTime.UtcNow;
                var endDate = request.StartDate ?? DateTime.UtcNow;
                response = await _financeSummaryGateway.GetStatementListAsync(request.TargetId, startDate, endDate).ConfigureAwait(false);
            }


            var result = response != null ? FileGenerator.WriteManualCSVFile(response) : null;
            return result;
        }
    }
}
