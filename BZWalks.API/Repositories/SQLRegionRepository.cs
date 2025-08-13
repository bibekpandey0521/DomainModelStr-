using BZWalks.API.Data;
using BZWalks.API.Models.Domain;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace BZWalks.API.Repositories
{
    public class SQLRegionRepository : IRegionRepository
    {
        private readonly BZWalksDbContext dbContext;
        public SQLRegionRepository(BZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Region>> GetAllAsync()
        {
            return await dbContext.Regions.ToListAsync();
        }

        public async Task<Region?> GetByIdAsync(Guid id)
        {
             return await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);  
        }
        public async Task<Region> CreateAsync(Region region)
        {
            await dbContext.Regions.AddAsync(region);
            await dbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region?> UpdateAsync(Guid id, Region region)
        {
            var existingRegion = await dbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);
            if (existingRegion == null) 
            {
                return null;
            }
            existingRegion.Code = region.Code;
            existingRegion.Name = region.Name;
            existingRegion.RegionImageUrl = region.RegionImageUrl;

            await dbContext.SaveChangesAsync();
            return existingRegion;
        }

        public async Task<Region?> DeleteAsync(Guid id)
        {
            var exisitingRegion = await dbContext.Regions.FirstOrDefaultAsync( x => x.Id == id);
            
            if(exisitingRegion == null)
            {
                return null;
            }
            dbContext.Regions.Remove(exisitingRegion);
            await dbContext.SaveChangesAsync();
            return exisitingRegion;
        }
    }
}
