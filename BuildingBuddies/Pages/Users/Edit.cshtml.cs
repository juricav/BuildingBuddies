using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.Users
{
    public class EditModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public EditModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User
                .Include(u => u.AgreedMeeting)
                .Include(u => u.Department)
                .Include(u => u.Meeting).FirstOrDefaultAsync(m => m.UserID == id);

            if (User == null)
            {
                return NotFound();
            }
           ViewData["AgreedMeetingID"] = new SelectList(_context.AgreedMeeting, "AgreedMeetingID", "AgreedMeetingID");
           ViewData["DepartmentID"] = new SelectList(_context.Department, "DepartmentID", "DepartmentID");
           ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "MeetingID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(User).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(User.UserID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.UserID == id);
        }
    }
}
