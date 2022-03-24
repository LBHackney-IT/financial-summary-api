using System;
using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Request
{
    public class GetBatchStatementsRequest
    {
        public List<Guid> TargetIds { get; set; }
    }
}
