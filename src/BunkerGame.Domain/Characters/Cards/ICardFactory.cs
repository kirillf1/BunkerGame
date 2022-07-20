using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.Cards
{
    public interface ICardFactory
    {
        public Task<Card> Create();
        public Task<IEnumerable<Card>> CreateCards(int count);
    }
}
