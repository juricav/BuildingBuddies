using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.AgreedMeetings
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
        ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "MeetingID");
            return Page();
        }

        [BindProperty]
        public AgreedMeeting AgreedMeeting { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.AgreedMeeting.Add(AgreedMeeting);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}