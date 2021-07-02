using FinancialSummaryApi.V1.Domain;
using FinancialSummaryApi.V1.Infrastructure.Entities;

namespace FinancialSummaryApi.V1.Factories
{
    public static class TenureInfoFactory
    {
        public static TenureInfo ToDomain(this TenureInfoDbEntity databaseEntity)
        {
            return databaseEntity == null ? null : new TenureInfo
            {
                Id = databaseEntity.Id,
                PaymentReference = databaseEntity.PaymentReference
            };
        }
    }
}
