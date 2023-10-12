using Newtonsoft.Json;

namespace OpenCountryAPISampleApp_UnitTests.Setups
{
    public class DataSetup
    {
        public IEnumerable<dynamic> SampleData { get; private set; }

        public DataSetup()
        {
            var json = File.ReadAllText("TestData/SampleRESTCountriesData.json");
            var deserializedData = JsonConvert.DeserializeObject<IEnumerable<dynamic>>(json);
            SampleData = deserializedData ?? throw new InvalidDataException();
        }

    }
}
