using System.Collections.Generic;
using FinancialSummaryApi.V1.Domain;

namespace FinancialSummaryApi.V1.Gateways
{
    public interface IExampleGateway
    {
        Entity GetEntityById(int id);

        List<Entity> GetAll();
    }
}
