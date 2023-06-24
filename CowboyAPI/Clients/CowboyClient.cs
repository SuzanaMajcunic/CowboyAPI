using CowboyAPI.Models;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace CowboyAPI.Clients
{
    public class CowboyClient
    {
        private readonly AppSettings _appSettings;

        public CowboyClient(IOptions<AppSettings> options)
        {
            _appSettings = options.Value;
        }

        public async Task<Cowboy?> GetCowboy()
        {
            Cowboy? result = null;

            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(_appSettings.DataProviderAPI);

                if(response?.IsSuccessStatusCode ?? false)
                {
                    var resultString = await response.Content.ReadAsStringAsync();

                    result = Newtonsoft.Json.JsonConvert.DeserializeObject<Cowboy>(resultString);
                }
            }

            return result;
        }
    }
}
