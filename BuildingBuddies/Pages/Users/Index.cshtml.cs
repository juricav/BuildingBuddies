using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Hangfire;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.Users
{
    public class IndexModel : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public IndexModel(BuildingBuddiesContext context)
        {
            _context = context;
        }

        public IList<User> User { get;set; }

        public async Task OnGetAsync()
        {
            User = await _context.User
                .Include(u => u.AgreedMeeting)
                .Include(u => u.Department)
                .Include(u => u.Meeting).ToListAsync();

            //MailSender ms = new MailSender();
            //await ms.Send("jurica313@gmail.com", "nehangfire test", DateTime.Now.ToShortDateString());

            MeetingGenerator mg = new MeetingGenerator();
            int b = 1;
            await mg.ConnectUsers(b, _context);

            //MailSender MailSender = new MailSender();
            //await MailSender.Send("jurica.smail@gmail.com", "Spojeni ste", "weee");
            

            //RecurringJob.AddOrUpdate(() => Console.WriteLine(""), Cron.Minutely);
        }
    }
}
