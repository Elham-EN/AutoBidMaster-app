using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;

namespace AuctionService.RequestHelpers
{
    public class MappingProfiles: Profile // derived from AutoMapper
    {
        public MappingProfiles()
        {
            // Tell AutoMapper what we want it to map from and what we want it to map to. 
            CreateMap<Auction, AuctionDto>().IncludeMembers(x => x.Item);
            CreateMap<Item, AuctionDto>();
            // Car properties goes to the Auction Item object from the source which is the car object
            CreateMap<CreateAuctionDto, Auction>().
                ForMember(d => d.Item, o => o.MapFrom(s => s));
            CreateMap<CreateAuctionDto, Item>();
        }   
    }
}