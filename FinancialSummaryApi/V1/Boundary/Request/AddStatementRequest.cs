using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    /// <summary>
    ///  Model to add new statement
    /// </summary>
    public class AddStatementRequest
    {
        /// <summary>
        /// Id of the approproate Estate, Block, Core or Property. Type should be specified in TargetType 
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
        [Required]
        public TargetType TargetType { get; set; }

        /// <example>
        /// 2021-04-27T23:00:00.0000000+00:00
        /// </example>
        [RequiredDateTime]
        public DateTime StatementPeriodEndDate { get; set; }

        /// <example>
        /// 123456789
        /// </example>
        [Required]
        public string RentAccountNumber { get; set; }

        /// <example>
        /// 12 Macron Court, E8 1ND
        /// </example>
        [Required]
        public string Address { get; set; }

        /// <summary>
        /// Type of the statement Id. Values: [0 - Tenant, 1 - Leasehold]
        /// </summary>
        /// <example>
        /// 0 
        /// </example>
        [Required]
        public StatementType StatementType { get; set; }

        /// <example>
        /// 200
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal ChargedAmount { get; set; }

        /// <example>
        /// 500
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal PaidAmount { get; set; }

        /// <example>
        /// 300
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal HousingBenefitAmount { get; set; }

        /// <example>
        /// 1000
        /// </example>
        public decimal StartBalance { get; set; }

        /// <example>
        /// 400
        /// </example>
        public decimal FinishBalance { get; set; }
    }
}
