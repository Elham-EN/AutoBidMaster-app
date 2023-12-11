
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.RequestHelpers;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItem([FromQuery]SearchParams searchParams)
        {
            // Retrieving Entities: Retrieving data
            var query = DB.PagedSearch<Item, Item>();
            // Search Functionaility
            if (!string.IsNullOrEmpty(searchParams.SearchTerm))
            {
                query.Match(Search.Full, searchParams.SearchTerm).SortByTextScore();
            }
            // Sorting By Fucntionility
            query = searchParams.OrderBy switch
            {
                "make" => query.Sort(x => x.Ascending(a => a.Make)),
                "new" => query.Sort(x => x.Descending(a => a.CreatedAt)),
                // Default parameter: auction ending sonnest
                _ => query.Sort(x => x.Ascending(a => a.AuctionEnd))
            };
            // Filtering By Functionility
            query = searchParams.FilterBy switch
            {
                // auctions that have already ended.
                "finished" => query.Match(x => x.AuctionEnd < DateTime.UtcNow),
                // Auctions that are ending within 6 hours, only returning auctions that are still live 
                // close to being ended
                "endingSoon" => query.Match(x => x.AuctionEnd < DateTime.UtcNow.AddHours(6) 
                    && x.AuctionEnd > DateTime.UtcNow),
                // Return only live auctions
                _ => query.Match(x => x.AuctionEnd > DateTime.UtcNow)
            };
            if (!string.IsNullOrEmpty(searchParams.Seller))
            {
                query.Match(x => x.Seller == searchParams.Seller);
            }
            if (!string.IsNullOrEmpty(searchParams.Winner))
            {
                query.Match(x => x.Winner == searchParams.Winner);
            }
            // Pagination Functionility
            query.PageNumber(searchParams.PageNumber);
            query.PageSize(searchParams.PageSize);
            // Get list of results from MongoDB
            var result = await query.ExecuteAsync();
            return Ok(new 
                {
                    result = result.Results, 
                    pageCount = result.PageCount, 
                    totalCount = result.TotalCount
                }
            );
        }
    }
}