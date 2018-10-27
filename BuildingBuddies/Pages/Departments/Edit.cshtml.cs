using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Departments
{
    public class EditModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public EditModel(BuildingBuddiesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Department Department { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Department = await _context.Department
                .Include(d => d.Meeting).FirstOrDefaultAsync(m => m.DepartmentID == id);

            if (Department == null)
            {
                return NotFound();
            }
            ViewData["MeetingID"] = new SelectList(_context.Meeting, "MeetingID", "Name");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Department).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DepartmentExists(Department.DepartmentID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool DepartmentExists(int id)
        {
            return _context.Department.Any(e => e.DepartmentID == id);
        }
    }
}
