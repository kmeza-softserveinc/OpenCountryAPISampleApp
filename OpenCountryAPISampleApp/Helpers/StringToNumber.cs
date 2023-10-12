namespace OpenCountryAPISampleApp.Helpers
{
    public static class StringToNumber
    {
        public static long ConvertMultiplierString(string? population)
        {
            if (string.IsNullOrEmpty(population))
            {
                return 0;
            }

            if (long.TryParse(population, out long numericValue))
            {
                return numericValue;
            }

            population = population.ToLower();
            long multiplier = 1;

            if (population.EndsWith("k"))
            {
                multiplier = 1000;
                population = population.TrimEnd('k');
            }
            else if (population.EndsWith("m"))
            {
                multiplier = 1000000;
                population = population.TrimEnd('m');
            }

            if (long.TryParse(population, out numericValue))
            {
                return numericValue * multiplier;
            }
            else
            {
                throw new ArgumentException("Invalid population format.");
            }
        }

    }
}
