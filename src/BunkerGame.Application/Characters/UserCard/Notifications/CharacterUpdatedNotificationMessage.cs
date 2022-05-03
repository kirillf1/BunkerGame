using BunkerGame.Domain.Characters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.Notifications
{
    public class CharacterUpdatedNotificationMessage : INotification
    {
        public CharacterUpdatedNotificationMessage(Character character)
        {
            Character = character;
        }
        public Character Character { get; }
    }
}
