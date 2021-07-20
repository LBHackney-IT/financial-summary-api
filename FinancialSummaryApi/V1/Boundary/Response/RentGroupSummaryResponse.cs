using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class RentGroupSummaryResponse
    {
        /// <summary>
        /// Id of the created model
        /// </summary>
        /// <example>
        /// 5f799ec9-4ba4-49f5-8ed1-ba82184b149f
        /// </example>
        public Guid Id { get; set; }

        /// <summary>
        /// Values: [RentGroup]
        /// </summary>
        /// <example>
        /// RentGroup
        /// </example>
        public TargetType TargetType { get; set; }

        /// <example>
        /// Leaseholders
        /// </example>
        public string TargetDescription { get; set; }

        /// <example>
        /// Leaseholders
        /// </example>
        public string RentGroupName { get; set; }

        /// <example>
        /// 1502.95
        /// </example>
        public decimal TotalCharged { get; set; }

        /// <example>
        /// 645.25
        /// </example>
        public decimal ChargedYTD { get; set; }

        /// <example>
        /// 845.75
        /// </example>
        public decimal TotalPaid { get; set; }

        /// <example>
        /// 458.15
        /// </example>
        public decimal PaidYTD { get; set; }

        /// <example>
        /// 187.1
        /// </example>
        public decimal ArrearsYTD { get; set; }

        /// <example>
        /// -657.2
        /// </example>
        public decimal TotalBalance { get; set; }

        /// <summary>
        /// Date and time when summary was calculated and saved
        /// </summary>
        /// <example>
        /// 2021-06-25T13:19:47.993Z
        /// </example>
        public DateTime SubmitDate { get; set; }
    }
}
