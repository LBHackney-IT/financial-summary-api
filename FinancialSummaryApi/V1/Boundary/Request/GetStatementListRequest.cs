using System;
using System.ComponentModel.DataAnnotations;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class GetStatementListRequest
    {
        private const int _defaultPageSize = 12;
        /// <summary>
        /// The size of a page
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageSize { get; set; } = _defaultPageSize;

        /// <summary>
        /// The number of a page
        /// </summary>
        [Range(1, int.MaxValue)]
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// The start date from when we want to filter statements
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// The end date until when we want to filter statements
        /// </summary>
        public DateTime EndDate { get; set; }
    }
}
