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
using Microsoft.AspNetCore.Identity;

namespace ASP.NET_Projekt.Pages.Events
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;


        public DetailsModel(ApplicationDbContext context,
            UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public Event Event { get; set; }

        [BindProperty]
        public bool AttendeeIsAttending { get; set; } = false;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Event = await _context.Events.Include(o => o.Organizer).FirstOrDefaultAsync(m => m.Id == id);

            var userId = _userManager.GetUserId(User);
            var attendee = await _context.Users
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            var getUserJoinedEvents = await _context.Events.Include(a => a.Attendees).FirstOrDefaultAsync(m => m.Id == id);

            if (getUserJoinedEvents.Attendees.Contains(attendee))
            {
                AttendeeIsAttending = true;
            }

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

            var userId = _userManager.GetUserId(User);
            var attendee = await _context.Users
                .Where(a => a.Id == userId)
                .FirstOrDefaultAsync();

            if (!Event.Attendees.Contains(attendee))
            {
            Event.SpotsAvailable--;
            Event.Attendees.Add(attendee);
            await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Details", new { id = id });
        }
    }
}
