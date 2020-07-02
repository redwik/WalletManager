using System;

namespace WalletManager.Domain.Exceptions
{
    public class WalletNotFound : Exception
    {
        public WalletNotFound() : base("Wallet not found.") { }
    }
}
