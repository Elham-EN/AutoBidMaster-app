
using System.Text.Json;
using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Models;
using SearchService.Services;

namespace SearchService.Data
{
    public class DbInitializer
    {
        public static async Task InitDb(WebApplication app)
        {
            // MongoDB Initialization
            await DB.InitAsync("SearchServiceDB", MongoClientSettings
                .FromConnectionString(app
                    .Configuration.GetConnectionString("MongoDbConnection")));
            // Create index for the Item (for cenrtain fields) to be able to search on
            await DB.Index<Item>()
                .Key(x => x.Make, KeyType.Text)
                .Key(x => x.Model, KeyType.Text)
                .Key(x => x.Color, KeyType.Text)
                .CreateAsync();
            var count = await DB.CountAsync<Item>();
            using var scope = app.Services.CreateScope();
            var httpClient = scope.ServiceProvider.GetRequiredService<AuctionServiceHttpClient>();
            var item = await httpClient.GetItemsForSearchDb();
            Console.WriteLine(item.Count + "returned from the auction service");
            // if there is data to save
            if (item.Count > 0) await DB.SaveAsync(item);
        }   
    }
}