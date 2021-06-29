using System.Collections.Generic;

namespace FinancialSummaryApi.V1.Boundary.Response
{
    //TODO: Rename to represent to object you will be returning eg. ResidentInformationList
    public class RentGroupSummaryListResponse
    {
        public IEnumerable<RentGroupSummaryResponse> RentGroupSummaries { get; set; }
    }
}
