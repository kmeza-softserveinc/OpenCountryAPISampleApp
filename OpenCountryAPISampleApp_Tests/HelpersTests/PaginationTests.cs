using OpenCountryAPISampleApp.Helpers;
using OpenCountryAPISampleApp_UnitTests.Setups;

namespace OpenCountryAPISampleApp_UnitTests.HelpersTests
{
    public class PaginationTests : IClassFixture<DataSetup>
    {
        private readonly IEnumerable<dynamic> _sampleData;

        public PaginationTests(DataSetup setup)
        {
            _sampleData = setup.SampleData;
        }

        [Fact]
        public void PaginateBySize_ReturnsExpectedNumberOfItems()
        {
            var topn = 5;
            var result = Pagination.PaginateBySize(_sampleData, topn).ToList();

            Assert.Equal(topn, result.Count);
        }

        [Fact]
        public void PaginateBySize_ReturnsCorrectItems()
        {
            var topn = 3;
            var result = Pagination.PaginateBySize(_sampleData, topn).ToList();
            var expected = _sampleData.Take(topn).ToList();

            Assert.Equal(expected, result);
        }

        [Fact]
        public void PaginateBySize_WithTopnGreaterThanCollectionSize_ReturnsAllItems()
        {
            var topn = _sampleData.Count() + 5;
            var result = Pagination.PaginateBySize(_sampleData, topn).ToList();

            Assert.Equal(_sampleData.Count(), result.Count);
        }

        [Fact]
        public void PaginateBySize_WithNegativeTopn_ReturnsEmptyCollection()
        {
            var result = Pagination.PaginateBySize(_sampleData, -5).ToList();

            Assert.Empty(result);
        }


    }
}
