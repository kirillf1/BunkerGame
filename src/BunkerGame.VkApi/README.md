# BunkerGame.VkApi
Реализация игры настольной игры "Бункер" через бота в ВК. Нужно всего лишь добавить бота в беседу по [ссылке](https://vk.com/club191848682) или захостить самостоятельно (далее будет рассмотрено более подробно) и написать "Бот, создать новую игру". После чего бот выдаст команды-кнопки для дальнейшего взаимодествия
# Как это работает
1. Отправка сообщения боту
2. Через Callback API ВК отправляет POST request на сервер игры, где содержится лишь один контроллер, который получает сообщение в виде JSON события
```C#
       [HttpPost]
        public IActionResult Callback([FromBody] Updates updates)
        {
            switch (updates.Type)
            {
                case "confirmation":
                    {
                        return Ok(configuration["Config:Confirmation"]);
                    }
                case "message_new":
                    {
                        // вызываем в отдельном потоке т.к. сервис иногда медленно выполняется и необходимо отправлять сразу OK 
                        // потому что вк может отправить запрос заново
                        Task.Run(async () =>
                        {
                            var message = Message.FromJson(new VkResponse(updates.Object));
                            await messageSender.SendMessage(message);
                        });
                        return Ok("OK");
                    }
                default:
                    return Ok("OK");
            }
```
3. CallbackController передает текст сообщения в IMessageService и тут идет поиск и создание VkCommand исходя из сообщения 
4. Выполнение команды. Если состояние игры изменилось, то передача событий объекта в IEventStore, который уведомит пользователей об изменении перед сохранением объекта в хранилище (более подробно о видах событий и классах [тут](/src/BunkerGame.Domain))
5. Отправка сообщения пользователям об изменении состояния игры через библиотеку [VkNet](https://vknet.github.io/vk/), а именно через класс VkApi
# Виды команд 
В классе [MessageService](/src/BunkerGame.VkApi/VkGame/VkGameServices/MessageService.cs) хранятся команды, которые доступны в беседе и в личных сообщениях с ботом (для взаимодействия с выданным персонажем)
1. Комманды беседы 
```C#
return new Dictionary<string, Type>
            {
                ["отмена"] = typeof(CancelConversationKeyboardCommand),
                ["новую игру"] = typeof(CreateGameSessionCommand),
                ["исключить"] = typeof(KickCommand),
                ["итоги"] = typeof(EndGameSessionCommand),
                ["стартовать игру"] = typeof(StartGameCommand),
                ["показать катастрофу"] = typeof(GetCurrentCatastrophe),
                ["показать бункер"] = typeof(GetCurrentBunker),
                ["статистика"] = typeof(StatisticsCommand),
                ["количество мест"] = typeof(GetAvailableSizeCommand),
                ["игроков:"] = typeof(ChangeCharactersCountCommand),
                ["количество игроков"] = typeof(GetAvailableCharactersCountCommand),
                ["сложность:"] = typeof(ChangeDifficultyCommand),
                ["установить сложность"] = typeof(GetAvailableDifficultiesCommand),
                ["правила"] = typeof(AnswerCommand)
            };
```
2. Команды в личных сообщениях
```C# 
 return new Dictionary<string, Type>
            {
                ["отмена"] = typeof(CancelPersonalKeyboardCommand),
                ["персонаж|персонажа"] = typeof(GetCharacterCommand),
                ["Раскрыть характеристики|!Хар:"] = typeof(UncoverCharacterComponentCommand),
                ["использовать карты"] = typeof(GetAvailableCardsCommand),
                [@"использовать карту №\d"] = typeof(TryUseCardCommand),
                ["карта на: "] = typeof(UseCardOnCharacterCommand),
                ["Выбрать игру"] = typeof(GetUserConversationsCommand),
                ["Беседа:"] = typeof(SetTargetConversationCommand),
                ["правила"] = typeof(AnswerCommand)
            };
```
Стоит отметить, что некоторые команды не будут доступны сразу. Например, получить персонажа можно лишь когда игра будет создана в беседе и т.д.
# Настройка игровых объектов
На данный момент все игровые объекты хранятся в JSON файлах. Описание и значение для игры можно настроить самостоятельно. Хранятся файлы [здесь](/src/BunkerGame.VkApi/Infrastructure/GameComponentsJson)
# Подведение итогов игры и сложность игры
После уведомления бота об необходимости окончить игру необходимо ввести команду "Бот, подвести итоги". Система начнет расчет значений, которые есть в игре исходя из сложности. Стоит отметить, что на данный момент расчет несбалансированный и может выдавать неточные результаты. Система расчета [тут](/src/BunkerGame.VkApi/VkGame/GameSessions/ResultCounters).

Все игровые объекты(катастрофа, характеристики персонажей, бункер) имеют свое значение, которое может быть положительным или отрицательным. Существует, на данный момент, 3 уровня сложности
1. Простой. Необходимо чтобы 2 персонажа имели возможность продолжить род и суммарное значение игровых объектов было больше 0.
2. Средний. Как и простая сложность, но теперь все игровые объекты могут иметь положительное или отрицательное психологическое воздействие. Если значение выживания или психологическое меньше 0, то проигрыш.
3. Тяжелый. Схож с средней, но теперь необходимо следить за показателем продовольствия.

# Установка
Если возникла необходимость установить бота на свой сервер, то необходимо выполнить следующие действия
1. Создать [мини-приложение](https://dev.vk.com/mini-apps/getting-started) и получить [ключ доступа](https://dev.vk.com/api/access-token/getting-started)
2. Добавить приложение в сообщество (вкладка приложения) и начать настройку бота (раздел настройки -> работа с API) вот тут [полное руководство](https://vk.com/dev/bots_docs)
3. Необходимо внести в [appsettings.json](/src/BunkerGame.VkApi/appsettings.json) изменения, а именно добавить "AccessToken"(ключ доступа) и "Confirmation"(то что должен возращать ваш сервер, а выдается в вкладке работа с API в сообществе)
4. Выбрать хостинг и отправить на сервер. Проект поддерживает докер, поэтому можно задеплоить через него. Я использую бесплатные сервера [heroku](https://heroku.com), а задеплоить можно по этому [руководству](https://medium.com/null-exception/deploy-net-core-app-to-heroku-a22a04f107c9). 
## Деплой на heroku через git
1. Регистрируемся на сайте и создаем приложение
2. Качаем [Heroku CLI](https://devcenter.heroku.com/articles/heroku-cli) и выполняем следующие команды
    + cd (путь до проекта)
    + heroku login
    + git init (если проект не содержит git)
    + heroku git:remote -a (название проекта в heroku)
    + heroku buildpacks:set https://github.com/jincod/dotnetcore-buildpack.git -a (название проекта в heroku)
    + git add .
    + git commit -am "deploy"
    + git push heroku master
3. Отправляем в сообществе -> настройки -> работа с API запрос на сервер по такому адресу https://**Имя_проекта_в_heroku**.herokuapp.com/api/callback
