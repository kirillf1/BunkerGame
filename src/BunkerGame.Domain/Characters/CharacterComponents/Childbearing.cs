using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Childbearing : CharacterComponent
    {
        public Childbearing(bool canGiveBirth)
        {
            CanGiveBirth = canGiveBirth;
        }
        public bool CanGiveBirth { get; set; }
        public override string ToString()
        {
            return "Деторождение: " + (CanGiveBirth ? "не childfree" : "childfree");
        }
    }
}
