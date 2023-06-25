using AutoMapper;
using Cowboy.Repository;
using Cowboy.Services.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace Cowboy.Services
{
    public class FirearmService : IFirearmService
    {
        private readonly ICowboyRepository _repository;

        public FirearmService(ICowboyRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResponse<bool>> ReloadTheGunAsync(int cowboyId)
        {
            var cowboy = await _repository.GetCowboyAsync(cowboyId);
            if (cowboy == null) return new ServiceResponse<bool>($"Cowboy (ID:{cowboyId}) not found.", false);

            if(cowboy.Firearm.BulletsLeft < cowboy.Firearm.BulletsMaxNumber)
            {
                JsonPatchDocument jsonPatchDocument = new JsonPatchDocument();
                jsonPatchDocument.Replace("/Firearm/BulletsLeft", cowboy.Firearm.BulletsMaxNumber);
                var updated = await _repository.UpdateCowboyPatchAsync(cowboyId, jsonPatchDocument);

                if (updated != null) return new ServiceResponse<bool>(true);
                else return new ServiceResponse<bool>(false);
            }
            return new ServiceResponse<bool>(true);
        }

        public async Task<ServiceResponse<bool>> ShootTheGunAsync(int cowboyId)
        {
            var cowboy = await _repository.GetCowboyAsync(cowboyId);
            if (cowboy == null) return new ServiceResponse<bool>($"Cowboy (ID:{cowboyId}) not found.", false);

            if (cowboy.Firearm.BulletsLeft > 0)
            {
                var bulletsLeft = cowboy.Firearm.BulletsLeft - 1;

                JsonPatchDocument jsonPatchDocument = new JsonPatchDocument();
                jsonPatchDocument.Replace("/Firearm/BulletsLeft", bulletsLeft);
                var updated = await _repository.UpdateCowboyPatchAsync(cowboyId, jsonPatchDocument);

                if (updated != null) return new ServiceResponse<bool>(true);
                else return new ServiceResponse<bool>(false);
            }
            return new ServiceResponse<bool>($"Cowboy (ID:{cowboyId}) has no more bullets left.", false);
        }
    }
}
