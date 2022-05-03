using BunkerGame.Application.GameSessions.CreateGameSession;
using BunkerGame.Domain.GameSessions;
using BunkerGame.Domain.Players;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunkerGame.VkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameCreateController : ControllerBase
    {
        //private readonly IRequestHandler<CreateGameCommand, GameSession> requestHandler;
        //private readonly IPlayerRepository playerRepository;

        //public GameCreateController(IRequestHandler<CreateGameCommand, GameSession> requestHandler,IPlayerRepository playerRepository)
        //{
        //    this.requestHandler = requestHandler;
        //    this.playerRepository = playerRepository;
        //}
        //[HttpGet]
        //public async Task<long> Create()
        //{
        //    var gameSession = await requestHandler.Handle(new CreateGameCommand(true, 10, new Random().Next(0, 1000000).ToString(), null),default);
        //    var res = VkExtensions.GameComponentsConventer.ConvertGameSession(gameSession);
        //    return gameSession.Id;

        //}

    }
}
