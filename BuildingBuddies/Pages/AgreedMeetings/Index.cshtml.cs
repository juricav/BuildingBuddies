using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.AgreedMeetings
{
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public IndexModel(BuildingBuddiesContext context)
        {
            _context = context;
        }
        
        public IList<AgreedMeeting> AgreedMeeting { get; set; }

        public async Task OnGetAsync()
        {
            AgreedMeeting = await _context.AgreedMeeting
                .Include(a => a.Meeting).ToListAsync();
        }
    }
}
