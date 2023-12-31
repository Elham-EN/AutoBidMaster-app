using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Contracts;

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
            // To publish the createdAuction, we first need to map to AuctionCreated object
            CreateMap<AuctionDto, AuctionCreated>();
            CreateMap<Auction, AuctionUpdated>().IncludeMembers(a => a.Item);
            CreateMap<Item, AuctionUpdated>();
        }   
    }
} 