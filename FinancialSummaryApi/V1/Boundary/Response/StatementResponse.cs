using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class StatementResponse
    {
        /// <summary>
        /// Id of the created model
        /// </summary>
        /// <example>
        /// 2f378d65-38d3-4fb4-877b-afeee666209e
        /// </example>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the appropriate tenure if TargetType = [0]Estate or [1]Block or [2]Core or [3]Property
        /// </summary>
        /// <example>
        /// 94b02545-0233-4640-98dd-b2900423c0a5
        /// </example>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Values: [Estate, Block, Core, Property]
        /// </summary>
        /// <example>
        /// 0
        /// </example>
        public TargetType TargetType { get; set; }

        /// <example>
        /// 2021-04-27T23:00:00.0000000+00:00
        /// </example>
        public DateTime StatementPeriodEndDate { get; set; }

        /// <example>
        /// 123456789
        /// </example>
        public string RentAccountNumber { get; set; }

        /// <example>
        /// 12 Macron Court, E8 1ND
        /// </example>
        public string Address { get; set; }

        /// <summary>
        /// Type of the statement Id. Values: [0 - Tenant, 1 - Leasehold]
        /// </summary>
        /// <example>
        /// 0
        /// </example>
        public StatementType StatementType { get; set; }

        /// <example>
        /// 200
        /// </example>
        public decimal ChargedAmount { get; set; }

        /// <example>
        /// 500
        /// </example>
        public decimal PaidAmount { get; set; }

        /// <example>
        /// 300
        /// </example>
        public decimal HousingBenefitAmount { get; set; }

        /// <example>
        /// 1000
        /// </example>
        public decimal StartBalance { get; set; }

        /// <example>
        /// 400
        /// </example>
        public decimal EndBalance { get; set; }
    }
}
