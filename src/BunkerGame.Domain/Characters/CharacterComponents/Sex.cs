using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Sex : CharacterComponent
    {
        /// <summary>
        /// sex will be "мужчина"
        /// </summary>
        public Sex()
        {
            name = "мужчина";
        }
        public Sex(bool isMan)
        {
            name = isMan ? "мужчина" : "женщина";
        }
        public Sex(string name)
        {
            if (name == "мужчина" || name == "женщина")
                this.name = name;
            else
                throw new ArgumentException("Value must be мужчина or женщина");

        }
        private string name;
        public string Name
        {
            get => name; set
            {
                if (value == "мужчина" || value == "женщина")
                    name = value;
                else
                    throw new ArgumentException("Value must be мужчина or женщина");
            }
        }
        public override string ToString()
        {
            return $"Пол: {Name}";
        }
    }
}
