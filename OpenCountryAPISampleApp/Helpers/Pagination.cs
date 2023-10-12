namespace OpenCountryAPISampleApp.Helpers
{
    public static class Pagination
    {
        public static IEnumerable<dynamic> PaginateBySize(IEnumerable<dynamic> collection, int topn)
        {
            return collection.Take(topn);
        }
    }
}
