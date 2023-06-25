using Cowboy.Repository.Models;
using Microsoft.AspNetCore.JsonPatch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
