using System;
using System.Threading.Tasks;
using WalletManager.Domain.Entities;
using WalletManager.Domain.Enums;

namespace WalletManager.DataAccess.Interfaces.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayer(Guid id);
        Task<Player> GetPlayer(string email);
        Task<Player> CreatePlayer(Player player);
        Task<Player> UpdatePlayer(Player player);
        Task<Transaction> AddTransaction(Transaction transaction);
    }
}
