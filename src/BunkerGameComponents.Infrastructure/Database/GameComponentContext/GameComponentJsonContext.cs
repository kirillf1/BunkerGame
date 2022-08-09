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
        JsonSerializerOptions options;
        private Dictionary<string, object> components;
        public GameComponentJsonContext(string jsonPath)
        {
            options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            path = jsonPath;
            components = new();
        }
        public List<T> Set<T>() where T : IGameComponent
        {
            var docName = Path.Combine(path, $"{typeof(T).Name}.json");
            if (TryGetComponentsInMemory<T>(docName, out var gameComponents))
            {
                return gameComponents;
            }
            if (File.Exists(docName))
            {
                using var stream = File.Open(docName, FileMode.Open, FileAccess.Read, FileShare.Read);
                gameComponents = JsonSerializer.Deserialize<List<T>>(stream, options)!;
                components[docName] = gameComponents;
                return gameComponents;
            }
            else
            {
                var writer = new StreamWriter(File.Create(docName));
                writer.Write(JsonSerializer.Serialize(new List<T>()));
                writer.Close();
                return Set<T>();
            }
        }
        public bool SaveChanges()
        {
            foreach (var gameComponents in components)
            {
                var docName = gameComponents.Key;
                File.WriteAllText(docName, JsonSerializer.Serialize(gameComponents.Value, options), Encoding.UTF8);
            }
            return true;
        }
        private bool TryGetComponentsInMemory<T>(string key, out List<T> gameComponents) where T : IGameComponent
        {
            if (components.TryGetValue(key, out var collection) && collection is List<T> newComponents)
            {
                gameComponents = newComponents;
                return true;
            }
            gameComponents = null;
            return false;
        }
    }
}
