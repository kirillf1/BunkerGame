using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard
{
    public class UseCardCommand : IRequest
    {
        public UseCardCommand(int characterId, byte cardNumber,CardUseParams cardUseParams)
        {
            CharacterId = characterId;
            CardNumber = cardNumber;
            CardUseParams = cardUseParams;
        }
        public int CharacterId { get; }
        public byte CardNumber { get; }
        public CardUseParams CardUseParams { get; }
    }
}
