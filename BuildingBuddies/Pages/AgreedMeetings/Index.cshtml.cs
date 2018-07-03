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
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public IndexModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }

        public IList<AgreedMeeting> AgreedMeeting { get;set; }

        public async Task OnGetAsync()
        {
            AgreedMeeting = await _context.AgreedMeeting
                .Include(a => a.Meeting).ToListAsync();
        }
    }
}
