using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Cowboy.Repository.Entities
{
    public class CowboyEntity
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 2)]
        public required string Name { get; set; }

        [StringLength(200, MinimumLength = 2)]
        public string? Address { get; set; }

        public int Height { get; set; }

        public string? Hair { get; set; }

        [JsonProperty("birth_data")]
        public string? BirthData { get; set; }

        public required FirearmEntity Firearm { get; set; }
    }
}
