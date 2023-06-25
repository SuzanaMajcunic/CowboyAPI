using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Repository.Models
{
    public class CowboyModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public int Height { get; set; }
        public string? Hair { get; set; }
        public string? BirthData { get; set; }

        public required FirearmModel Firearm { get; set; }
    }
}
