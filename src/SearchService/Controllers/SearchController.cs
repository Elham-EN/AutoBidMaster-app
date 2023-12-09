
using Microsoft.AspNetCore.Mvc;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class SearchController: ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Item>>> SearchItem(string searchTerm)
        {
            // Retrieving Entities: Retrieving data
            var query = DB.Find<Item>();
            // Sort the query data in ascending order
            query.Sort(x => x.Ascending(a => a.Make));
            // Search Functionaility
            if (!string.IsNullOrEmpty(searchTerm))
            {
                query.Match(Search.Full, searchTerm).SortByTextScore();
            }
            // Get list of results from MongoDB
            var result = await query.ExecuteAsync();
            return result;
        }
    }
}