using BZWalks.API.Data;
using BZWalks.API.Models.Domain;
using BZWalks.API.Models.DTO;
using BZWalks.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace BZWalks.API.Controllers
{
    // https://localhost:port
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly BZWalksDbContext dbContext;
        private readonly IRegionRepository regionRepository;

        public RegionsController(BZWalksDbContext dbContext,IRegionRepository regionRepository)
        {
            this.dbContext = dbContext;
            this.regionRepository = regionRepository;
        }


        //GET ALL REGIONS
        //GET:https://localhost:portnumber/api/rescources
        [HttpGet]

        public async Task<IActionResult> GetAll()
        {
            //Get Data From Database - Domain models
            var regionsDomain = await regionRepository.GetAllAsync();

            //Map Domain Model to DTO
            var regionsDto = new List<RegionDto>();

            foreach (var regionDomain in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = regionDomain.Id,
                    Code = regionDomain.Code,
                    Name = regionDomain.Name,
                    RegionImageUrl = regionDomain.RegionImageUrl,
                });

            }
            //Return DTOs
            return Ok(regionsDto);
        }


        //GET SINGLE REGION (Get Region By Id)
        //GET:https://localhost:portnumber/api/regoins/{id}
        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            //Get Region Domain MOdel From Database
            //var  regionDomain = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomain = await regionRepository.GetByIdAsync(id);
            if(regionDomain == null)
            {
                return NotFound();
            }

            //Map/Convert Region Domain Model to Region Dto 
            //
            var regionsDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code  = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };

            //Return DTO back to client
            return Ok(regionsDto);
        }
        //POST To Create New Region
        //POST:https://localhost:portnumber/api/regions
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl =addRegionRequestDto.RegionImageUrl,
            };

            //Use Domain Model to create Region
            //await dbContext.Regions.AddAsync(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            regionDomainModel = await regionRepository.CreateAsync(regionDomainModel);

            //Map Domain Model back to DTO
            var regionDto = new RegionDto 
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl=regionDomainModel.RegionImageUrl,
            };

            return CreatedAtAction(nameof(GetById),new {id = regionDto.Id},regionDto);

        }

        //Update the Region
        //PUT: https://localhost:portnumber/api/regions/{id}
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exits
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x=> x.Id == id);

            //Map DTO to Domain model 
            var regionDomainModel = new Region
            {
                Code = updateRegionRequestDto.Code,
                Name = updateRegionRequestDto.Name,
                RegionImageUrl = updateRegionRequestDto.RegionImageUrl,
            };

            regionDomainModel = await regionRepository.UpdateAsync(id,regionDomainModel);
            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //Map DTO To Domain Model
            //regionDomainModel.Code= updateRegionRequestDto.Code;
            //regionDomainModel.Name = updateRegionRequestDto.Name;
            //regionDomainModel.RegionImageUrl= updateRegionRequestDto.RegionImageUrl;

            //await dbContext.SaveChangesAsync();

            //Convert Domain Model to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }

        //Delete Region
        //DELETE:https://localhost:portnumber/api/regions/{id}
        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            //var regionDomainModel = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            var regionDomainModel = await regionRepository.DeleteAsync(id);
            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            //dbContext.Regions.Remove(regionDomainModel);
            //await dbContext.SaveChangesAsync();

            //return deleted Region back
            //map Domain Model to Dto
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl,
            };

            return Ok(regionDto);
        }
    }
}    