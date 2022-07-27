# О проекте
Проект является реализацией настольной игры "Бункер" со своими наработками. С правилами можно ознакомиться [здесь](GAME_RULES.md).</p>
Создавался проект для игры в компании от 5 до 12 человек удаленно без использования физической настольной игры. И с возможностью редактирования описаний событий, характеристик самостоятельно в зависимости от компании.

На данный момент играть можно только через Вконтаке, ссылка на бота [тут](https://vk.com/club191848682). Его нужно добавить в беседу и написать "Бот, начать новую игру".
</p> 
После выхода .NET MAUI ведется работа по созданию приложения на всех устройствах и сервера на SignalR для взаимодествия между игроками.

Некоторые проекты содержат свое описание, если интересно узнать подробности, то можете перейти по ссылкам указанным в структуре проекта.

# Структура
## [BunkerGame.Domain](/src/BunkerGame.Domain) и [BunkerGame.Framework](/src/BunkerGame.Framework)
Являются ядром проекта, где описываются ключевые классы для создания AgreggateRoots и событий игры (используется библиотека [MediatR](https://github.com/jbogard/MediatR)). BunkerGame.Domain содержит ограниченные контексты, необходимые для игры. А Unit тесты [здесь](/src/BunkerGame.Tests).

## [BunkerGame.VkApi](/src/BunkerGame.VkApi)
Обеспечивает функционирование бота в ВК и хранит состояние игр. Работает через [Callback API](https://vk.com/dev/callback_api) и содержит только один контроллер, который получает JSON события от ВК. Интеграционные тесты [здесь](/src/BunkerGame.VkApi.IntegrationTests). 

## [BunkerGame.GameTypes](/src/BunkerGame.GameTypes)
Содержатся типы для расчета итогов игры и возможных событий во время проживания в бункере.

## [BunkerGameComponents.Domain](/src/BunkerGameComponents.Domain) и [BunkerGameComponents.Infrastructure](/src/BunkerGameComponents.Infrastructure)
Содержастся "сырые" классы, необходимые для создания персонажей, бункеров, катаклизмов. Храниться могут как в базе данных(реализовано через EF Core) и в JSON файлах. В этих классах происходит процесс создания и редактирования игровых объектов.
