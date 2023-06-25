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
