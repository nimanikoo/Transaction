using Microsoft.EntityFrameworkCore;

namespace Transaction.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {}
        public DbSet<TransactionsModel> Transactions { get; set; }

    }
}
