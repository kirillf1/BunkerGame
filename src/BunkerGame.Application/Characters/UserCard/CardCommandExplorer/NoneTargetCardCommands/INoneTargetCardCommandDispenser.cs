using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Application.Characters.UserCard.CardCommandExplorer.NoneTargetCardCommands
{

    internal interface INoneTargetCardCommandDispenser
    {
        public object? GiveCommandHandler(NoneTargetCardArgs cardArgs);
    }
}
