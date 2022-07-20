using MediatR;

namespace BunkerGame.Domain.Characters.Cards
{
    public class CardCommandResult : Value<CardCommandResult>
    {
        internal CardCommandResult(List<CardExecuteError> errors, IRequest? command)
        {
            Command = command;
            Errors = errors;
        }
        public IRequest GetCommand()
        {
            if (Command == null || !IsValid)
                throw new InvalidOperationException($"{nameof(CardCommandResult)} is invalid!");
            return Command;
        }
        private readonly IRequest? Command;
        public bool IsValid { get => Errors.Count == 0; }
        public List<CardExecuteError> Errors { get; }
    }
    public class CardCommandResultBuilder
    {
        public CardCommandResultBuilder()
        {
            Errors = new();
        }
        private List<CardExecuteError> Errors;
        private IRequest? Command;
        public CardCommandResultBuilder AddError(CardExecuteError error)
        {
            Errors.Add(error);
            return this;
        }
        public CardCommandResultBuilder AddCommand(IRequest command)
        {
            Command = command;
            return this;
        }
        public CardCommandResult Build()
        {
            if (Errors.Count == 0 && Command == null)
                throw new InvalidOperationException("CardCommandResult is valid but have't command");
            return new CardCommandResult(Errors, Command);
        }
    }
    public enum CardExecuteError
    {
        NoTargetCharacter,
        NoSuchCommand,
        InvalidComponentType,
        CardUsed
    }
}
