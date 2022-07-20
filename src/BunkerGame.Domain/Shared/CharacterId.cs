﻿namespace BunkerGame.Domain.Shared
{
    public class CharacterId : Value<CharacterId>
    {
        public Guid Value { get; }
        public CharacterId(Guid value)
        {
            if (value == default)
                throw new ArgumentNullException(nameof(value), "Character id cannot be empty");
            Value = value;
        }
    }
}
