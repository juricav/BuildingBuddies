using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.Meetings
{
    public class EditModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public EditModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meeting.FirstOrDefaultAsync(m => m.MeetingID == id);

            if (Meeting == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentMeeting = _context.Meeting.Where(m => m.MeetingID == Meeting.MeetingID).FirstOrDefault();

            if(currentMeeting.EndDate >= Meeting.EndDate)
            {
                ModelState.AddModelError("Wrong end date", "The end date can't be moved back");
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Meeting).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MeetingExists(Meeting.MeetingID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("/Details", new { id = Meeting.MeetingID });
        }

        private bool MeetingExists(int id)
        {
            return _context.Meeting.Any(e => e.MeetingID == id);
        }
    }
}
