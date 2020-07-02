using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WalletManager.DataAccess.Interfaces.Repositories;
using WalletManager.DataAccess.Repositories;

namespace WalletManager.DataAccess
{
    public static class DataAccessDependencies
    {
        public static IServiceCollection AddWalletManagerDataAccess(this IServiceCollection services)
        {
            services.AddTransient<IPlayerRepository, PlayerRepository>();

            services.AddEntityFrameworkSqlite();
            services.AddDbContext<WalletManagerDbContext>(options => options.UseSqlite("Data Source=(LocalDB)"));

            return services;
        }
    }
}
