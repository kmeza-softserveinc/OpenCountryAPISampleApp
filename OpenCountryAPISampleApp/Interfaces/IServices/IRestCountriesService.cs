namespace OpenCountryAPISampleApp.Interfaces.IServices
{
    public interface IRestCountriesService
    {
        Task<IEnumerable<dynamic>> GetAllCountriesAsync(int pagination = 0, string sort = "");

        Task<IEnumerable<dynamic>> GetCountriesByNameAsync(string name, int pagination = 0, string sort = "");

        Task<IEnumerable<dynamic>> GetCountriesByPopulationAsync(string population, int pagination = 0, string sort = "");

        Task<IEnumerable<dynamic>> GetCountriesByNameAndPopulationAsync(string name, string population, int pagination = 0, string sort = "");
    }
}
