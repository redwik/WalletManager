using System;
using System.Threading.Tasks;
using WalletManager.Application.Interfaces.Services;
using WalletManager.DataAccess.Interfaces.Repositories;
using WalletManager.Domain.Entities;
using WalletManager.Domain.Enums;
using WalletManager.Domain.Exceptions;

namespace WalletManager.Application.Services
{
    public class PlayerService : IPlayerService
    {
        public readonly IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public async Task<bool> AddTransaction(Guid playerId, TransactionType type, decimal amount)
        {
            if (amount <= 0)
            {
                throw new InvalidAmountException(amount);
            }

            var player = await _playerRepository.GetPlayer(playerId);
            if (player == null)
            {
                throw new PlayerNotFoundException();
            }
            if (player.Wallet == null)
            {
                throw new WalletNotFound();
            }

            if (type == TransactionType.Stake && player.Wallet.Balance < amount)
            {
                return false;
            }

            var transaction = new Transaction
            {
                Type = type,
                Amount = amount,
                PlayerId = playerId
            };

            ProcessTransaction(transaction, player.Wallet);

            await _playerRepository.UpdatePlayer(player);
            await _playerRepository.AddTransaction(transaction);

            return true;
        }

        public async Task<Player> CreatePlayer(string name, string email)
        {
            var dbPlayer = await _playerRepository.GetPlayer(email);
            if (dbPlayer != null)
            {
                throw new PlayerAlreadyExistsException();
            }

            var player = new Player
            {
                Name = name,
                Email = email
            };

            return await _playerRepository.CreatePlayer(player);
        }

        public async Task<decimal> GetBalance(Guid playerId)
        {
            var player = await _playerRepository.GetPlayer(playerId);
            if (player == null)
            {
                throw new PlayerNotFoundException();
            }
            if (player.Wallet == null)
            {
                throw new WalletNotFound();
            }

            return player.Wallet.Balance;
        }

        public async Task<Player> RegisterWallet(Guid playerId)
        {
            var player = await _playerRepository.GetPlayer(playerId);
            if (player == null)
            {
                throw new PlayerNotFoundException();
            }
            if (player.Wallet != null)
            {
                throw new WalletAlreadyRegisteredException();
            }

            var wallet = new Wallet
            {
                Balance = 0
            };
            player.Wallet = wallet;

            return await _playerRepository.UpdatePlayer(player);
        }

        private void ProcessTransaction(Transaction transaction, Wallet wallet)
        {
            if (transaction.Type == TransactionType.Stake)
            {
                wallet.Balance -= transaction.Amount;
            }
            else
            {
                wallet.Balance += transaction.Amount;
            }
        }
    }
}
