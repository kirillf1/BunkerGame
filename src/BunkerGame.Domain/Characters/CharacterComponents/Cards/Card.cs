using BunkerGame.Domain.Characters.CharacterComponents.Cards;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Card : CharacterEntity
    {
#pragma warning disable CS8618 
        private Card(string description, bool isBalance) : base(description, isBalance) { }
#pragma warning restore CS8618

        public Card(string description,CardMethod cardMethod, bool isSpecial = false, bool isBalance = true) : base(description, isBalance)
        {
            IsSpecial = isSpecial;
            CardMethod = cardMethod;
        }
        public CardMethod CardMethod { get; set; }

        public bool IsSpecial { get; private set; }
        private List<Character> Characters { get;  } = new List<Character>();
        public override string ToString()
        {
            return Description;
        }
        public bool IsTargetCharacterCard()
        {
            var methodGroup = CardMethod.DefineDirectionGroup();

            if(methodGroup == DirectionGroup.Character)
            {
                if (CardMethod.MethodType == MethodType.Update || CardMethod.MethodType == MethodType.SpyYourself)
                    return false;
                return true;
            }
            return false;
            
        }
        public IEnumerable<CardActivateRequirement> GetActivateRequirements()
        {
            var requiremnts = new List<CardActivateRequirement>();
            var methodGroup = CardMethod.DefineDirectionGroup();
            if (CardMethod.MethodType == MethodType.None)
                return requiremnts;
            if (CardMethod.MethodType == MethodType.Update || CardMethod.MethodType == MethodType.SpyYourself)
            {
                switch (methodGroup)
                {
                    case DirectionGroup.Unknown:
                    case DirectionGroup.Character:
                        return requiremnts;
                }
            }
            

            switch (methodGroup)
            {
                case DirectionGroup.Unknown:
                    return requiremnts;
                case DirectionGroup.Character:
                    requiremnts.Add(CardActivateRequirement.CharacterId);
                    break;
                case DirectionGroup.Bunker:
                case DirectionGroup.Catastrophe:
                case DirectionGroup.GameSession:
                    requiremnts.Add(CardActivateRequirement.GameSessionId);
                    break;
            }
            return requiremnts;
        }

    }
}
