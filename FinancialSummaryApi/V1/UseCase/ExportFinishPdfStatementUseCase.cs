using FinancialSummaryApi.V1.Boundary.Request;
using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Gateways.Abstracts;
using FinancialSummaryApi.V1.UseCase.Helpers;
using FinancialSummaryApi.V1.UseCase.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialSummaryApi.V1.UseCase
{
    public class ExportFinishPdfStatementUseCase : IExportFinishPdfStatementUseCase
    {
        private readonly IFinanceSummaryGateway _gateway;

        private readonly string _header;
        private readonly string _subHeader;
        private readonly string _footer;
        private readonly string _subFooter;

        private const string Header = "Header";
        private const string SubHeader = "SubHeader";
        private const string Footer = "Footer";
        private const string SubFooter = "SubFooter";

        public ExportFinishPdfStatementUseCase(IFinanceSummaryGateway gateway, IConfiguration configuration)
        {
            _gateway = gateway;
            _header = configuration.GetValue<string>(Header);
            if (string.IsNullOrEmpty(_header))
            {
                throw new ArgumentException($"Configuration does not contain a report setting value for the parameter {Header}.");
            }
            _subHeader = configuration.GetValue<string>(Header);
            if (string.IsNullOrEmpty(_subHeader))
            {
                throw new ArgumentException($"Configuration does not contain a report setting value for the parameter {SubHeader}.");
            }
            _footer = configuration.GetValue<string>(Footer);
            if (string.IsNullOrEmpty(_footer))
            {
                throw new ArgumentException($"Configuration does not contain a report setting value for the parameter {Footer}.");
            }
            _subFooter = configuration.GetValue<string>(SubFooter);
            if (string.IsNullOrEmpty(_subFooter))
            {
                throw new ArgumentException($"Configuration does not contain a report setting value for the parameter {SubFooter}.");
            }
        }

        public async Task<Stream> ExecuteAsync(ExportStatementRequest request)
        {
            string name;
            string period;
            DateTime startDate;
            DateTime endDate;
            var lines = new List<string>();
            if (request.TypeOfStatement == TypeOfStatement.Quarterly)
            {
                startDate = DateTime.UtcNow.AddMonths(-3);
                endDate = DateTime.UtcNow;
                name = TypeOfStatement.Quarterly.ToString();

            }
            else
            {
                startDate = DateTime.UtcNow.AddMonths(-12);
                endDate = DateTime.UtcNow;
                name = TypeOfStatement.Yearly.ToString();
                period = $"{startDate:D} to {endDate:D}";
                lines.Add(_header.Replace("{itemId}", name));
                lines.Add(_subHeader.Replace("{itemId}", period));
            }

            var response = await _gateway.GetStatementListAsync(request.TargetId, startDate, endDate).ConfigureAwait(false);

            if (response.Any())
            {
                var accountBalance = $"{ response.LastOrDefault().FinishBalance}";
                var date = $"{DateTime.Today:D}";
                period = $"{startDate:D} to {endDate:D}";
                lines.Add(_header.Replace("{itemId}", name).ToUpper());
                lines.Add(_subHeader.Replace("{itemId}", period));
                lines.Add(_subFooter.Replace("{itemId}", date).Replace("{itemId_1}", accountBalance));
                lines.Add(_footer);
                return await FileGenerator.CreateFinishPdfTemplate(response, lines).ConfigureAwait(false);
            }

            return null;
        }
    }
}
