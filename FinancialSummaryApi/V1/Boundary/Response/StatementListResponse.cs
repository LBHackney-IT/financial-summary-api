using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class StatementListResponse
    {
        public long Total { get; set; }
        public List<StatementResponse> Statements { get; set; }

        public StatementListResponse()
        {
            Statements = new List<StatementResponse>();
        }
    }
}
