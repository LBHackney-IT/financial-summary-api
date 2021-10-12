using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    /// <summary>
    /// Model to add new asset summary
    /// </summary>
    public class AddAssetSummaryRequest
    {
        /// <summary>
        /// Id of the approproate Estate, Block or Core. Type should be specified in TargetType
        /// </summary>
        /// <example>
        /// 95d9da98-c937-49a2-8d15-8683dc478e3b
        /// </example>
        [NonEmptyGuid]
        public Guid TargetId { get; set; }
        /// <summary>
        /// Type of the target Id. Values: [0 - Estate, 1 - Block, 2 - Core]
        /// </summary>
        /// <example>
        /// 0
        /// </example>
        [Required]
        public TargetType TargetType { get; set; }
        /// <example>
        /// id libero excepturi
        /// </example>
        [Required]
        public string AssetName { get; set; }
        /// <example>
        /// 27.78
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalDwellingRent { get; set; }
        /// <example>
        /// 158.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalNonDwellingRent { get; set; }
        /// <example>
        /// 18.52
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalServiceCharges { get; set; }
        /// <example>
        /// 84.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalRentalServiceCharge { get; set; }
        /// <example>
        /// 184.56
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalIncome { get; set; }
        /// <example>
        /// 487.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalExpenditure { get; set; }
        /// <example>
        /// 2021-09-23T11:17:30.494+03:00
        /// </example>
        [RequiredDateTime]
        public DateTime SubmitDate { get; set; }
    }
}
