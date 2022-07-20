namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Sex : Value<Sex>
    {
        /// <summary>
        /// sex will be "мужчина"
        /// </summary>
        public Sex()
        {
            Name = "мужчина";
        }
        public Sex(bool isMan)
        {
            Name = isMan ? "мужчина" : "женщина";
        }
        public Sex(string name)
        {
            if (name == "мужчина" || name == "женщина")
                Name = name;
            else
                throw new ArgumentException("Name must be мужчина or женщина");
        }
        public string Name { get; }

        public override string ToString()
        {
            return $"Пол: {Name}";
        }
    }
}
