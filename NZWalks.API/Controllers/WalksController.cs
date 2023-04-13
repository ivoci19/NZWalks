using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NZWalks.API.CustomActionFilters;
using NZWalks.Data.IRepositories;
using NZWalks.Data.Models.Domain;
using ViewModels.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WalksController : ControllerBase
    {

        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }

        [HttpGet]
        //Get: /api/walks?filterOn=Name&filterQuery=Track
        public async Task<IActionResult> GetAllWalksAsync([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            // Fetch data from database - domain walks
            var walksDomain = await walkRepository.GetAllAsync(filterOn,filterQuery);

            // Convert domain walks to DTO Walks
            var walksDTO = mapper.Map<List<WalkDTO>>(walksDomain);

            // Return response
            return Ok(walksDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("GetWalkAsync")]
        public async Task<IActionResult> GetWalkAsync(Guid id)
        {
            // Get Walk Domain object from database
            var walkDomin = await walkRepository.GetAsync(id);

            // Convert Domain object to DTO
            var walkDTO = mapper.Map<Walk>(walkDomin);

            // Return response
            return Ok(walkDTO);
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> AddWalkAsync([FromBody] WalkDTO addWalkRequest)
        {
            // Convert DTO to Domain Object
            var walkDomain = new Walk
            {
                LengthInKm = addWalkRequest.LengthInKm,
                Name = addWalkRequest.Name,
                RegionId = addWalkRequest.RegionId,
                DifficultyId = addWalkRequest.DifficultyId
            };

            // Pass domain object to Repository to persist this
            walkDomain = await walkRepository.AddAsync(walkDomain);

            // Convert the Domain object back to DTO
            var walkDTO = new Walk
            {
                Id = walkDomain.Id,
                LengthInKm = walkDomain.LengthInKm,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                DifficultyId = walkDomain.DifficultyId
            };

            // Send DTO response back to Client
            return CreatedAtAction(nameof(GetWalkAsync), new { id = walkDTO.Id }, walkDTO);
        }


        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModel]
        public async Task<IActionResult> UpdateWalkAsync([FromRoute] Guid id,
            [FromBody] WalkDTO updateWalkRequest)
        {
            // Convert DTO to Domain object
            var walkDomain = new Walk
            {
                LengthInKm = updateWalkRequest.LengthInKm,
                Name = updateWalkRequest.Name,
                RegionId = updateWalkRequest.RegionId,
                DifficultyId = updateWalkRequest.DifficultyId
            };

            // Pass details to Repository - Get Domain object in response (or null)
            walkDomain = await walkRepository.UpdateAsync(id, walkDomain);

            // Handle Null (not found)
            if (walkDomain == null)
            {
                return NotFound();
            }

            // Convert back Domain to DTO
            var walkDTO = new WalkDTO
            {
                Id = walkDomain.Id,
                LengthInKm = walkDomain.LengthInKm,
                Name = walkDomain.Name,
                RegionId = walkDomain.RegionId,
                DifficultyId = walkDomain.DifficultyId
            };

            // Return Response
            return Ok(walkDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteWalkAsync(Guid id)
        {
            // call Repository to delete walk
            var walkDomain = await walkRepository.DeleteAsync(id);

            if (walkDomain == null)
            {
                return NotFound();
            }

            var walkDTO = mapper.Map<WalkDTO>(walkDomain);

            return Ok(walkDTO);
        }
    }
}
