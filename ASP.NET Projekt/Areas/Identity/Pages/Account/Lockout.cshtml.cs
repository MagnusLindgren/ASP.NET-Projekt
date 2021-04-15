using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Projekt.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LockoutModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public LockoutModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public User ThisUser { get; set; }
        public async Task OnGetAsync(string id)
        {
            ThisUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == id);            
        }
    }
}
