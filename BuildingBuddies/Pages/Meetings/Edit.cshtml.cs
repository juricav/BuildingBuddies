using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Meetings
{
    public class EditModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public EditModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        [BindProperty]
        public Meeting Meeting { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();
                var MeetingOrganizer = LoggedUser.MeetingOrganizer;

                ViewData.Add("MeetingOrganizer", MeetingOrganizer);

                var connected = false;

                if (LoggedUser.AgreedMeetingID != null)
                {
                    connected = true;
                }

                ViewData.Add("Connected", connected);
            }

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

            if (currentMeeting.EndDate >= Meeting.EndDate)
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
