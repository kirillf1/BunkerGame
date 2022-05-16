using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public class BunkerSize
    {
        private BunkerSize()
        {
        }
        public BunkerSize(double value)
        {
            Value = value;
        }

        public double Value { get; private set; }
    }
}
