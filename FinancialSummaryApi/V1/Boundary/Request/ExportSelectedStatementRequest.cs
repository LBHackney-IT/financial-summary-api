using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class ExportSelectedStatementRequest
    {
        [NonEmptyGuid]
        public Guid TargetId { get; set; }

        /// <summary>
        /// Type of targetype Estate,Block,Core,Property
        /// </summary>
        /// <example>
        /// Rent
        /// </example>
        public TargetType? TargetType { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<Guid> StatementIdsToExport { get; set; }
    }
}
