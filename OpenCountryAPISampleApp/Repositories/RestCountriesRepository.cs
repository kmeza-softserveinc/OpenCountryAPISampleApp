using Newtonsoft.Json;
using OpenCountryAPISampleApp.Interfaces.IRepositories;

namespace OpenCountryAPISampleApp.Repositories
{
    public class RestCountriesRepository : IRestCountriesRepository
    {
        private readonly HttpClient _httpClient;

        public RestCountriesRepository(HttpClient httpClient, IConfiguration configuration)
        {
            var baseAddress = configuration["RESTCountriesAPI:BaseAddress"];
            httpClient.BaseAddress = new Uri(baseAddress);
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<dynamic>> GetAllCountriesAsync()
        {
            var response = await _httpClient.GetStringAsync("all");

            var result = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(response);
            if (result == null)
            {
                throw new HttpRequestException("Failed to retrieve countries or deserialize the response.");
            }
            return result;
        }


    }
}
