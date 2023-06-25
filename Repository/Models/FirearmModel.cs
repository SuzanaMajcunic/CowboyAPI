namespace Cowboy.Repository.Models
{
    public class FirearmModel
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int BulletsMaxNumber { get; set; }
        public int BulletsLeft { get; set; }
    }
}
