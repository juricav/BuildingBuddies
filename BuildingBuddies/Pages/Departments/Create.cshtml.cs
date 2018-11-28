using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Departments
{
    public class CreateModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public CreateModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        public IActionResult OnGet()
        {
            SetMenuItems();

            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();

                ViewData["MeetingID"] = new SelectList(_context.Meeting.Where(m => m.CreatorID == LoggedUser.Id).ToList(), "MeetingID", "Name");

            }

            return Page();
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            SetMenuItems();

            if (Department.Meeting == null)
            {
                ModelState.AddModelError("Department is empty", "Please, choose or create a Meeting first.");
            }
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var meeting = _context.Meeting.Where(m => m.MeetingID == Department.Meeting.MeetingID).FirstOrDefault();
            Department.Meeting = meeting;
            Department.MeetingID = meeting.MeetingID;

            _context.Department.Add(Department);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

        public void SetMenuItems()
        {
            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();
                var MeetingOrganizer = LoggedUser.MeetingOrganizer;

                var connected = false;

                if (LoggedUser.AgreedMeetingID != null)
                {
                    connected = true;
                }

                ViewData.Add("Connected", connected);
                ViewData.Add("MeetingOrganizer", MeetingOrganizer);
            }
        }
    }
}