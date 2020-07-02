using System;

namespace WalletManager.Domain.Entities
{
    public class Wallet
    {
        public Guid Id { get; set; }
        public decimal Balance { get; set; }
    }
}
