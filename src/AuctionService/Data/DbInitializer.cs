

using AuctionService.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Data
{
    // Use this class program.cs (startup of the app), before the app even run
    public class DbInitializer
    {
        public static void InitDb(WebApplication app) 
        {
            // Access DbContext to add data to the database
            using var scope = app.Services.CreateScope();
            SeedData(scope.ServiceProvider.GetService<AuctionDbContext>());

        }
        private static void SeedData(AuctionDbContext context)
        {
            // Applies any pending migrations for the context to the database. 
            // Will create the database if it does not already exist.
            context.Database.Migrate();
            // First check if there are some auctions data in the database
            if (context.Auctions.Any()) 
            {
                // If data exist in database no need to seed more data
                Console.WriteLine("Alreadly have data - no need to seed");
                return;
            }
            // To seed data to the database and save changes to the database
            var auctions = DataSeeder.GetAuctions();
            context.AddRange(auctions);
            context.SaveChanges();

        }
    }
}