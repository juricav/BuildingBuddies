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
    public class DetailsModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public DetailsModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

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
    }
}
