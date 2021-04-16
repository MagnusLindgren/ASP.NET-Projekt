using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Projekt.Pages.MyEvents
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public DeleteModel(ASP.NET_Projekt.Data.ApplicationDbContext context,
                       UserManager<User> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events
                .OrderBy(e => e.Date)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userId = _userManager.GetUserId(User);

            var getUserJoinedEvents = await _context.Users
                .Where(u => u.Id == userId)
                .Include(j => j.JoinedEvents)
                .FirstOrDefaultAsync();

            Event = await _context.Events
                .Where(e => e.Id == id)
                .FirstOrDefaultAsync();

            getUserJoinedEvents.JoinedEvents.Remove(Event);
            Event.SpotsAvailable++;

            if (Event != null)
            {
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {

                    return RedirectToPage("/Error");
                }
                
            }

            return Page();
        }
    }
}
