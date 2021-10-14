using FinancialSummaryApi.V1.Domain;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    public class GetStatementListResponse
    {
        public long Total { get; set; }
        public List<StatementResponse> Statements { get; set; }
        public GetStatementListResponse()
        {
            Statements = new List<StatementResponse>();
        }
    }
}
