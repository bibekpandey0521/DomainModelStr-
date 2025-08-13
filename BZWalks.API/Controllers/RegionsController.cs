using BZWalks.API.Data;
using BZWalks.API.Models.Domain;
using BZWalks.API.Models.DTO;
using BZWalks.API.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BZWalks.API.Controllers
{
    // https://localhost:port
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly BZWalksDbContext dbContext;

        public RegionsController(BZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        //Get All Regions
        //Get:  https://localhost:poetnumber/api/regions
        [HttpGet]
        public IActionResult GetAll()
        {
            //var regions = new List<Region>
            //{
            //    new Region
            //    {
            //        Id   = Guid.NewGuid(),
            //        Name = "Auckland Region",
            //        Code = "AKL",
            //        RegionImageUrl = "https://media.istockphoto.com/id/1419410282/photo/silent-forest-in-spring-with-beautiful-bright-sun-rays.jpg?s=612x612&w=0&k=20&c=UHeb1pGOw6ozr6utsenXHhV19vW6oiPIxDqhKCS2Llk="
            //    },
            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "Wellington Region",
            //        Code = "WlG",
            //        RegionImageUrl = "https://media.istockphoto.com/id/1419410282/photo/silent-forest-in-spring-with-beautiful-bright-sun-rays.jpg?s=612x612&w=0&k=20&c=UHeb1pGOw6ozr6utsenXHhV19vW6oiPIxDqhKCS2Llk="
            //    }

            //};
            //

            // Get  Data from Database -Domain Models
            var regionsDomain = dbContext.Regions.ToList();

            //Map Domain Models to DTOs
            
            var regionsDto = new List<RegionDto>();

            foreach (var region in regionsDomain)
            {
                regionsDto.Add(new RegionDto()
                {
                    Id = region.Id,
                    Name = region.Name,
                    Code = region.Code,
                    RegionImageUrl = region.RegionImageUrl,
                });
            }

            //Return DTOs  
            return Ok(regionsDto);
        }

        // GET SINGLE REGION (Get Region By Id)
        // GET: https://localhost:portnumber/api/regions

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //var region = dbContext.Regions.Find(id);
            
            //var region = dbContext.Regions.FirstOrDefault(x => x.Id == id);
            
            //Get Region Domain Model From Database
            var regionDomain = dbContext.Regions.FirstOrDefault(x => x.Id == id);


            //Map Domain Models to DTOs

            if(regionDomain == null)
            {
                return NotFound();
            }

            // Map/Convert Region Domain Model to Region DTO

            var regionDto = new RegionDto
            {
                Id = regionDomain.Id,
                Code = regionDomain.Code,
                Name = regionDomain.Name,
                RegionImageUrl = regionDomain.RegionImageUrl
            };


            //Return DTO back to client
            return Ok(regionDto);
        }

        //POST To Create New Region
        //POST: https://localhost:portnumber/api/regions
        
        [HttpPost]
        public IActionResult Create([FromBody] AddRegionRequestDto addRegionRequestDto)
        {
            //Map or Convert DTO to Domain Model
            var regionDomainModel = new Region
            {
                Code = addRegionRequestDto.Code,
                Name = addRegionRequestDto.Name,
                RegionImageUrl = addRegionRequestDto.RegionImageUrl
            };
            

            //Use Domain Model to create Region
            dbContext.Regions.Add(regionDomainModel);
            dbContext.SaveChanges();

            //Map Domain model back to DTO
            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return CreatedAtAction(nameof(GetById), new { id = regionDomainModel.Id }, regionDto);
        
        }

        // Update region
        // PUT : https://localhost:portnumber/api/regions/{id}
        
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody]UpdateRegionRequestDto updateRegionRequestDto)
        {
            //Check if region exists
            var  regionDomainModel =   dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }
            //Map DTO to Domain model
            regionDomainModel.Code = updateRegionRequestDto.Code;
            regionDomainModel.Name = updateRegionRequestDto.Name;
            regionDomainModel.RegionImageUrl = updateRegionRequestDto.RegionImageUrl;

            dbContext.SaveChanges();

            //Convert Domain Model to DTO

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };
            return Ok(regionDto);
        }

        // Delete Region
        // DELETE: https://localhost:portnumber/api/regions/{id}

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            var regionDomainModel =  dbContext.Regions.FirstOrDefault(x => x.Id == id);

            if(regionDomainModel == null)
            {
                return NotFound();
            }

            //Delete region
            dbContext.Regions.Remove(regionDomainModel);
            dbContext.SaveChanges();

            //return deleted Region back
            //map Domain Model to Dto

            var regionDto = new RegionDto
            {
                Id = regionDomainModel.Id,
                Code = regionDomainModel.Code,
                Name = regionDomainModel.Name,
                RegionImageUrl = regionDomainModel.RegionImageUrl
            };

            return Ok(regionDto);
        }
    }
}
