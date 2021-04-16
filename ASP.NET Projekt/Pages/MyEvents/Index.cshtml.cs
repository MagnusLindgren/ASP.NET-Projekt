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
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public IndexModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IList<Event> Events { get;set; }

        public async Task OnGetAsync()
        {
            var userId = await _userManager.GetUserAsync(User);
            var attendee = await _context.Users
                .Where(a => a.Id == userId.Id)
                .Include(a => a.JoinedEvents)
                .FirstOrDefaultAsync();

            Events = attendee.JoinedEvents;

        }
    }
}
