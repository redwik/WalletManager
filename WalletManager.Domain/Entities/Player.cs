using System;

namespace WalletManager.Domain.Entities
{
    public class Player
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Wallet Wallet { get; set; }
    }
}
