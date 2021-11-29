using FinancialSummaryApi.V1.Infrastructure;
using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class AddRentGroupSummaryRequest
    {
        /// <example>
        /// magnam quibusdam nemo
        /// </example>
        public string TargetDescription { get; set; }
        /// <example>
        /// Garages
        /// </example>
        [Required]
        public string RentGroupName { get; set; }
        /// <example>
        /// 27.78
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalCharged { get; set; }
        /// <example>
        /// 158.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal ChargedYTD { get; set; }
        /// <example>
        /// 18.52
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalPaid { get; set; }
        /// <example>
        /// 84.5
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal PaidYTD { get; set; }
        /// <example>
        /// 184.56
        /// </example>
        [Range(0, (double) decimal.MaxValue)]
        public decimal TotalArrears { get; set; }
        /// <example>
        /// 487.5
        /// </example>
        [Required]
        public decimal TotalBalance { get; set; }
        /// <example>
        /// 2021-09-23T11:17:30.494+03:00
        /// </example>
        [RequiredDateTime]
        public DateTime SubmitDate { get; set; }
    }
}
