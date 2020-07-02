using Moq;
using System.Threading.Tasks;
using WalletManager.DataAccess.Interfaces.Repositories;
using WalletManager.Domain.Entities;
using WalletManager.Tests.List;

namespace WalletManager.Tests.Mocks
{
    public static class PlayerRepositoryMock
    {
        public static Mock<IPlayerRepository> Get()
        {
            var players = PlayersList.Get();

            var mock = new Mock<IPlayerRepository>();

            mock.Setup(repo => repo.GetPlayer(players[0].Id)).ReturnsAsync(players[0]);
            mock.Setup(repo => repo.GetPlayer(players[0].Email)).ReturnsAsync(players[0]);
            mock.Setup(repo => repo.GetPlayer(players[1].Id)).ReturnsAsync(players[1]);
            mock.Setup(repo => repo.GetPlayer(players[1].Email)).ReturnsAsync(players[1]);

            mock.Setup(repo => repo.CreatePlayer(It.IsAny<Player>())).Returns<Player>(x => Task.FromResult(x));
            mock.Setup(repo => repo.UpdatePlayer(It.IsAny<Player>())).Returns<Player>(x => Task.FromResult(x));
            mock.Setup(repo => repo.AddTransaction(It.IsAny<Transaction>())).Returns<Transaction>(x => Task.FromResult(x));

            return mock;
        }
    }
}
