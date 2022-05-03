using BunkerGame.Domain.Characters;
using MediatR;

namespace BunkerGame.Application.Characters.GetCharacterCommand
{
    public class GetCharacterCommand : IRequest<Character>
    {
        public GetCharacterCommand(long gameSessionId,long playerId)
        {
            GameSessionId = gameSessionId;
            PlayerId = playerId;
        }

        public long GameSessionId { get; }
        public long PlayerId { get; }
    }
}
