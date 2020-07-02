using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using WalletManager.DataAccess.Interfaces.Repositories;
using WalletManager.Domain.Entities;

namespace WalletManager.DataAccess.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly WalletManagerDbContext _dbContext;

        public PlayerRepository(WalletManagerDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Transaction> AddTransaction(Transaction transaction)
        {
            await _dbContext.Transactions.AddAsync(transaction);
            await _dbContext.SaveChangesAsync();

            return transaction;
        }

        public async Task<Player> CreatePlayer(Player player)
        {            
            await _dbContext.Players.AddAsync(player);
            await _dbContext.SaveChangesAsync();

            return player;
        }

        public async Task<Player> GetPlayer(Guid id)
        {
            return await _dbContext.Players.Include(player => player.Wallet).FirstOrDefaultAsync(player => player.Id.Equals(id));
        }

        public async Task<Player> GetPlayer(string email)
        {
            return await _dbContext.Players.Include(player => player.Wallet).FirstOrDefaultAsync(player => player.Email.Equals(email));
        }

        public async Task<Player> UpdatePlayer(Player player)
        {
            _dbContext.Players.Update(player);
            await _dbContext.SaveChangesAsync();

            return player;
        }
    }
}
