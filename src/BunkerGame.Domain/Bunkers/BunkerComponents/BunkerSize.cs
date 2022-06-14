using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class BunkerSize : BunkerComponent
    {
        private BunkerSize()
        {
        }
        public BunkerSize(double value)
        {
            Value = value;
        }

        public double Value { get; private set; }
        public override string ToString()
        {
            return $"Размер бункера {Value}";
        }
    }
}
