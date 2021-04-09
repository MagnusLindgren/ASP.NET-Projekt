using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;

namespace ASP.NET_Projekt.Pages.MyEvents
{
    public class IndexModel : PageModel
    {
        private readonly ASP.NET_Projekt.Data.ApplicationDbContext _context;

        public IndexModel(ASP.NET_Projekt.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Event> Events { get;set; }

        public async Task OnGetAsync()
        {
            var attendee = await _context.Users.Include(a => a.JoinedEvents).FirstOrDefaultAsync();
            Events = attendee.JoinedEvents;

        }
    }
}
