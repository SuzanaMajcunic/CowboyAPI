using Cowboy.Repository.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace Cowboy.Repository
{
    public interface ICowboyRepository
    {
        Task<IEnumerable<CowboyEntity>> GetCowboysAsync();
        Task<CowboyEntity?> GetCowboyAsync(int id);
        Task<CowboyEntity> AddCowboyAsync(CowboyEntity cowboy);
        Task<CowboyEntity?> UpdateCowboyPatchAsync(int id, JsonPatchDocument cowboyDocument);
        Task<bool> DeleteCowboyAsync(int id);
    }
}
