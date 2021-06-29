using FinancialSummaryApi.V1.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancialSummaryApi.V1.Infrastructure
{
    public class FinanceSummaryContext : DbContext
    {
        public FinanceSummaryContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<FinanceSummaryDbEntity> SummaryEntities { get; set; }
    }
}
