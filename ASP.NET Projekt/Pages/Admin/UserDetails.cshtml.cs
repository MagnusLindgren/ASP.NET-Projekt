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

namespace ASP.NET_Projekt.Pages.Admin
{
    [Authorize(Roles = "Administrator")]
    public class UserDetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public UserDetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public User User { get; set; }
        public IList<Event> UserEvents { get; set; }

        public async Task<IActionResult> OnGetAsync(string? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);

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
        public async Task<IActionResult> OnPostAsync(bool? BanUser)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (BanUser ?? false)
            {
                User.LockoutEnd = DateTime.Now.AddDays(9999);
                User.LockoutEnabled = true;
            }

            _context.Attach(User).State = EntityState.Modified;

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

            return RedirectToPage("./Index");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
