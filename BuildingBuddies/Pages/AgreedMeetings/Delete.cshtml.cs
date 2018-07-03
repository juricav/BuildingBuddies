using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.AgreedMeetings
{
    public class DeleteModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public DeleteModel(BuildingBuddies.Models.BuildingBuddiesContext context)
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            AgreedMeeting = await _context.AgreedMeeting.FindAsync(id);

            if (AgreedMeeting != null)
            {
                _context.AgreedMeeting.Remove(AgreedMeeting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
