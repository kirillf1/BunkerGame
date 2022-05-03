using BunkerGame.Domain.Bunkers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.Catastrophes;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameResults;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.ExternalSurroundings;
using BunkerGame.Domain.Players;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Infrastructure.JsonContext
{
    public class BunkerGameJsonContext
    {
        public List<AdditionalInformation> AdditionalInformations { get; set; } = new List<AdditionalInformation>();
        public List<BunkerWall> BunkerWalls { get; set; } = new List<BunkerWall>();
        public List<BunkerEnviroment> BunkerEnviroments { get; set; } = new List<BunkerEnviroment>();
        public List<BunkerObject> BunkerObjects { get; set; } = new List<BunkerObject>();
        public List<Card> Cards { get; set; } = new List<Card>();
        public List<CharacterItem> CharacterItems { get; set; } = new List<CharacterItem>();
        public List<Health> Healths { get; set; } = new List<Health>();
        public List<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public List<ItemBunker> ItemBunkers { get; set; } = new List<ItemBunker>();
        public List<Phobia> Phobias { get; set; } = new List<Phobia>();
        public List<Profession> Professions { get; set; } = new List<Profession>();
        public List<GameSession> GameSessions { get; set; } = new List<GameSession>();
        public List<Bunker> Bunkers { get; set; } = new List<Bunker>();
        public List<Character> Characters { get; set; } = new List<Character>();
        public List<Trait> Traits { get; set; } = new List<Trait>();
        public List<Catastrophe> Catastrophes { get; set; } = new List<Catastrophe>();
        public List<Player> Players { get; set; } = new List<Player>();
        public List<GameResult> GameResults { get; set; } = new List<GameResult>();
        public List<ExternalSurrounding> ExternalSurroundings { get; set; } = new List<ExternalSurrounding>();
    }
}
