using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Models
{
    public class BuildingBuddiesContext : DbContext
    {
        public BuildingBuddiesContext (DbContextOptions<BuildingBuddiesContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }
        public DbSet<Department> Department { get; set; }
        public DbSet<Meeting> Meeting { get; set; }
        public DbSet<AgreedMeeting> AgreedMeeting { get; set; }
    }
}
