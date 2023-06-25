using Cowboy.Repository.Entities;

namespace Cowboy.Services.Interfaces
{
    public interface ICowboyClient
    {
        Task<CowboyEntity?> GetCowboyAsync();
    }
}
