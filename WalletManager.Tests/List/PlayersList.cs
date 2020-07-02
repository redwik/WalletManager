using System;
using System.Collections.Generic;
using WalletManager.Domain.Entities;

namespace WalletManager.Tests.List
{
    public static class PlayersList
    {
        public static List<Player> Get()
        {
            return new List<Player>
            {
                new Player
                {
                    Id = new Guid("2ed8d162-911c-4196-8604-5a18deb9f7b6"),
                    Name = "John Doe",
                    Email = "johndoe@foomail.com"
                },
                new Player
                {
                    Id = new Guid("a0951ea1-0a14-4240-8946-700bbe74d36a"),
                    Name = "Billy Kid",
                    Email = "billykid@foomail.com",
                    Wallet = new Wallet
                    {
                        Id = Guid.NewGuid(),
                        Balance = 50
                    }
                }
            };
        }
    }
}
