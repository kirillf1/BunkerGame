using BunkerGame.GameTypes.CharacterTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BunkerGameComponents.Domain.CharacterComponents.Cards
{
    public class Method
    {
        public Method()
        {
            MethodType = MethodType.None;
            MethodDirection = MethodDirection.None;
        }
        [JsonInclude]
        public MethodType MethodType { get; set; }
        [JsonInclude]
        public MethodDirection MethodDirection { get; set; }
        [JsonInclude]
        public ComponentId? ItemId { get; set; }


    }
}
