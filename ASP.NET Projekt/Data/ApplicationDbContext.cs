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

        public async Task ResetAndSeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            await Database.EnsureDeletedAsync();
            await Database.EnsureCreatedAsync();

            // Skapar roller
            await roleManager.CreateAsync(new IdentityRole("Administrator"));
            await roleManager.CreateAsync(new IdentityRole("Organizer"));
            await roleManager.CreateAsync(new IdentityRole("Attendee"));

            await Roles.ToListAsync();

            // Admin
            User admin = new User()
            {
                UserName = "Admin",
                Email = "admin2admin.com",
                EmailConfirmed = true,
                FirstName = "Admin",
                LastName = "Strator",
                PhoneNumber = "072 222 23 34"
            };
            await userManager.CreateAsync(admin, "Admin123!");
            await userManager.AddToRoleAsync(admin, "Administrator");

            User organizer = new User()
            {
                UserName = "Organizer",
                Email = "organizer@organizer.com",
                EmailConfirmed = true,
                FirstName = "Org",
                LastName = "Anizer",
                PhoneNumber = "072 888 64 71"
            };
            await userManager.CreateAsync(organizer, "Org123!");
            await userManager.AddToRoleAsync(organizer, "Organizer");

            // Vanlig User
            User user = new User()
            {
                UserName = "testuser",
                Email = "test@test.com",
                EmailConfirmed = true,
                FirstName = "Test",
                LastName = "User",
                PhoneNumber = "070 666 40 45"                
            };
            await userManager.CreateAsync(user, "Losenord123!");
            await userManager.AddToRoleAsync(user, "Attendee");

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
                    Organizer=organizer
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
                    Organizer=organizer
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
                    Organizer=organizer
                }
            };
            

            await AddRangeAsync(events);
            await SaveChangesAsync();
        }
    }
}
