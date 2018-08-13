using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Hangfire;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildingBuddies.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public IndexModel(BuildingBuddiesContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            MeetingGenerator MeetingGenerator = new MeetingGenerator(_context);
            RecurringJob.AddOrUpdate("uniqueId", () => MeetingGenerator.DailyBatch(), Cron.Daily);
        }
    }
}
