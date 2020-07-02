using WalletManager.Domain.Enums;

namespace WalletManager.Api.Models
{
    public class AddTransactionModel
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
    }
}
