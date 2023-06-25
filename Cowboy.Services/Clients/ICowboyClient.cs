using Cowboy.Repository.Entities;

namespace Cowboy.Services.Clients
{
    public interface ICowboyClient
    {
        Task<CowboyEntity?> GetCowboyAsync();
    }
}
