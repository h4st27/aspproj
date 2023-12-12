using Newtonsoft.Json;

namespace MyApp.entities
{
    public class Book
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("author")]
        public string? Author { get; set; }
    }
}
