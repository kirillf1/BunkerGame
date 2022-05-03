using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class CharactersExchangedNotification : INotification
    {
        public CharactersExchangedNotification(Character characterFist, Character characterSecond)
        {
            CharacterFist = characterFist;
            CharacterSecond = characterSecond;
        }
        public Character CharacterFist { get; }
        public Character CharacterSecond { get; }
    }
}
