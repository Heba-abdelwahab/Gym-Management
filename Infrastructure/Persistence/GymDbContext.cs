using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace Persistence
{
    public class GymDbContext:DbContext
    {
        public GymDbContext(DbContextOptions<GymDbContext>options):base(options)
        {
            
        }
        public DbSet<User>users { get; set; }

    }
}
