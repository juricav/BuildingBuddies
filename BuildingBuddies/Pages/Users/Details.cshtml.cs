using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using BuildingBuddies.Models;

namespace BuildingBuddies.Pages.Users
{
    public class DetailsModel : PageModel
    {
        private readonly BuildingBuddies.Models.BuildingBuddiesContext _context;

        public DetailsModel(BuildingBuddies.Models.BuildingBuddiesContext context)
        {
            _context = context;
        }
        
        public new User User { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            User = await _context.User
                .Include(u => u.AgreedMeeting)
                .Include(u => u.Department)
                .Include(u => u.Meeting).FirstOrDefaultAsync(m => m.UserID == id);

            if (User == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
