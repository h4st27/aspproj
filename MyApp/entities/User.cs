using Newtonsoft.Json;

namespace MyApp.entities
{
    public class User
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("age")]
        public int Age { get; set; }
    }

}
