using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ASP.NET_Projekt.Data;
using ASP.NET_Projekt.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ASP.NET_Projekt.Pages
{
    public class ContactModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public ContactModel(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        [BindProperty]
        public ContactFormModel Contact { get; set; }
        public User ThisUser { get; set; }

        public class ContactFormModel
        {
            [Required]
            public string OrganizationName { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public string Email { get; set; }
            [Required]
            public string Message { get; set; }
        }
        public async Task OnGetAsync()
        {
            /*
            var userId = _userManager.GetUserId(User);
            ThisUser = await _context.Users.FirstOrDefaultAsync(m => m.Id == userId);
            */
        }

        public void OnPost()
        {
            if (!ModelState.IsValid)
            {
                Page();
            }

            /*
            var mailbody = $@"Hello Event Organizer Admin,

                            This is a new Organizer request:

                            Organization: {Contact.OrganizationName}
                            FirstName: {Contact.FirstName}
                            LastName: {Contact.LastName}
                            Email: {Contact.Email}
                            Message: ""{Contact.Message}""
                            ";

            SendMail(mailbody);

            RedirectToPage("/Index");
            */
        }

        private void SendMail(string mailbody)
        {
            /*
            using (var message = new MailMessage(Contact.Email, "mangecl@hotmail.com"))
            {
                message.To.Add(new MailAddress("mangecl@hotmail.com"));
                message.From = new MailAddress(Contact.Email);
                message.Subject = "New Organizer Request";
                message.Body = mailbody;

                using (var smtpClient = new SmtpClient("smtp-mail.outlook.com", 587))
                {
                    smtpClient.Send(message);
                }
            
            }*/
        }
    }
}
