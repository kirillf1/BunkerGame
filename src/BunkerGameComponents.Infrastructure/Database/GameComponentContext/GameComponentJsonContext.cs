using BunkerGameComponents.Domain;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

namespace BunkerGameComponents.Infrastructure.Database.GameComponentContext
{
    public class GameComponentJsonContext
    {
        private readonly string path;
        JsonSerializerOptions Options;
        public GameComponentJsonContext(string jsonPath)
        {
            Options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            path = jsonPath;
        }
        public List<T> Set<T>() where T : IGameComponent
        {
            var docName = Path.Combine(path, $"{typeof(T).Name}.json");

            if (File.Exists(docName))
            {
                using var stream = File.Open(docName, FileMode.Open, FileAccess.Read,FileShare.Read);
                return JsonSerializer.Deserialize<List<T>>(stream, Options)!;
            }
            else
            {
                var writer = new StreamWriter(File.Create(docName));
                writer.Write(JsonSerializer.Serialize(new List<T>()));
                writer.Close();
                return Set<T>();
            }
        }
        public bool SaveChanges<T>(List<T> entities) where T : IGameComponent
        {
            if (entities == null)
                return true;
            try
            {
                var docName = Path.Combine(path, $"{typeof(T).Name}.json");
                File.WriteAllText(docName, JsonSerializer.Serialize(entities, Options), Encoding.UTF8);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
