
namespace CabManagementSystem.Data
{
    public class ApplicationDbContext: IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
           
           
        }


        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        public DbSet<Booking> Bookings { get; set; }

        public DbSet<Driver> Drivers { get; set; }

        
    }
}
