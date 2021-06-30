using FinancialSummaryApi.V1.Domain;
using System;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class AssetSummaryResponse
    {
        // ToDo: add more summary annotations

        /// <summary>
        /// Id of the created model
        /// </summary>
        /// <example>
        /// 49b8e9a1-c58f-4d48-820f-5715c0e07424
        /// </example>
        public Guid Id { get; set; }

        /// <summary>
        /// Id of the appropriate tenure if TargetType = [0]Estate or [1]Block
        /// </summary>
        /// <example>
        /// ada361e1-9e93-4cbb-b534-da97a3f5f9f9
        /// </example>
        public Guid TargetId { get; set; }

        /// <summary>
        /// Values: [Estate, Block, RentGroup]
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

        /// <example>
        /// 2021-06-25T13:19:47.993Z
        /// </example>
        public DateTime SubmitDate { get; set; }
    }
}
