using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contracts;
using MassTransit;

namespace SearchService.Consumers
{
    public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
    {
        public Task Consume(ConsumeContext<AuctionDeleted> context)
        {
            Console.WriteLine("---> Consuming auction deleted: " + context.Message.Id);
            throw new NotImplementedException();
        }
    }
}