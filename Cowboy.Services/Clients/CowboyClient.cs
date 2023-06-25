using Cowboy.Repository.Entities;
using Cowboy.Repository.Models;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace Cowboy.Services.Clients
{
    public class CowboyClient : ICowboyClient
    {
        private readonly AppSettings _appSettings;

        public CowboyClient(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<CowboyEntity?> GetCowboyAsync()
        {
            CowboyEntity? result = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_appSettings.DataProviderAPI);

                if (response?.IsSuccessStatusCode ?? false)
                {
                    var resultString = await response.Content.ReadAsStringAsync();

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<CowboyEntity>(resultString);
                }
            }

            return result;
        }
    }
}
