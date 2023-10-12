using OpenCountryAPISampleApp.Interfaces.IRepositories;
using OpenCountryAPISampleApp.Services;
using OpenCountryAPISampleApp_UnitTests.Setups;

namespace OpenCountryAPISampleApp_UnitTests.ServicesTests;

public class RestCountriesServiceTests
{
    private readonly IRestCountriesRepository _mockRepository;
    private readonly RestCountriesService _service;
    private readonly DataSetup _dataSetup;
    private readonly RestCountriesRepositoryMockSetup _repositoryMockSetup;

    
    public RestCountriesServiceTests()
    {
        _dataSetup = new DataSetup();
        _repositoryMockSetup = new RestCountriesRepositoryMockSetup();
        _mockRepository = _repositoryMockSetup.MockRepository;
        _service = new RestCountriesService(_mockRepository);
    }

    [Fact]
    public async Task GetAllCountriesAsync_ReturnsAllCountries_WhenNoFilterIsApplied()
    {
        // Act
        var result = await _service.GetAllCountriesAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_dataSetup.SampleData, result);
    }

    [Theory]
    [InlineData("CHILE")]
    [InlineData("Chile")]
    [InlineData("chile")]
    [InlineData("cHiLe")]
    public async Task GetAllCountriesAsync_ReturnsOneCountry_WhenFilterByChile(string countryFilter)
    {
        // Act
        var result = await _service.GetCountriesByNameAsync(countryFilter);
        
        // Assert
        Assert.True(result.Count() == 1);
    }
    
    [Theory]
    [InlineData("CHI")]
    [InlineData("Chi")]
    [InlineData("chi")]
    [InlineData("cHi")]
    public async Task GetAllCountriesAsync_ReturnsMultipleCountries_WhenFilterByCHI(string countryFilter)
    {
        // Act
        var result = await _service.GetCountriesByNameAsync(countryFilter);
        
        // Assert
        Assert.True(result.Count() > 1);
    }
    
}