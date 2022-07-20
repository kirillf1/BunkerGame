using BunkerGame.Domain.Characters.Cards.CardCommandExplorer;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.Cards.CardCommandExplorer.NoneTargetCardCommands
{

    internal interface INoneTargetCardCommandDispenser
    {
        public void GiveCommandHandler(NoneTargetCardArgs cardArgs, CardCommandResultBuilder resultBuilder);
    }
}
