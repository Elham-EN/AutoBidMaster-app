
namespace AuctionService.Entities
{
    public class Item
    {
        public Guid Id { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Mileage { get; set; }
        public string ImageUrl { get; set; }

        // It has a navigational property Auction to link to its 
        // corresponding auction, along with AuctionId as a foreign 
        // key.
        public Auction Auction { get; set; }
        public Guid AuctionId { get; set; }
    }
}