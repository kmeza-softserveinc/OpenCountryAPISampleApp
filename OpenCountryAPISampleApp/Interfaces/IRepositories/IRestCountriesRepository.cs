namespace OpenCountryAPISampleApp.Interfaces.IRepositories
{

    public interface IRestCountriesRepository
    {
        Task<IEnumerable<dynamic>> GetAllCountriesAsync();

    }

}