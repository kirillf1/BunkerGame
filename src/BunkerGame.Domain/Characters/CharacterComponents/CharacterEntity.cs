using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public abstract class CharacterEntity : CharacterComponent
    {
        public int Id { get; set; }

        public string Description { get; set; }
        public double Value { get; set; }

        protected CharacterEntity(string description, bool isBalance, double value = 0)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"\"{nameof(description)}\" не может быть пустым или содержать только пробел.", nameof(description));
            }

            Description = description;
            IsBalance = isBalance;
            Value = value;
        }

        public bool IsBalance { get; set; } = true;
        public virtual void UpdateDescription(string newDesctiption)
        {
            if (string.IsNullOrEmpty(newDesctiption))
                throw new ArgumentException("Value must be not empty or not null");
            Description = newDesctiption;
        }
    }
}
