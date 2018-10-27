using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BuildingBuddies.Models
{
    public class BuildingBuddiesContext : IdentityDbContext<User>
    {
        public BuildingBuddiesContext (DbContextOptions<BuildingBuddiesContext> options)
            : base(options)
        {            
        }

        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<AgreedMeeting> AgreedMeeting { get; set; }
        public DbSet<ChatMessage> ChatMessage { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
