using Cowboy.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace Cowboy.Services
{
    public interface ICowboyService
    {
        Task<ServiceResponse<IEnumerable<CowboyModel>>> GetAllCowboysAsync();

        Task<ServiceResponse<CowboyModel>> GetCowboyByIdAsync(int id);

        Task<ServiceResponse<CowboyModel>> CreateCowboyAsync();

        Task<ServiceResponse<bool>> DeleteCowboyAsync(int id);

        Task<ServiceResponse<CowboyModel>> UpdateCowboyPatchAsync(int id, JsonPatchDocument cowboyDocument);
    }
}
