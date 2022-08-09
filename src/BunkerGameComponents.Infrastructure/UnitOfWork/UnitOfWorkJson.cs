using BunkerGameComponents.Domain;
using BunkerGameComponents.Infrastructure.Database.GameComponentContext;

namespace BunkerGameComponents.Infrastructure.UnitOfWork
{
    public class UnitOfWorkJson : IUnitOfWork
    {
        private readonly GameComponentJsonContext context;

        public UnitOfWorkJson(GameComponentJsonContext context)
        {
            this.context = context;
        }
        public Task Save(CancellationToken cancellationToken)
        {
            context.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
