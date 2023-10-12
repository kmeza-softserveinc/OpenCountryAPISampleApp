namespace OpenCountryAPISampleApp.Helpers
{
    public static class Sort
    {
        public static IEnumerable<dynamic> SortByName(IEnumerable<dynamic> collection, string sortOrder)
        {
            sortOrder = sortOrder.ToLower();

            if (sortOrder != "asc" && sortOrder != "ascending" && sortOrder != "desc" && sortOrder != "descending")
            {
                return new List<dynamic>
                {
                    new { error = "Invalid Sort" }
                };
            }

            switch (sortOrder)
            {
                case "asc":
                case "ascending":
                    return collection.OrderBy(x => x.name.common.ToString());

                case "desc":
                case "descending":
                    return collection.OrderByDescending(x => x.name.common.ToString());

                default:
                    throw new ArgumentException("Invalid Sort Order provided");
            }
        }
    }
}
