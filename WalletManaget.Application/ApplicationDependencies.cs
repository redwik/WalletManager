using Microsoft.Extensions.DependencyInjection;
using WalletManager.Application.Interfaces.Services;
using WalletManager.Application.Services;

namespace WalletManager.Application
{
    public static class ApplicationDependencies
    {
        public static IServiceCollection AddWalletManagerApplication(this IServiceCollection services)
        {
            services.AddTransient<IPlayerService, PlayerService>();

            return services;
        }
    }
}
