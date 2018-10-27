using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Meetings
{
    public class DeleteModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public DeleteModel(BuildingBuddiesContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Meeting = await _context.Meeting.FindAsync(id);

            if (Meeting != null)
            {
                _context.Meeting.Remove(Meeting);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
