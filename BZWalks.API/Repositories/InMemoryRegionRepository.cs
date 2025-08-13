using BZWalks.API.Models.Domain;

namespace BZWalks.API.Repositories
{
    public class InMemoryRegionRepository : IRegionRepository
    {
        public  async Task<List<Region>> GetAllAsync()
        {
            return new List<Region>
            {
                new Region()
                {
                    Id = Guid.NewGuid(),
                    Code = "XYZ",
                    Name = "XYZ's Region Name"
                }
            };

        }
    }
}
