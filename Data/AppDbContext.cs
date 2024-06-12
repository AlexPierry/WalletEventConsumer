using Microsoft.EntityFrameworkCore;
using WalletEventConsumer.Model;

namespace WalletEventConsumer.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BalanceModel> BalanceEvents { get; set; }
    }
}
