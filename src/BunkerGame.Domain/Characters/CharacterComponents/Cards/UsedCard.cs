using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents.Cards
{
    public class UsedCard
    {
        private UsedCard()
        {

        }
        public UsedCard(bool cardUsed,int characterId,int cardId, byte cardNumber)
        {
            CardUsed = cardUsed;
            CharacterId = characterId;
            CardId = cardId;
            CardNumber = cardNumber;
        }

        public bool CardUsed { get; private set; }
        public int CharacterId { get;private set; }
        public int CardId { get; private set; }

        public byte CardNumber { get; private set; }
        public void ChangeCardUsage(bool cardUsed)
        {
            CardUsed = cardUsed;
        }
    }
}
