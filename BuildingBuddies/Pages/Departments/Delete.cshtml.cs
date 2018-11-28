using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Departments
{
    public class DeleteModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public DeleteModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            SetMenuItems();

            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Department.FindAsync(id);

            if (Department != null)
            {
                _context.Department.Remove(Department);
                await _context.SaveChangesAsync();
            }
            else
            {
                return NotFound();
            }

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
