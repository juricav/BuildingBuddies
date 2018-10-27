using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace BuildingBuddies.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public IndexModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        public ActionResult OnGet()
        {
            MeetingGenerator MeetingGenerator = new MeetingGenerator(_context);
            RecurringJob.AddOrUpdate("uniqueId", () => MeetingGenerator.DailyBatch(), Cron.Daily);

            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();
                var MeetingOrganizer = LoggedUser.MeetingOrganizer;

                if (MeetingOrganizer == true)
                {
                    ViewData.Add("MeetingOrganizer", true);
                }
                else
                {
                    ViewData.Add("MeetingOrganizer", false);
                }
            }
            return Page();
        }
    }

    
}
