using Newtonsoft.Json;

namespace CowboyAPI.Models
{
    public class Cowboy
    {
        public int Id { get; set; }
        public required string Name  { get; set; }
        public string? Address { get; set; }
        public int Height { get; set; }
        public string? Hair { get; set; }

        [JsonProperty("birth_data")]
        public string? BirthData { get; set; }
    }
}
