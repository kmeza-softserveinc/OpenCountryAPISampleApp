using OpenCountryAPISampleApp.Helpers;
using OpenCountryAPISampleApp_UnitTests.Setups;

namespace OpenCountryAPISampleApp_UnitTests.HelpersTests
{
    public class FilterTests : IClassFixture<DataSetup>
    {
        private readonly IEnumerable<dynamic> _sampleData;

        public FilterTests(DataSetup setup)
        {
            _sampleData = setup.SampleData;
        }

        [Fact]
        public void FilterByName_CaseInsensitiveMatch_ReturnsFilteredCountries()
        {
            var filtered = Filter.FilterByName(_sampleData, "cHilE").ToList();
            Assert.True(filtered.Count > 0);
        }

        [Fact]
        public void FilterByName_PartialMatch_ReturnsFilteredCountries()
        {
            var filtered = Filter.FilterByName(_sampleData, "CHI").ToList();
            Assert.True(filtered.Count > 0);
        }

        [Fact]
        public void FilterByName_NoMatch_ReturnsEmptyList()
        {
            var filtered = Filter.FilterByName(_sampleData, "NonExistentOrImaginaryCountryName").ToList();
            Assert.Empty(filtered);
        }

        [Fact]
        public void FilterByPopulation_NumericValue_ReturnsFilteredCountries()
        {
            var filtered = Filter.FilterByPopulation(_sampleData, "10000").ToList();
            Assert.True(filtered.Count > 0);
        }

        [Fact]
        public void FilterByPopulation_StringValue_ReturnsFilteredCountries()
        {
            var filtered = Filter.FilterByPopulation(_sampleData, "5M").ToList();
            Assert.True(filtered.Count > 0);
        }

        [Fact]
        public void FilterByPopulation_InvalidNumericValue_ThrowsArgumentException()
        {
            
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                var filtered = Filter.FilterByPopulation(_sampleData, "NonNumericOrInvalidValue").ToList();
            });

            Assert.Equal("Invalid population format.", exception.Message);
        }


    }
}
