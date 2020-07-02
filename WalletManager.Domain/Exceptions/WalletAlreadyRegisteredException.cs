using System;

namespace WalletManager.Domain.Exceptions
{
    public class WalletAlreadyRegisteredException : Exception
    {
        public WalletAlreadyRegisteredException() : base("Wallet for this player is already registered.") { }
    }
}
