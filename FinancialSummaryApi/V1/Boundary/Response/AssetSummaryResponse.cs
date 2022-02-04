using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class AssetSummaryResponse
    {
        /// <summary>
        /// Id of the created model
        /// </summary>
        /// <example>
        /// 49b8e9a1-c58f-4d48-820f-5715c0e07424
        /// </example>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the appropriate tenure if TargetType = [0]Estate or [1]Block or [2]Core or [3]Property
        /// </summary>
        /// <example>
        /// ada361e1-9e93-4cbb-b534-da97a3f5f9f9
        /// </example>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Values: [Estate, Block, Core]
        /// </summary>
        /// <example>
        /// Estate
        /// </example>
        public TargetType TargetType { get; set; }

        /// <example>
        /// Estate 1
        /// </example>
        public string AssetName { get; set; }

        /// <example>
        /// 105.55
        /// </example>
        public decimal TotalDwellingRent { get; set; }

        /// <example>
        /// 25.3
        /// </example>
        public decimal TotalNonDwellingRent { get; set; }

        /// <example>
        /// 94.85
        /// </example>
        public decimal TotalServiceCharges { get; set; }

        /// <example>
        /// 225.7
        /// </example>
        public decimal TotalRentalServiceCharge { get; set; }

        /// <summary>
        /// 125.2
        /// </summary>
        public decimal TotalIncome { get; set; }

        /// <summary>
        /// 675.62
        /// </summary>
        public decimal TotalExpenditure { get; set; }

        /// <summary>
        /// Date and time when summary was calculated and saved
        /// </summary>
        /// <example>
        /// 2021-06-25T13:19:47.993Z
        /// </example>
        public DateTime SubmitDate { get; set; }

        /// <summary>
        /// Summary Year for Estimate 
        /// </summary>
        public short SummaryYear { get; set; }

        /// <summary>
        /// Total Leaseholder Count 
        /// </summary>
        public int TotalLeaseholders { get; set; }

        /// <summary>
        /// Total Freehoders Count
        /// </summary>
        public int TotalFreeholders { get; set; }

        /// <summary>
        /// TOtal Dwellings Count
        /// </summary>
        public int TotalDwellings { get; set; }
    }
}
