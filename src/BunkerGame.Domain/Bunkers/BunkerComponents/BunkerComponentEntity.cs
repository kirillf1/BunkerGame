using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Domain.Bunkers.BunkerComponents
{
    public abstract class BunkerComponentEntity : BunkerComponent
    {
        public BunkerComponentEntity(double value, string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException($"\"{nameof(description)}\" не может быть пустым или содержать только пробел.", nameof(description));
            }

            Value = value;
            Description = description;
        }
        public int Id { get; set; }

        public double Value { get;  set; }

        public string Description { get;  set; } = string.Empty;
        public virtual void UpdateValue(double value)
        {
            Value = value;
        }
        public void UpdateDescription(string newDesctiption)
        {
            if (string.IsNullOrEmpty(newDesctiption))
                throw new ArgumentException("Value must be not empty or null");
            Description = newDesctiption;
        }


    }
}
