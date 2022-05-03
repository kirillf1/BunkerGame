using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Age : CharacterComponent
    {
        public Age()
        {
            Count = 16;
        }
        public Age(int age)
        {
            if (age <= 10)
                throw new ArgumentException("Age must be more than 10");
            Count = age;
        }
        public int Count { get; private set; }
    }
}
