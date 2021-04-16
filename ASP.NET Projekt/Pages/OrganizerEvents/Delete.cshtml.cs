using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;

namespace ASP.NET_Projekt.Pages.OrganizerEvents
{
    public class DeleteModel : PageModel
    {
        private readonly ASP.NET_Projekt.Data.ApplicationDbContext _context;

        public DeleteModel(ASP.NET_Projekt.Data.ApplicationDbContext context)
        {
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

            Event = await _context.Events.FirstOrDefaultAsync(m => m.Id == id);

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

            Event = await _context.Events.FindAsync(id);

            if (Event != null)
            {
                try
                {
                    _context.Events.Remove(Event);
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
