using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildingBuddies.Models;
using BuildingBuddies.Helpers;

namespace BuildingBuddies.Pages.Meetings
{
    public class CreateModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public CreateModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Meeting.Link = "https://localhost:44315/Users/Create/"
                        + LinkGenerator.GenerateRandomString(10);
            Meeting.MeetingEnded = false;

            _context.Meeting.Add(Meeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Details", new { id = Meeting.MeetingID });
        }
    }
}