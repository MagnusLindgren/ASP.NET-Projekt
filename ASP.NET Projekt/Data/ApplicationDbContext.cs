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
                    Address="De Schorre, 2850 Boom",
                    Date=DateTime.Now.AddDays(34),
                    SpotsAvailable=234,
                    Organizer=user
                },
                new Event()
                {
                    Title="Ultra Music Festival",
                    Description="is an annual outdoor electronic music festival that takes place during March in Miami, United States. " +
                    "The festival was founded in 1999 by Russell Faibisch and Alex Omes and is named after the 1997 Depeche Mode album, Ultra.",
                    Place="Miami",
                    Address="395 E Flagler St, Miami, FL 33132",
                    Date=DateTime.Now.AddDays(34),
                    SpotsAvailable=234,
                    Organizer=user
                },
                new Event()
                {
                    Title="Defqon.1 Festival",
                    Description="is an annual music festival held in the Netherlands. It was founded in 2003 by festival organizer Q-dance. " +
                    "The festival plays mostly hardstyle and related genres such as hardcore techno, hard house and hard trance.",
                    Place="Netherlands",
                    Address="Spijkweg 30, 8256 RJ Biddinghuizen",
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
