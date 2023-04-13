using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.CustomActionFilters;
using NZWalks.Data.Data;
using NZWalks.Data.IRepositories;
using NZWalks.Data.Models.Domain;
using System.Text.RegularExpressions;
using ViewModels.DTO;

namespace NZWalks.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRegionRepository _regionRepository;
        private readonly IMapper _mapper;

        public RegionsController(ApplicationDbContext _dbContext, IRegionRepository _regionRepository, IMapper _mapper)
        {
            this._dbContext = _dbContext;
            this._regionRepository = _regionRepository;
            this._mapper = _mapper;
        }

        [HttpGet("GetAllRegions")]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _regionRepository.GetAllAsync();

            return Ok(_mapper.Map<List<RegionViewDTO>>(regions));

        }

        [HttpGet("GetRegionById")]
        public async Task<IActionResult> GetRegionById(Guid id)
        {
            //var Region = _dbContext.Regions.Find(id);
            var Region = await _regionRepository.GetAsync(id);


            if (Region == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RegionViewDTO>(Region));

        }

        [HttpPost("CreateRegion")]
        [ValidateModel]
        public async Task<IActionResult> CreateRegion([FromBody] RegionDTO regionBaseDTO)
        {
            
                var region = _mapper.Map<Region>(regionBaseDTO);

                region = await _regionRepository.AddAsync(region);

                var regionDto = _mapper.Map<RegionViewDTO>(region);

                //Map or Convert Dto to domain model
                return CreatedAtAction(nameof(GetRegionById), new { id = regionDto.Id }, regionDto);
           
        }

        [HttpPut("UpdateRegion")]
        [ValidateModel]
        public async Task<IActionResult> UpdateRegion(Guid id, [FromBody] RegionDTO regionEditDTO)
        {
            var regionModel = _mapper.Map<Region>(regionEditDTO);

            regionModel = await _regionRepository.UpdateAsync(id, regionModel);

            if(regionModel == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<RegionDTO>(regionModel));
        }


        [HttpDelete("DeleteRegion")]
        public async Task<IActionResult> DeleteRegion( Guid id)
        {
            var regionDomainModel = await _regionRepository.DeleteAsync(id);

            if( regionDomainModel == null)
            {
                return NotFound();
            }

            
            return Ok(_mapper.Map<RegionDTO>(regionDomainModel));
        }

    }
}
