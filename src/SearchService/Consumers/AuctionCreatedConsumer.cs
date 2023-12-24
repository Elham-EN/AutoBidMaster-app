

using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using SearchService.Models;

namespace SearchService.Consumers
{
    // Need to map the AuctionCreated into an item, so it can update
    // the database. Provide a profile to go from the AuctionCreated to
    // the item, which the MongoDB needs to save this as. 
    public class AuctionCreatedConsumer : IConsumer<AuctionCreated>
    {
        private readonly IMapper _mapper;
        // Inject AutoMapper into this particular class using DI Pattern 
        public AuctionCreatedConsumer(IMapper mapper) {
            _mapper = mapper;
        }
        // Here what we do when we consume this message (AuctionCreated)
        // when it arrives from the service bus. 
        public async Task Consume(ConsumeContext<AuctionCreated> context)
        {
            Console.WriteLine("---> Consuming auction created: " + context.Message.Id);
            // Create the item from the message
            var item = _mapper.Map<Item>(context.Message);
            // Save the item to the MongoDB Database
            await item.SaveAsync();
        }
    }
}

