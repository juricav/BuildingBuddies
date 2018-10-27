using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Linq;

namespace BuildingBuddies.Pages
{
    public class CountdownModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public CountdownModel(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        public void OnGet()
        {
            var UserName = _iHttpContext.HttpContext.User.Identity.Name;

            if (UserName != null)
            {
                var LoggedUser = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).FirstOrDefault();

                var meeting = _context.Meeting.Where(m => m.MeetingID == LoggedUser.MeetingID).FirstOrDefault();

                var remainingDays = meeting.EndDate.Subtract(DateTime.Now.Date).Days;

                ViewData["remainingDays"] = remainingDays;
            }
        }
    }
}