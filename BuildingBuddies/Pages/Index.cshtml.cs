using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BuildingBuddies.Pages
{
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public IndexModel(BuildingBuddiesContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            //MeetingGenerator mg = new MeetingGenerator(_context);
            //RecurringJob.AddOrUpdate("uniqueId", () => mg.DailyBatch(), Cron.Daily);

            MailSender ms = new MailSender();

            await ms.Send("jurica.smail@gmail.com", "test1", "testb");
        }
    }
}
