using System;
using System.Threading.Tasks;
using WalletManager.Application.Services;
using WalletManager.Domain.Enums;
using WalletManager.Domain.Exceptions;
using WalletManager.Tests.List;
using WalletManager.Tests.Mocks;
using Xunit;

namespace WalletManager.Tests
{
    public class PlayerServiceTests
    {
        [Fact]
        public async Task CreatePlayer_Should_CreatePlayer()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);

            // Act
            var result = await service.CreatePlayer("Mark Markson", "markmarkson@foomail.com");

            // Assert
            Assert.Equal("Mark Markson", result.Name);
            Assert.Null(result.Wallet);
        }

        [Fact]
        public async Task CreatePlayer_Should_ThrowPlayerAlreadyExistsException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act & Assert
            var result = await Assert.ThrowsAsync<PlayerAlreadyExistsException>(() => service.CreatePlayer(players[0].Name, players[0].Email));

            // Assert
            Assert.Equal("Player with this email is already registered.", result.Message);
        }

        [Fact]
        public async Task GetBalance_Should_GetBalance()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act
            var result = await service.GetBalance(players[1].Id);

            // Assert
            Assert.Equal(players[1].Wallet.Balance, result);
        }

        [Fact]
        public async Task GetBalance_Should_ThrowPlayerNotFoundException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);

            // Act & Assert
            var result = await Assert.ThrowsAsync<PlayerNotFoundException>(() => service.GetBalance(Guid.NewGuid()));

            // Assert
            Assert.Equal("Player not found.", result.Message);
        }

        [Fact]
        public async Task GetBalance_Should_ThrowWalletNotFoundException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act & Assert
            var result = await Assert.ThrowsAsync<WalletNotFound>(() => service.GetBalance(players[0].Id));

            // Assert
            Assert.Equal("Wallet not found.", result.Message);
        }

        [Fact]
        public async Task RegisterWallet_Should_RegisterWallet()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act
            var result = await service.RegisterWallet(players[0].Id);

            // Assert
            Assert.NotNull(result.Wallet);
            Assert.Equal(0, result.Wallet.Balance);
        }

        [Fact]
        public async Task RegisterWallet_Should_ThrowPlayerNotFoundException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);

            // Act & Assert
            var result = await Assert.ThrowsAsync<PlayerNotFoundException>(() => service.RegisterWallet(Guid.NewGuid()));

            // Assert
            Assert.Equal("Player not found.", result.Message);
        }

        [Fact]
        public async Task RegisterWallet_Should_WalletAlreadyRegisteredException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act & Assert
            var result = await Assert.ThrowsAsync<WalletAlreadyRegisteredException>(() => service.RegisterWallet(players[1].Id));

            // Assert
            Assert.Equal("Wallet for this player is already registered.", result.Message);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-10)]
        public async Task AddTransaction_Should_ThrowInvalidAmountException(decimal amount)
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);

            // Act & Assert
            var result = await Assert.ThrowsAsync<InvalidAmountException>(() => service.AddTransaction(Guid.NewGuid(), TransactionType.Deposit, amount));

            // Assert
            Assert.Equal($"Invalid transaction amount: {amount}", result.Message);
        }

        [Fact]
        public async Task AddTransaction_Should_ThrowPlayerNotFoundException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);

            // Act & Assert
            var result = await Assert.ThrowsAsync<PlayerNotFoundException>(() => service.AddTransaction(Guid.NewGuid(), TransactionType.Deposit, 10));

            // Assert
            Assert.Equal("Player not found.", result.Message);
        }

        [Fact]
        public async Task RegisterWallet_Should_WalletNotFoundException()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act & Assert
            var result = await Assert.ThrowsAsync<WalletNotFound>(() => service.AddTransaction(players[0].Id, TransactionType.Deposit, 10));

            // Assert
            Assert.Equal("Wallet not found.", result.Message);
        }

        [Fact]
        public async Task RegisterWallet_Should_RejectTransaction()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act
            var result = await service.AddTransaction(players[1].Id, TransactionType.Stake, 100);

            // Assert
            Assert.False(result);
        }

        [Fact]
        public async Task RegisterWallet_Should_AcceptTransaction()
        {
            // Arrange
            var service = new PlayerService(PlayerRepositoryMock.Get().Object);
            var players = PlayersList.Get();

            // Act
            var result = await service.AddTransaction(players[1].Id, TransactionType.Stake, 10);

            // Assert
            Assert.True(result);
        }
    }
}
