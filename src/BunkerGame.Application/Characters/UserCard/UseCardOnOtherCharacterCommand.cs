using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard
{
    public class UseCardOnOtherCharacterCommand : IRequest<Unit>
    {
        public UseCardOnOtherCharacterCommand(byte cardNumber,int useCardCharacterId, int targetCharacterId)
        {
            CardNumber = cardNumber;
            UseCardCharacterId = useCardCharacterId;
            TargetCharacterId = targetCharacterId;
        }

        public byte CardNumber { get; }
        public int UseCardCharacterId { get; }
        public int TargetCharacterId { get; }
    }
}
