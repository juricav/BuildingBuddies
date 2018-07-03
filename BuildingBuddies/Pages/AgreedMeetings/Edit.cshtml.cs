using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.AgreedMeetings
{
    public class EditModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public EditModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public AgreedMeeting AgreedMeeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AgreedMeeting = await _context.AgreedMeeting
                .Include(a => a.Meeting).FirstOrDefaultAsync(m => m.AgreedMeetingID == id);

            if (AgreedMeeting == null)
            {
                return NotFound();
            }
           ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "MeetingID");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(AgreedMeeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgreedMeetingExists(AgreedMeeting.AgreedMeetingID))
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

        private bool AgreedMeetingExists(int id)
        {
            return _context.AgreedMeeting.Any(e => e.AgreedMeetingID == id);
        }
    }
}
