using System;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class GetStatementListRequest
    {
        private const int DefaultPageSize = 12;
        /// <summary>
        /// The size of a page
        /// </summary>
        public int PageSize { get; set; } = DefaultPageSize;

        /// <summary>
        /// The number of a page
        /// </summary>
        public string PaginationToken { get; set; }

        /// <summary>
        /// The start date from when we want to filter statements. Required only if EndDate is provided
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date until when we want to filter statements. Required only if StartDate is provided
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
