using Microsoft.EntityFrameworkCore;
using WalletManager.Domain.Entities;

namespace WalletManager.DataAccess
{
    public class WalletManagerDbContext : DbContext
    {
        public WalletManagerDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Player> Players { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }
}
