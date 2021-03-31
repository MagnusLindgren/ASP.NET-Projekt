using System;
using ASP.NET_Projekt.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ASP.NET_Projekt.Areas.Identity.IdentityHostingStartup))]
namespace ASP.NET_Projekt.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ASPNET_ProjektContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ASPNET_ProjektContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ASPNET_ProjektContext>();
            });
        }
    }
}