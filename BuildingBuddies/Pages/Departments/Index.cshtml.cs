using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Departments
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

        public IList<Department> Department { get;set; }

        public async Task OnGetAsync()
        {
            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User
                                            .Where(u => u.NormalizedUserName == UserName.ToUpper())
                                            .FirstOrDefault();

                Department = await _context.Department
                                                .Where(d => d.Meeting.CreatorID == LoggedUser.Id)
                                                .Include(d => d.Meeting).ToListAsync();

            }

            
        }
    }
}
