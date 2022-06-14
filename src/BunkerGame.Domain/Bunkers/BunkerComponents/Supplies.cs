using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class Supplies: BunkerComponent
    {
        private Supplies()
        {

        }
        public Supplies(int suplliesYears)
        {
            SuplliesYears = suplliesYears;
        }

        public int SuplliesYears { get; private set; }
        public override string ToString()
        {
            return "Припасы на срок:" + SuplliesYears;
        }
    }
}
