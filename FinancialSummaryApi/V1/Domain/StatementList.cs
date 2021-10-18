using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Domain
{
    public class StatementList
    {
        public long Total { get; set; }
        public List<Statement> Statements { get; set; }

        public StatementList()
        {
            Statements = new List<Statement>();
        }
    }
}
