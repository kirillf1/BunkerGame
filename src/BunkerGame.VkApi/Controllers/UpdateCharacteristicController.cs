using BunkerGame.Application.Characters.ChangeCharacteristic;
using BunkerGame.Domain.Characters;
using BunkerGame.Domain.Characters.CharacterComponents;
using BunkerGame.Domain.GameSessions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunkerGame.VkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UpdateCharacteristicController : ControllerBase
    {
        //private readonly IRequestHandler<ChangeCharacteristicCommand, Character> requestHandler;
        //private readonly IGameSessionRepository gameSessionRepository;

        //public UpdateCharacteristicController(IRequestHandler<ChangeCharacteristicCommand, Character> requestHandler, IGameSessionRepository gameSessionRepository)
        //{
        //    this.requestHandler = requestHandler;
        //    this.gameSessionRepository = gameSessionRepository;
        //}
        //[HttpGet("{id}")]
        //public async Task<bool> Update(long id)
        //{
        //    var gameSession = await gameSessionRepository.GetGameSession(id);
        //    var character = gameSession.Characters.Find(c => c.IsAlive);
        //    if (character == null)
        //        return false;
        //    await requestHandler.Handle(new ChangeCharacteristicCommand(character.Id,typeof(Profession)), default);
        //    return true;
        //}
    }
}
