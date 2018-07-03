using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.Users
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
        ViewData["AgreedMeetingID"] = new SelectList(_context.AgreedMeeting, "AgreedMeetingID", "AgreedMeetingID");
        ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "DepartmentID");
        ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "MeetingID");
            return Page();
        }

        [BindProperty]
        public User User { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.User.Add(User);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}