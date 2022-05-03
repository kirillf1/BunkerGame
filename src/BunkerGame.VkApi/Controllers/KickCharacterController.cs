using BunkerGame.Application.GameSessions.KickCharacter;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.GameSessions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunkerGame.VkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KickCharacterController : ControllerBase
    {
        //private readonly IRequestHandler<KickCharacterCommand, Character> requestHandler;
        //private readonly IGameSessionRepository gameSessionRepository;

        //public KickCharacterController(IRequestHandler<KickCharacterCommand, Character> requestHandler, IGameSessionRepository gameSessionRepository)
        //{
        //    this.requestHandler = requestHandler;
        //    this.gameSessionRepository = gameSessionRepository;
        //}
        //[HttpGet("{id}")]
        //public async Task<bool> Kick(long id)
        //{
        //    var gameSession = await gameSessionRepository.GetGameSession(id);
        //    var character = gameSession.Characters.Find(c => c.IsAlive);
        //    if (character == null)
        //        return false;
        //    await requestHandler.Handle(new KickCharacterCommand(gameSession.Id, character.Id),default);
        //    return true;
        //}
    }
}
