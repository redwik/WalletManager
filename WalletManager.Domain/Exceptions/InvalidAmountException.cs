using System;

namespace WalletManager.Domain.Exceptions
{
    public class InvalidAmountException : Exception
    {
        public InvalidAmountException(decimal amount) : base($"Invalid transaction amount: {amount}") { }
    }
}
