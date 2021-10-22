using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class StatementListResponse
    {
        public int Total { get; set; }
        public List<StatementResponse> Statements { get; set; }

        public StatementListResponse()
        {
            Statements = new List<StatementResponse>();
        }
    }
}
