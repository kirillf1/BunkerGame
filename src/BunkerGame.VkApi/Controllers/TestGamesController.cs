using BunkerGame.Application.GameSessions.ResultCounters;
using BunkerGame.Domain.GameSessions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BunkerGame.VkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestGamesController : ControllerBase
    {
        //private readonly IGameSessionFactory gameSessionFactory;

        //public TestGamesController(IGameSessionFactory gameSessionFactory)
        //{
        //    this.gameSessionFactory = gameSessionFactory;
        //}
        //[HttpGet("{count}")]
        //public async Task<string> TestGames([FromRoute] int count)
        //{
        //    var reports = new List<ResultGameReport>();
        //    var text = String.Empty;
        //    for (int i = 0; i < count; i++)
        //    {
        //       var game = await gameSessionFactory.CreateGameSession(new GameSessionCreateOptions("test", 6) { CharactersAlive = true });
        //        game.Characters.Take(2).ToList().ForEach(c => c.ChangeLive(false));
        //       var report = await game.EndGame(new GameResultCounterEasy());
        //        text += $"игра №{i}{Environment.NewLine} {GameComponentsConventer.ConvertGameSession(game)} {Environment.NewLine} " 
        //             + string.Join($"Персонаж:{Environment.NewLine}",game.Characters.Where(c=>c.IsAlive).Select(c=>GameComponentsConventer.ConvertCharacter(c))) +
        //            $"{Environment.NewLine}Результат:{report.GameReport} {Environment.NewLine}" +
        //        $"Значение:{report.SurvivingValue}";
        //    }
        //    return text;
        //}
       
    }
}
