namespace Cowboy.Repository.Models
{
    public class FirearmModel
    {
        public required string Name { get; set; }
        public int BulletsMaxNumber { get; set; }
        public int BulletsLeft { get; set; }
    }
}
