using VkNet.Enums.SafetyEnums;
using VkNet.Model.Keyboard;

namespace BunkerGame.VkApi.VkGame.VkExtensions
{
    public static class VkKeyboardFactory
    {
        public static MessageKeyboard CreateStartGameButtons()
        {
            var keyboardBuilder = new KeyboardBuilder();
            keyboardBuilder.AddButton("Бот,создать новую игру", "игра", KeyboardButtonColor.Primary);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Бот, статистика", "статистика", KeyboardButtonColor.Default);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Бот, правила", "правила", KeyboardButtonColor.Positive);
            return keyboardBuilder.Build();
        }
        public static MessageKeyboard CreateConversationButtons(bool isGameStarted)
        {
            var keyboardBuilder = new KeyboardBuilder();

            if (isGameStarted)
            {
                keyboardBuilder.AddButton("Бот, показать катастрофу", "места", KeyboardButtonColor.Negative);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот, показать бункер", "места", KeyboardButtonColor.Negative);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот,количество мест", "места", KeyboardButtonColor.Negative);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот, исключить персонажей", "исключить", KeyboardButtonColor.Primary);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот, подвести итоги", "итоги", KeyboardButtonColor.Primary);
            }
            else
            {
                keyboardBuilder.AddButton("Бот, стартовать игру!", "старт", KeyboardButtonColor.Positive);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот,установить сложность", "сложность", KeyboardButtonColor.Negative);
                keyboardBuilder.AddLine();
                keyboardBuilder.AddButton("Бот,количество игроков", "количество", KeyboardButtonColor.Primary);
            }
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Бот, правила", "правила", KeyboardButtonColor.Positive);
            return keyboardBuilder.Build();
        }
        public static MessageKeyboard CreateOptionsButtoms(List<string> names, string pattern)
        {
            var keyboardBuilder = new KeyboardBuilder();
            for (int i = 0; i < names.Count; i++)
            {
                if (i % 2 > 0)
                    keyboardBuilder.AddLine();
                keyboardBuilder.AddButton(pattern + names[i], names[i], KeyboardButtonColor.Negative);
            }
            if (names.Count >= 1)
                keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Отмена", "отмена", KeyboardButtonColor.Primary);
            return keyboardBuilder.Build();
        }
        public static MessageKeyboard CreatePersonalButtons()
        {
            var keyboardBuilder = new KeyboardBuilder();
            keyboardBuilder.AddButton("Получить персонажа", "персонаж", KeyboardButtonColor.Positive);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Раскрыть характеристики", "характеристики", KeyboardButtonColor.Default);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("использовать карты", "карты", KeyboardButtonColor.Primary);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Выбрать игру", "игра", KeyboardButtonColor.Primary);
            keyboardBuilder.AddLine();
            keyboardBuilder.AddButton("Правила", "правила", KeyboardButtonColor.Primary);

            return keyboardBuilder.Build();
        }
        public static MessageKeyboard CreateUncoverCharacteristicButtoms()
        {
            KeyboardBuilder key = new();

            key.AddButton("!Хар: фобия", "фобия", KeyboardButtonColor.Primary);
            key.AddButton("!Хар: здоровье", "здоровье", KeyboardButtonColor.Positive);
            key.AddLine();
            key.AddButton("!Хар: багаж", "багаж", KeyboardButtonColor.Negative);
            key.AddButton("!Хар: деторождение", "деторождение", KeyboardButtonColor.Negative);
            key.AddButton("!Хар: возраст", "возраст", KeyboardButtonColor.Default);
            key.AddLine();
            key.AddButton("!Хар: доп.информация", "катаклизм", KeyboardButtonColor.Negative);
            key.AddButton("!Хар: черта характера", "персонаж", KeyboardButtonColor.Primary);
            key.AddLine();
            key.AddButton("!Хар: пол", "характеристики", KeyboardButtonColor.Primary);
            key.AddButton("!Хар: масса и рост", "характеристики", KeyboardButtonColor.Primary);
            key.AddButton("!Хар: хобби", "хобби");
            key.AddLine();
            key.AddButton("Отмена", "игра", KeyboardButtonColor.Primary);
            return key.Build();
        }
    }
}
