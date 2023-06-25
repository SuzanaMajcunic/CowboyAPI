using AutoMapper;
using Cowboy.Repository;
using Cowboy.Repository.Entities;
using Cowboy.Repository.Models;
using Cowboy.Services.Clients;
using Microsoft.AspNetCore.JsonPatch;

namespace Cowboy.Services
{
    public class CowboyService : ICowboyService
    {
        private readonly ICowboyRepository _repository;
        private readonly IMapper _mapper;
        private ICowboyClient _client;

        public CowboyService(ICowboyRepository repository, ICowboyClient client, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _client = client;
        }

        public async Task<ServiceResponse<IEnumerable<CowboyModel>>> GetAllCowboysAsync()
        {
            var cowboys = await _repository.GetCowboysAsync();
            if (cowboys?.Any() ?? false)
            {
                var data = _mapper.Map<IEnumerable<CowboyModel>>(cowboys);
                return new ServiceResponse<IEnumerable<CowboyModel>>(data);
            }
            return new ServiceResponse<IEnumerable<CowboyModel>>();
        }

        public async Task<ServiceResponse<CowboyModel>> GetCowboyByIdAsync(int id)
        {
            var cowboy = await _repository.GetCowboyAsync(id);
            if (cowboy == null)
            {
                return new ServiceResponse<CowboyModel>($"Cowboy (ID:{id}) not found.", true);
            }
            return new ServiceResponse<CowboyModel>(_mapper.Map<CowboyModel>(cowboy));
        }

        public async Task<ServiceResponse<CowboyModel>> CreateCowboyAsync()
        {
            var newCowboy = await _client.GetCowboyAsync();
            if (newCowboy == null)
            {
                return new ServiceResponse<CowboyModel>($"External API error on creating a new cowboy record.", false);
            }

            GenerateFirearm(newCowboy);

            var result = await _repository.AddCowboyAsync(newCowboy);
            if (result == null)
            {
                return new ServiceResponse<CowboyModel>($"Error on Cowboy save.", false);
            }

            return new ServiceResponse<CowboyModel>(_mapper.Map<CowboyModel>(result));
        }

        public async Task<ServiceResponse<bool>> DeleteCowboyAsync(int id)
        {
            var success = await _repository.DeleteCowboyAsync(id);
            if (success)
            {
                return new ServiceResponse<bool> { Success = true };
            }
            else
            {
                return new ServiceResponse<bool>($"Cowboy (ID:{id}) not found.", false);
            }
        }

        public async Task<ServiceResponse<CowboyModel>> UpdateCowboyPatchAsync(int id, JsonPatchDocument cowboyDocument)
        {
            var cowboy = await _repository.UpdateCowboyPatchAsync(id, cowboyDocument);

            if(cowboy == null) return new ServiceResponse<CowboyModel>($"Cowboy (ID:{id}) not found.", false);

            return new ServiceResponse<CowboyModel>(_mapper.Map<CowboyModel>(cowboy));
        }

        #region => Private helpers
        private void GenerateFirearm(CowboyEntity cowboy)
        {
            var randomGenerator = new Random();
            int bulletsNumber = randomGenerator.Next(5, 20);
            string[] gunname = { "Revolver", "Pistol", "Uzi", "Shotgun", "Rifle", "Glock" };

            FirearmEntity firearm = new FirearmEntity()
            {
                Name = gunname[randomGenerator.Next(0, gunname.Length)],
                BulletsMaxNumber = bulletsNumber,
                BulletsLeft = bulletsNumber,
                Cowboy = cowboy
            };

            cowboy.Firearm = firearm;
        }
        #endregion
    }
}
