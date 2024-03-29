﻿using BunkerGame.GameTypes.CharacterTypes;
using System.Text.Json.Serialization;

namespace BunkerGameComponents.Domain.CharacterComponents
{
    public class CharacterHobby : CharacterComponentBase
    {
        public CharacterHobby(ComponentId id) : base(id)
        {
            HobbyType = HobbyType.Useless;
        }
        public void UpdateHobbyType(HobbyType hobbyType)
        {
            HobbyType = hobbyType;
        }
        [JsonInclude]
        public HobbyType HobbyType { get; set; }
    }
}
