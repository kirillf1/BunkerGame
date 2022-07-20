using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.GameSessions.Bunkers;

namespace BunkerGame.VkApi.VkGame.GameSessions.ResultCounters
{
    public record ResultCounterParams(IEnumerable<CharacterWithName> NotKickedCharacters, Bunker Bunker, IEnumerable<ExternalSurrounding> ExternalSurroundings, Catastrophe Catastrophe);

    public record CharacterWithName(string Name, Character Character);
}
