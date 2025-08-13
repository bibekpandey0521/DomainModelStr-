using BZWalks.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BZWalks.API.Data
{
    public class BZWalksDbContext : DbContext
    {
        public BZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {
                               
        }
        public DbSet<Difficulty>   Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk>  Walks { get; set; }

    }
}
