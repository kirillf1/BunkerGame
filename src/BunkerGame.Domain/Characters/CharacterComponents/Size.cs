namespace BunkerGame.Domain.Characters.CharacterComponents
{
    public class Size : Value<Size>
    {
        public Size()
        {
            Weight = 90;
            Height = 190;
        }
        public Size(double height, double weight)
        {
            Weight = weight;
            Height = height;
        }
        private double height;
        private double weight;
        public double Height
        {
            get => height;
            private set
            {
                height = value;
                if (height > 210)
                    height = 210;
                else if (height < 130)
                    height = 130;
            }
        }
        public double Weight
        {
            get => weight;
            private set
            {
                weight = value;
                if (weight > 230)
                    weight = 230;
                else if (weight < 35)
                    weight = 35;
            }
        }
        public string GetAvagereIndexBody()
        {
            double index = Weight / (Height / 100 * Height / 100);
            if (index >= 18 && index < 27)
                return "Нормальный вес";
            else if (index >= 27 && index < 30)
                return "Избыточный вес";
            else if (index >= 30 && index < 35)
                return "Ожирение I степени";
            else if (index >= 35 && index < 40)
                return "Ожирение II степени";
            else if (index >= 40)
                return "Ожирение III степени";
            else
                return "Недостаток массы тела";
        }
        public override string ToString()
        {
            return $"Телосложение: вес: {Weight} кг., рост: {Height} см. - {GetAvagereIndexBody()}";
        }
    }
}
