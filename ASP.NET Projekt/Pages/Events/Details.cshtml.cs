using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Authorization;

namespace ASP.NET_Projekt.Pages.Events
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ASP.NET_Projekt.Data.ApplicationDbContext _context;

        public DetailsModel(ASP.NET_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Event Event { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.Include(o => o.Organizer).FirstOrDefaultAsync(m => m.Id == id);

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

            Event = await _context.Events.Include(u => u.Attendees).FirstOrDefaultAsync(m => m.Id == id);

            if (Event == null)
            {
                return NotFound();
            }

            var attendee = await _context.Users.FirstOrDefaultAsync();
            Event.SpotsAvailable--;
            Event.Attendees.Add(attendee);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = id });
        }
    }
}
