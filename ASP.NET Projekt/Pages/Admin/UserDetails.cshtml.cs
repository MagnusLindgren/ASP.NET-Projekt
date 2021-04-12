using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;

namespace ASP.NET_Projekt.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class UserDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
       
        public UserDetailsModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;           
        }

        [BindProperty]
        public User User { get; set; }
        public IList<Event> UserEvents { get; set; }
        public IList<string> Roles { get; set; }

        public async Task<IActionResult> OnGetAsync(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            Roles = await _userManager.GetRolesAsync(User);

            var attendee = await _context.Users.Include(a => a.JoinedEvents).FirstOrDefaultAsync();
            UserEvents = attendee.JoinedEvents;

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string id, bool? BanUser)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var newUserDetails = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

            newUserDetails.UserName = User.UserName;
            newUserDetails.FirstName = User.FirstName;
            newUserDetails.LastName = User.LastName;
            newUserDetails.Email = User.Email;
            newUserDetails.PhoneNumber = User.PhoneNumber;

            if (BanUser ?? false)
            {
                newUserDetails.LockoutEnd = DateTime.Now.AddDays(9999);
                newUserDetails.LockoutEnabled = true;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
           
           

            return RedirectToPage("./UserDetails", new { id = id });
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
