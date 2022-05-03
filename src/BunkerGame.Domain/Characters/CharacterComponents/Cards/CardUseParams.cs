using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents.Cards
{
    public class CardUseParams
    {
        public long? GameSessionId { get; set; }
        public int? TargetCharacterId { get; set; }

        public bool IsEmpty()
        {
            return GameSessionId == null && TargetCharacterId == null;
        }
    }
}
