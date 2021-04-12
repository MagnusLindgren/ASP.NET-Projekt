using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Projekt
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = true) 
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdminRole",
                    policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequireOrganizerRole",
                    policy => policy.RequireRole("Organizer"));
                options.AddPolicy("RequireAttendeeRole",
                    policy => policy.RequireRole("Attendee"));

            });

            services.AddRazorPages(o =>
            {
                o.Conventions.AllowAnonymousToPage("/Index");
                o.Conventions.AllowAnonymousToPage("/Events/Index");
                o.Conventions.AuthorizeFolder("/");
                o.Conventions.AuthorizeFolder("/OrganizerEvents", "RequireOrganizerRole");
                o.Conventions.AuthorizeFolder("/Admin", "RequireAdminRole");
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
