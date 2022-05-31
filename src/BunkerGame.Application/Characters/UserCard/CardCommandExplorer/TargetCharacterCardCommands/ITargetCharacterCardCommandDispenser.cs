using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.TargetCharacterCardCommands
{
    internal interface ITargetCharacterCardCommandDispenser
    {
        public object? GiveCommandHandler(TargetCharacterCardArgs cardArgs);
    }
}
