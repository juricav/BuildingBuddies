using System;
using BuildingBuddies.Areas.Identity.Data;
using BuildingBuddies.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
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
                services.AddDbContext<BuildingBuddiesIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("BuildingBuddiesIdentityContextConnection")));

                services.AddDefaultIdentity<BuildingBuddiesUser>()
                    .AddEntityFrameworkStores<BuildingBuddiesIdentityContext>();
            });
        }
    }
}