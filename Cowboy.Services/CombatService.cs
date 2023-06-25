using Cowboy.Repository;
using Cowboy.Repository.Entities;
using Cowboy.Services.Interfaces;

namespace Cowboy.Services
{
    public class CombatService : ICombatService
    {
        private readonly ICowboyRepository _repository;
        private readonly IFirearmService _firearmService;

        public CombatService(ICowboyRepository repository, IFirearmService firearmService)
        {
            _repository = repository;
            _firearmService = firearmService;
        }
        public async Task<ServiceResponse<string>> CombatAsync(int cowboyOneId, int cowboyTwoId)
        {
            var cowboyOne = await _repository.GetCowboyAsync(cowboyOneId);
            if (cowboyOne == null) return new ServiceResponse<string>($"Cowboy(ID:{cowboyOneId}) not found.", false);

            var cowboyTwo = await _repository.GetCowboyAsync(cowboyTwoId);
            if (cowboyTwo == null) return new ServiceResponse<string>($"Cowboy(ID:{cowboyTwoId}) not found.", false);

            if (cowboyOne.Firearm.BulletsLeft <= 0) return new ServiceResponse<string>($"Cowboy(ID:{cowboyOneId}) has no bullets left. Reload the gun.", false);
            if (cowboyTwo.Firearm.BulletsLeft <= 0) return new ServiceResponse<string>($"Cowboy(ID:{cowboyTwoId}) has no bullets left. Reload the gun.", false);

            var resultString = Combat(cowboyOne, cowboyTwo);
            if(!string.IsNullOrWhiteSpace(resultString)) return new ServiceResponse<string>(resultString);
            else return new ServiceResponse<string>($"Something went wrong in combat...", false);
        }

        #region => Private helpers
        private string Combat(CowboyEntity cowboyOne, CowboyEntity cowboyTwo)
        {
            string result;
            var randomGenerator = new Random();
            var successRateFirstCowboy = 0;
            var successRateSecondCowboy = 0;

            while (cowboyOne.Firearm.BulletsLeft > 0 && cowboyTwo.Firearm.BulletsLeft > 0)
            {
                var shootSuccessFirst = randomGenerator.Next(100) < 50;
                if (shootSuccessFirst)
                {
                    successRateFirstCowboy++;
                    cowboyOne.Firearm.BulletsLeft--;
                    _firearmService.ShootTheGunAsync(cowboyOne.Id);
                }

                var shootSuccessSecond = randomGenerator.Next(100) < 50;
                if (shootSuccessSecond)
                {
                    successRateSecondCowboy++;
                    cowboyTwo.Firearm.BulletsLeft--;
                    _firearmService.ShootTheGunAsync(cowboyTwo.Id);
                }
            }

            if (successRateFirstCowboy > successRateSecondCowboy) result = $"The first cowboy won with ratio {successRateFirstCowboy}:{successRateSecondCowboy}.";
            else if (successRateFirstCowboy < successRateSecondCowboy) result = $"The second cowboy won with ratio {successRateSecondCowboy}:{successRateFirstCowboy}.";
            else result = "The result is tied. Nobody won.";

            return result;
        }
        #endregion
    }
}
