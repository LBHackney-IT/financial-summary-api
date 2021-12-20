using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class ExportStatementRequest
    {
        /// <summary>
        /// The guid of a tenancy/property
        /// </summary>
        /// <example>
        /// 94b02545-0233-4640-98dd-b2900423c0a5
        /// </example>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }

        /// <summary>
        /// Type of the target Id. Values: [0 - Estate, 1 - Block, 2 - Core, 3 - Property] 
        /// </summary>
        /// <example>
        /// 0 
        /// </example>
        public TargetType? TargetType { get; set; }
        /// <summary>
        /// Type of statement quaterly/yearly
        /// </summary>
        /// <example>
        /// Quaterly
        /// </example>
        public TypeOfStatement TypeOfStatement { get; set; }
        /// <summary>
        /// Type of file (csv or pdf)
        /// </summary>
        /// <example>
        /// csv
        /// </example>
        public string FileType { get; set; }
    }
}
