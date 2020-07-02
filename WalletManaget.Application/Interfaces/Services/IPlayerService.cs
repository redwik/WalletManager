using System;
using System.Threading.Tasks;
using WalletManager.Domain.Entities;
using WalletManager.Domain.Enums;

namespace WalletManager.Application.Interfaces.Services
{
    public interface IPlayerService
    {
        Task<decimal> GetBalance(Guid playerId);
        Task<Player> CreatePlayer(string name, string email);
        Task<Player> RegisterWallet(Guid playerId);
        Task<bool> AddTransaction(Guid playerId, TransactionType type, decimal amount);
    }
}
