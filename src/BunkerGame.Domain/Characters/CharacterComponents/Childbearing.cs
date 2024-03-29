﻿namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public record Childbearing : Value<Childbearing>
    {
        private Childbearing()
        {

        }
        public Childbearing(bool canGiveBirth)
        {
            CanGiveBirth = canGiveBirth;
        }
        public bool CanGiveBirth { get; }
        public override string ToString()
        {
            return "Деторождение: " + (CanGiveBirth ? "не childfree" : "childfree");
        }
    }
}
