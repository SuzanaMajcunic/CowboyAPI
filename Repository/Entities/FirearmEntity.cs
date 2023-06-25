using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cowboy.Repository.Entities
{
    public class FirearmEntity
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BulletsMaxNumber { get; set; }
        public int BulletsLeft { get; set; }

        public CowboyEntity Cowboy { get; set; } = null!;
        public int CowboyId { get; set; }
    }
}
