
using AutoMapper;
using Contracts;
using SearchService.Models;

namespace SearchService.RequestHelpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            // Creates a mapping configuration from the 
            // TSource type to the TDestination type
            CreateMap<AuctionCreated, Item>();
        }
    }
}