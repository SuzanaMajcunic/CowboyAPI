using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Repository.Entities
{
    public class CowboyEntity
    {
        public int Id { get; set; }

        [StringLength(200, MinimumLength = 2)]
        public required string Name { get; set; }
        public string? Address { get; set; }
        public int Height { get; set; }
        public string? Hair { get; set; }

        [JsonProperty("birth_data")]
        public string? BirthData { get; set; }

        public required FirearmEntity Firearm { get; set; }
    }
}
