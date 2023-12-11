
namespace SearchService.RequestHelpers
{
    // Properties use for query strings
    public class SearchParams
    {
        // Pagination
        public string SearchTerm { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 4;
        // Get auction they're either selling or the auction that they won
        public string Seller { get; set; }
        public string Winner { get; set; }
        // Sorting and Filtering
        public string OrderBy { get; set; }
        public string FilterBy { get; set; }
    }
}