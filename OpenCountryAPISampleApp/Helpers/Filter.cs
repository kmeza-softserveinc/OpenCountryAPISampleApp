using System.Linq;

namespace OpenCountryAPISampleApp.Helpers
{
    public static class Filter
    {
        public static IEnumerable<dynamic> FilterByName(IEnumerable<dynamic> collection, string name)
        {
            return collection.Where(x => x.name.common.ToString().IndexOf(name, StringComparison.OrdinalIgnoreCase) >= 0);

        }

        public static IEnumerable<dynamic> FilterByPopulation(IEnumerable<dynamic> collection, string population)
        {
            var populationNumber = StringToNumber.ConvertMultiplierString(population);

            return collection.Where(x =>
            {
                var currentPopulation = x.population;

                if (currentPopulation == null)
                    return false;

                int currentPopulationNumber;

                if (!int.TryParse(currentPopulation.ToString(), out currentPopulationNumber))
                    return false;

                return currentPopulationNumber <= populationNumber;
            });
        }
    }
}
