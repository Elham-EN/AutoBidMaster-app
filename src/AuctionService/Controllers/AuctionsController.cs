using AuctionService.Data;
using AuctionService.DTOs;
using AuctionService.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionService.Controllers
{
    [ApiController]
    [Route("api/auctions")]
    public class AuctionsController: ControllerBase
    {
        private readonly AuctionDbContext _context;
        private readonly IMapper _mapper;
        public AuctionsController(AuctionDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }
        // return a list of auction DTOs to the client
        [HttpGet]
        public async Task<ActionResult<List<AuctionDto>>> GetAllAcutions()
        {
            try
            {
                var auctions = await _context.Auctions
                    .Include(x => x.Item)
                    .OrderBy(x => x.Item.Make)
                    .ToListAsync();
                return _mapper.Map<List<AuctionDto>>(auctions);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to fetch all auctions from the database");
                return StatusCode(500, ex.Message);
            }
        }
        // To get specific auction, must specify a route paramter of 'id'
        [HttpGet("{id}")]
        public async Task<ActionResult<AuctionDto>> GetAuctionById(Guid id)
        {
           try
           {
            var auction = await _context.Auctions
               .Include(x => x.Item)
               // Asynchronously returns the first element of a sequence that satisfies a 
               // specified condition or a default value if no such element is found.
               .FirstOrDefaultAsync(x => x.Id == id);
            if (auction == null) return NotFound("item not found");
            return _mapper.Map<AuctionDto>(auction);
           }
           catch (Exception ex)
           {
                Console.WriteLine("Failed to fetch all auctions from the database");
                return StatusCode(400, ex);
           }
        }
        [HttpPost]
        public async Task<ActionResult<AuctionDto>> CreateAuction(CreateAuctionDto auctionDto)
        {
            // Map the CreateAuctionDto into Auction Entity
            var auction = _mapper.Map<Auction>(auctionDto);
            // TODO: add current user as seller
            auction.Seller = "test";
            _context.Auctions.Add(auction);
            // if return 0, mean noting was saved in the database
            var result = await _context.SaveChangesAsync() > 0;
            if (!result) return BadRequest("Could not save changes to the DB");
            return CreatedAtAction(
                nameof(GetAuctionById), 
                new {auction.Id}, 
                _mapper.Map<AuctionDto>(auction));
        }
    }
}