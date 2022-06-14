using BunkerGame.Domain.Characters;
using MediatR;

namespace BunkerGame.Application.Characters.ExchangeCharacter
{
    public class CharactersExchangedNotification : INotification
    {
        public CharactersExchangedNotification(Character characterFirst, Character characterSecond)
        {
            CharacterFirst = characterFirst;
            CharacterSecond = characterSecond;
        }
        public Character CharacterFirst { get; }
        public Character CharacterSecond { get; }
    }
}
