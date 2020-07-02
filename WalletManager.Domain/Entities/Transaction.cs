using System;
using WalletManager.Domain.Enums;

namespace WalletManager.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
    }
}
