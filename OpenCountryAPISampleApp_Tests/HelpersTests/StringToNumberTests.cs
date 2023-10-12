using OpenCountryAPISampleApp.Helpers;

namespace OpenCountryAPISampleApp_UnitTests.HelpersTests
{
    public class StringToNumberTests
    {
        [Theory]
        [InlineData("12345", 12345)]
        [InlineData("10k", 10000)]
        [InlineData("5m", 5000000)]
        [InlineData("5M", 5000000)]
        [InlineData("0", 0)]
        public void ConvertMultiplierString_ValidInput_ReturnsCorrectNumber(string input, long expected)
        {
            long result = StringToNumber.ConvertMultiplierString(input);
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("10km")]
        [InlineData("k10")]
        [InlineData("10m10")]
        [InlineData("m")]
        [InlineData("k")]
        public void ConvertMultiplierString_InvalidInput_ThrowsArgumentException(string input)
        {
            Assert.Throws<ArgumentException>(() => StringToNumber.ConvertMultiplierString(input));
        }

        [Fact]
        public void ConvertMultiplierString_NullOrEmptyInput_ReturnsZero()
        {
            Assert.Equal(0, StringToNumber.ConvertMultiplierString(null));
            Assert.Equal(0, StringToNumber.ConvertMultiplierString(string.Empty));
        }
    }
    
}
