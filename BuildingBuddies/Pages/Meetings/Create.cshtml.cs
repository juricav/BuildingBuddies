using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Meetings
{
    public class CreateModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public CreateModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Meeting.EndDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("Wrong End date", "End date cannot be set in the past.");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();

                Meeting.Link = "https://localhost:44315/Identity/Account/Register/"
                        + LinkGenerator.GenerateRandomString(15);
                Meeting.MeetingEnded = false;
                Meeting.CreatorID = LoggedUser.Id;
                Meeting.Domain = LoggedUser.Email.Split("@").LastOrDefault();

                _context.Meeting.Add(Meeting);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Details", new { id = Meeting.MeetingID });
            }

            return Page();
        }
    }
}