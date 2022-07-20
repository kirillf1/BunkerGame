using BunkerGame.Domain.GameSessions.Bunkers;

namespace BunkerGame.Domain.GameSessions
{
    public class GameSession : AggregateRoot<GameSessionId>
    {
        public const byte MaxCharactersInGame = 12;
        public const byte MinCharactersInGame = 5;
        private GameSession() { }
        public GameSession(GameSessionId id, PlayerId creator) 
        {
            Id = id;
            CreatorId = creator;
            Catastrophe = Catastrophe.DefaultCatastrophe;
            Bunker = Bunker.DefaultBunker;
            Name = "unknown";
            characters = new();
            GameState = GameState.Preparation;
            Difficulty = Difficulty.Easy;
            FreePlaceSize = new FreeSeatsSize(3,0);
            externalSurroundings = new();
            CurrentMaxCharactersInGame = MinCharactersInGame;
            AddEvent(new Events.GameCreated(Id, CreatorId));
        }
        public byte CurrentMaxCharactersInGame { get; private set; }
        public string Name { get; private set; }
        public IReadOnlyCollection<ExternalSurrounding> ExternalSurroundings => externalSurroundings;
        private readonly List<ExternalSurrounding> externalSurroundings;
        public IReadOnlyCollection<CharacterGame> Characters => characters;
        private readonly List<CharacterGame> characters;
        public PlayerId CreatorId { get; }
        public Catastrophe Catastrophe { get; private set; }
        public Bunker Bunker { get; private set; }
        public GameState GameState { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public FreeSeatsSize FreePlaceSize { get; private set; }
        private int NotKickedCharactersCount => characters.Count(c => !c.IsKicked);

        public void AddCharacter(CharacterGame character)
        {
            if (GameState != GameState.Preparation)
                return;
            if (characters.Any(c => c.Id == character.Id))
                return;
            if (characters.Count > CurrentMaxCharactersInGame)
                return;
            var characterNumber = Characters.Count == 0 ? (byte)0 : Characters.Max(c => c.CharacterNumber);
            character.SetCharacterNumber(++characterNumber);
            characters.Add(character);
            AddEvent(new Events.CharacterAdded(base.Id, character.Id));
        }
        public void AddFreePlace()
        {
            if (GameState == GameState.Ended)
                return;
            UpdateFreePlaceSize(FreePlaceSize.Add());
        }
        public void RemoveFreePlace()
        {
            if (GameState == GameState.Ended)
                return;
            UpdateFreePlaceSize(FreePlaceSize.Subtract());
        }
        private void UpdateFreePlaceSize(FreeSeatsSize freePlaceSize)
        {
            FreePlaceSize = freePlaceSize;
            AddEvent(new Events.FreeSeatsChanged(Id, FreePlaceSize.FreeSeats));
            if (FreePlaceSize.FreeSeatsFilled(NotKickedCharactersCount))
                AddEvent(new Events.SeatsFilled(Id));
        }
        public void UpdateBunker(Bunker bunker)
        {
            if (GameState == GameState.Ended)
                return;
            Bunker = bunker;
            AddEvent(new Events.BunkerUpdated(Id, bunker));
            var newFreePlaceSize = new FreeSeatsSize(bunker.Size);
            if (newFreePlaceSize != FreePlaceSize)
                UpdateFreePlaceSize(FreePlaceSize);
        }
        public void UpdateCatastrophe(Catastrophe catastrophe)
        {
            Catastrophe = catastrophe;
            AddEvent(new Events.CatastropheChanged(Id, Catastrophe));
        }
        public void AddExternalSurrounding(ExternalSurrounding externalSurrounding)
        {
            externalSurroundings.Add(externalSurrounding);
            AddEvent(new Events.ExternalSurroundigAdded(Id, externalSurrounding));
        }
        public void ChangeDifficulty(Difficulty difficulty)
        {
            Difficulty = difficulty;
            AddEvent(new Events.DifficultyChanged(Id, Difficulty));
        }
        public void StartGame()
        {
            if (!CanStartGame())
                return;
            GameState = GameState.Started;
            AddEvent(new Events.GameStarted(this));
        }
        public void KickCharacter(CharacterId characterId)
        {
            if (GameState != GameState.Started)
                return;
            var character = Characters.FirstOrDefault(c => c.Id == characterId);
            if (character == null || character.IsKicked)
                return;
            character.Kick();
            AddEvent(new Events.CharacterKicked(Id, characterId));
            if (FreePlaceSize.FreeSeatsFilled(NotKickedCharactersCount))
                AddEvent(new Events.SeatsFilled(Id));
        }
        public void EndGame()
        {
            if (GameState != GameState.Started)
                return;
            GameState = GameState.Ended;
            AddEvent(new Events.GameEnded(Id));
        }
        private bool CanStartGame()
        {
            if (Characters.Count < MinCharactersInGame || Characters.Count > MaxCharactersInGame || GameState != GameState.Preparation)
                return false;
            return true;
        }
        public void ChangeName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException($"\"{nameof(name)}\" не может быть неопределенным или пустым.", nameof(name));
            }
            Name = name;
            AddEvent(new Events.NameChanged(Id, Name));
        }
        public void ChangeMaxCharactersInGame(byte count)
        {
            if (GameState != GameState.Preparation)
                return;
            if (count > MaxCharactersInGame || count < MinCharactersInGame)
                throw new ArgumentOutOfRangeException($"Characters must be less than {MaxCharactersInGame +1} or more then {MinCharactersInGame}. {nameof(GameSession)}");
            CurrentMaxCharactersInGame = count;
            AddEvent(new Events.MaxCharacterCountChanged(Id, CurrentMaxCharactersInGame));
        }
        public void Restart()
        {
            Catastrophe = Catastrophe.DefaultCatastrophe;
            Bunker = Bunker.DefaultBunker;
            characters.Clear();
            GameState = GameState.Preparation;
            FreePlaceSize = new FreeSeatsSize(3, 0);
            externalSurroundings.Clear();
            AddEvent(new Events.GameRestarted(Id));
        }
    }
    public enum GameState
    {
        Preparation,
        Started,
        Ended
    }
    public enum Difficulty
    {
        Easy = 0,
        Medium,
        Hard
    }
}
