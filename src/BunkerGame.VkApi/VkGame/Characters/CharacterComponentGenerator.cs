using BunkerGame.Domain.Characters.CharacterComponents;

namespace BunkerGame.VkApi.VkGame.Characters
{
    public class CharacterComponentGenerator
    {
        Random Random;

        private const byte _minAge = 18;
        private const byte _maxAge = 70;
        public CharacterComponentGenerator()
        {
            Random = new Random();
        }
        public Sex GenerateSex()
        {
            const byte _isManChancePercent = 50;
            return new Sex(Random.Next(0, 100) > _isManChancePercent);
        }
        public Age GenerateAge()
        {
            return new Age(Random.Next(_minAge, _maxAge));
        }
        public Age GenerateAge(byte professionExperience)
        {
            var randomAge = Random.Next(_minAge, _maxAge);
            randomAge += professionExperience;
            if (randomAge > _maxAge)
                randomAge = _maxAge;
            return new Age(randomAge);
        }
        public Childbearing GenerateChildbearing()
        {
            const byte childFreeChancePercent = 37;
            return new Childbearing(Random.Next(0, 100) > childFreeChancePercent);
        }
        public Size GenerateSize()
        {
            const byte heightMax = 200;
            const byte heightMin = 140;
            var randomHeight = Random.Next(heightMin, heightMax);
            var randomWidth = GenerateWeight(randomHeight);
            return new Size(randomHeight, randomWidth);
        }
        private int GenerateWeight(int height)
        {
            int minWeight;
            int maxWeight;
            switch (height)
            {
                case int h when h < 160:
                    maxWeight = 90;
                    minWeight = 40;
                    break;
                case int h when h > 160 && h < 190:
                    maxWeight = 130;
                    minWeight = 60;
                    break;
                default:
                    minWeight = 70;
                    maxWeight = 170;
                    break;
            }
            return Random.Next(minWeight, maxWeight);
        }
    }
}
