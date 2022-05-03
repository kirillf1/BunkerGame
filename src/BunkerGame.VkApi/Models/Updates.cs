using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
namespace BunkerGame.VkApi.Models
{

    [Serializable]
    public class Updates
    {

        /// <summary>
        /// Тип события
        /// </summary>
        [JsonProperty("type")]
#pragma warning disable CS8618
        public string Type { get; set; }
#pragma warning restore CS8618
        /// <summary>
        /// Объект, инициировавший событие
        /// Структура объекта зависит от типа уведомления
        /// </summary>
        [JsonProperty("object")]
#pragma warning disable CS8618 
        public JObject? Object { get; set; }
#pragma warning restore CS8618 
        /// <summary>
        /// ID сообщества, в котором произошло событие
        /// </summary>
        [JsonProperty("group_id")]
        public long GroupId { get; set; }
#pragma warning disable CS8618
        /// <summary>
        /// Секретный ключ. Передается с каждым уведомлением от сервера
        /// </summary>
        [JsonProperty("secret")]
        public string? Secret { get; set; }
    }
}
