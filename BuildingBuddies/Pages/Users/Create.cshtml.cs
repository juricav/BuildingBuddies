using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BuildingBuddies.Models;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Helpers;

namespace BuildingBuddies.Pages.Users
{
    public class CreateModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public CreateModel(BuildingBuddiesContext context)
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
        public new User User { get; set; }

        public async Task<IActionResult> OnPostAsync(string meetingLink)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            Meeting SourceMeeting = await _context.Meeting.Where(m => m.Link.Contains(meetingLink)).FirstOrDefaultAsync();
            User ExistingUser = await _context.User.Where(u => u.Email.Equals(User.Email)).FirstOrDefaultAsync();
            
            if(SourceMeeting.Domain != User.Email.Split('@')[1] || !(ExistingUser is null))
            {
                return Page();
            }

            User.MeetingID = SourceMeeting.MeetingID;
            User.Username = LinkGenerator.GenerateRandomString(2);

            _context.User.Add(User);
            await _context.SaveChangesAsync();
            
            return RedirectToPage("./Details", new { id = User.UserID });
        }
    }
}