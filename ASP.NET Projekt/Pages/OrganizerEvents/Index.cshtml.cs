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

namespace ASP.NET_Projekt.Pages.OrganizerEvents
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

        public IList<Event> Event { get;set; }

        public async Task OnGetAsync()
        {
            var organizerId = _userManager.GetUserId(User);

            Event = await _context.Events
                .Where(o => o.Organizer.Id == organizerId)
                .ToListAsync();
        }
    }
}
