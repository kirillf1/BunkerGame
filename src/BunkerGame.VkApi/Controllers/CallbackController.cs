using BunkerGame.VkApi.Models;
using BunkerGame.VkApi.VkGame.VkGameServices;
using Microsoft.AspNetCore.Mvc;
using VkNet.Model;
using VkNet.Utils;

namespace BunkerGame.VkApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CallbackController : ControllerBase
    {
        private readonly IConfiguration configuration;
        private readonly IMessageService messageSender;

        public CallbackController(IConfiguration configuration, IMessageService messageSender)
        {
            this.configuration = configuration;
            this.messageSender = messageSender;
        }

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

        }
    }
}
