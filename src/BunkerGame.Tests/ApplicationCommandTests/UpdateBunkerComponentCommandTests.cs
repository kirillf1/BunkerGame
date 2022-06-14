using BunkerGame.Application.Bunkers.ChangeBunkerComponent;
using BunkerGame.Application.Bunkers.ChangeBunkerComponent.ComponentHandlers;
using BunkerGame.Domain.Bunkers.BunkerComponents;
using BunkerGame.Domain.GameSessions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BunkerGame.Tests.ApplicationCommandTests
{
    public class UpdateBunkerComponentCommandTests
    {
        [Fact]
        public async void ExecuteChangeBunkerComponentCommand_ShouldUpdateBunkerComponent()
        {
            var gameSession = GameSessionFactory.CreateGameSession();
            var bunkerComponentRepository = new Mock<IBunkerComponentRepository<BunkerWall>>();
            var newComponent = new BunkerWall(10, "test");
            bunkerComponentRepository.Setup(b => b.GetBunkerComponent(It.IsAny<bool>(), It.IsAny<Expression<Func<BunkerWall, bool>>>())).ReturnsAsync(newComponent);
            var gameSessionRepository = new Mock<IGameSessionRepository>();
            gameSessionRepository.Setup(g => g.GetGameSessionWithBunker(It.IsAny<long>())).ReturnsAsync(gameSession);
            var mediator = new Mock<IMediator>().Object;

            var commandHandler = new ChangeBunkerComponentCommandHandler<BunkerWall>(bunkerComponentRepository.Object,gameSessionRepository.Object,mediator);
            var res = await commandHandler.Handle(new ChangeBunkerComponentCommand<BunkerWall>(gameSession.Id, newComponent.Id),default);

            Assert.Equal(gameSession.Bunker.BunkerWall, res);

        }
    }
}
