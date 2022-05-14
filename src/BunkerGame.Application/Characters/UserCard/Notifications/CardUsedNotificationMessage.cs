using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class CardUsedNotificationMessage : INotification
    {
        public CardUsedNotificationMessage(long gameSessionId, string cardDescription, Character usedCardCharacter, int? targetCharacterId = null)
        {
            GameSessionId = gameSessionId;
            CardDescription = cardDescription;
            UsedCardCharacter = usedCardCharacter;
            TargetCharacterId = targetCharacterId;
        }

        public long GameSessionId { get; }
        public string CardDescription { get; }
        public Character UsedCardCharacter { get; }
        public int? TargetCharacterId { get; }

    }
}
