using OpenCountryAPISampleApp.Repositories;
using System.Xml.Linq;
using OpenCountryAPISampleApp.Helpers;
using OpenCountryAPISampleApp.Interfaces.IRepositories;
using OpenCountryAPISampleApp.Interfaces.IServices;

namespace OpenCountryAPISampleApp.Services
{
    public class RestCountriesService : IRestCountriesService
    {
        private readonly IRestCountriesRepository _restCountriesRespository;

        public RestCountriesService(IRestCountriesRepository restCountriesRespository)
        {
            _restCountriesRespository = restCountriesRespository;
        }

        public async Task<IEnumerable<dynamic>> GetAllCountriesAsync(int pagination = 0, string sort = "")
        {
            
            var countries = await _restCountriesRespository.GetAllCountriesAsync();

            if (pagination == 0 && sort == "")
            {
                return countries;
            }

            if (sort != "")
            {
                countries = Sort.SortByName(countries, sort);
            }
            

            if (pagination > 0)
            {
                countries = Pagination.PaginateBySize(countries, pagination);

            }

            return countries;

        }


        public async Task<IEnumerable<dynamic>> GetCountriesByNameAsync(string name, int pagination = 0, string sort = "")
        {
            var countries = await _restCountriesRespository.GetAllCountriesAsync();
            var filteredCountries = Filter.FilterByName(countries, name);

            if (pagination == 0 && sort == "")
            {
                return filteredCountries;
            }

            if (sort != "")
            {
                filteredCountries = Sort.SortByName(filteredCountries, sort);
            }

            if (pagination > 0)
            {
                filteredCountries = Pagination.PaginateBySize(filteredCountries, pagination);
            }

            return filteredCountries;
        }

        public async Task<IEnumerable<dynamic>> GetCountriesByPopulationAsync(string population, int pagination = 0, string sort = "")
        {
            var countries = await _restCountriesRespository.GetAllCountriesAsync();
            var filteredCountries = Filter.FilterByPopulation(countries, population);

            if (pagination == 0 && sort == "")
            {
                return filteredCountries;
            }

            if (sort != "")
            {
                filteredCountries = Sort.SortByName(filteredCountries, sort);
            }

            if (pagination > 0)
            {
                filteredCountries = Pagination.PaginateBySize(filteredCountries, pagination);
            }

            return filteredCountries;
        }


        public async Task<IEnumerable<dynamic>> GetCountriesByNameAndPopulationAsync(string name, string population, int pagination = 0, string sort = "")
        {
            var countries = await _restCountriesRespository.GetAllCountriesAsync();
            
            var filteredCountries = Filter.FilterByPopulation(countries, population);
            
            filteredCountries = Filter.FilterByName(filteredCountries, name);

            if (pagination == 0 && sort == "")
            {
                return filteredCountries;
            }

            if (sort != "")
            {
                filteredCountries = Sort.SortByName(filteredCountries, sort);
            }

            if (pagination > 0)
            {
                filteredCountries = Pagination.PaginateBySize(filteredCountries, pagination);
            }

            return filteredCountries;
        }


    }
}

