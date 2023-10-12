using OpenCountryAPISampleApp.Interfaces.IRepositories;

namespace OpenCountryAPISampleApp_UnitTests.Setups;

public class RestCountriesRepositoryMockSetup
{
    public IRestCountriesRepository MockRepository { get; private set; }
    public DataSetup DataSetup { get; private set; }

    public RestCountriesRepositoryMockSetup()
    {
        DataSetup = new DataSetup();
        MockRepository = Substitute.For<IRestCountriesRepository>();
        MockRepository.GetAllCountriesAsync().Returns(DataSetup.SampleData);
    }
}