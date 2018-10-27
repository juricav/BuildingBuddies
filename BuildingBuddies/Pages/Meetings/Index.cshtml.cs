using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Meetings
{
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public IndexModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        public IList<Meeting> Meeting { get; set; }

        public async Task OnGetAsync()
        {
            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();
                var MeetingOrganizer = LoggedUser.MeetingOrganizer;

                if (MeetingOrganizer == true)
                {
                    // vraćamo samo Meetinge tog korisnika
                    Meeting = await _context.Meeting.Where(m => m.CreatorID == LoggedUser.Id).ToListAsync();
                }
            }
        }
    }
}
