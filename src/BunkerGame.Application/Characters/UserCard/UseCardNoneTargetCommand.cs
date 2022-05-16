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
    public class UseCardNoneTargetCommand : IRequest<Unit>
    {
        public UseCardNoneTargetCommand(int characterId, byte cardNumber)
        {
            CharacterId = characterId;
            CardNumber = cardNumber;
        }
        public int CharacterId { get; }
        public byte CardNumber { get; }
    }
}
