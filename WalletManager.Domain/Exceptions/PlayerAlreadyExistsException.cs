using System;

namespace WalletManager.Domain.Exceptions
{
    public class PlayerAlreadyExistsException : Exception
    {
        public PlayerAlreadyExistsException() : base("Player with this email is already registered.") { }
    }
}
