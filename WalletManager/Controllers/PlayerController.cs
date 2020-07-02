using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WalletManager.Api.Models;
using WalletManager.Application.Interfaces.Services;

namespace WalletManager.Api.Controllers
{
    public class PlayerController : Controller
    {
        private readonly IPlayerService _playerService;

        public PlayerController(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        [HttpGet]
        [Route("[controller]/balance")]
        public async Task<IActionResult> GetBalance([FromQuery] Guid playerId)
        {
            var balance = await _playerService.GetBalance(playerId);
            return Ok(new BalanceModel { Balance = balance });
        }

        [HttpPost]
        [Route("[controller]/player")]
        public async Task<IActionResult> CreatePlayer([FromBody] CreatePlayerModel model)
        {
            var player = await _playerService.CreatePlayer(model.Name, model.Email);
            return Ok(player);
        }

        [HttpPost]
        [Route("[controller]/player/{id}/wallet")]
        public async Task<IActionResult> RegisterWallet(Guid id)
        {
            var player = await _playerService.RegisterWallet(id);
            return Ok(player);
        }

        [HttpPost]
        [Route("[controller]/player/{id}/transaction")]
        public async Task<IActionResult> AddTransaction(Guid id, [FromBody] AddTransactionModel transaction)
        {
            var result = await _playerService.AddTransaction(id, transaction.Type, transaction.Amount);
            return Ok(new AddTransactionResultModel { Success = result });
        }
    }
}
