using BuildingBuddies.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(BuildingBuddies.Areas.Identity.IdentityHostingStartup))]
namespace BuildingBuddies.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<BuildingBuddiesContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BuildingBuddiesContext")));

                services.AddDefaultIdentity<User>()
                    .AddEntityFrameworkStores<BuildingBuddiesContext>();
            });
        }
    }
}