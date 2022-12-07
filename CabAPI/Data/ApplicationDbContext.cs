using Microsoft.EntityFrameworkCore;

namespace CabAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }
            public DbSet<Cab> Cabs { get; set; }
    }
}
