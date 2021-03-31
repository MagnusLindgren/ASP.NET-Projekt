using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASP.NET_Projekt.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public async Task ResetAndSeedAsync(UserManager<User> userManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            User user = new User()
            {
                UserName = "testuser",
                Email = "test@test.com",
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "070 666 40 45"                
            };

            Event[] events = new Event[]
            {
                new Event()
                {
                    Title="TomorrowLand",
                    Description="is a Belgian electronic dance music festival held in Belgium. Tomorrowland was first held in 2005." +
                    " It now stretches over two weekends and usually sells out in minutes.",
                    Place="Belgium",
                    Address="515 S Cascade Ave Colorado Springs, CO 80903",
                    Date=DateTime.Now.AddDays(34),
                    SpotsAvailable=234,
                    Organizer=user
                }
            };
            await userManager.CreateAsync(user, "Losenord123!");

            await AddRangeAsync(events);
            await SaveChangesAsync();
        }
    }
}
