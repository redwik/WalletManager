using System;

namespace WalletManager.Domain.Exceptions
{
    public class PlayerNotFoundException : Exception
    {
        public PlayerNotFoundException() : base("Player not found.") { }
    }
}
