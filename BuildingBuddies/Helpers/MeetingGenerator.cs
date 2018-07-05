using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Helpers
{
    public class MeetingGenerator : PageModel
    {
        private static Random rng = new Random((int)DateTime.Now.Ticks);

        private readonly BuildingBuddiesContext _context;

        public MeetingGenerator(BuildingBuddiesContext context)
        {
            _context = context;
        }

        public async Task ConnectUsers (int meetingId)
        {
            List<User> FreeUsers = await _context.User.Where(u => u.MeetingID == meetingId)
                                                        .OrderBy(u => rng.Next())
                                                        .ToListAsync();

            if(FreeUsers.Count % 2 != 0)
            {
                FreeUsers.RemoveAt(FreeUsers.Count - 1); // mičemo zadnjeg da ih bude paran broj
            }

            for(int i = 0; i < FreeUsers.Count(); i+=2)
            {
                // generiramo AgreedMeeting i upisujemo njegov ID u 2 korisnika i šaljemo im mail
                AgreedMeeting AgreedMeeting = new AgreedMeeting();
                AgreedMeeting.MeetingID = meetingId;

                _context.AgreedMeeting.Add(AgreedMeeting);

                _context.SaveChanges();

                AgreedMeeting NewAgreedMeeting = _context.AgreedMeeting.Where(am => am.MeetingID == meetingId).LastOrDefault();

                User FirstUser = (from x in _context.User
                           where x.UserID == FreeUsers[i].UserID
                           select x).First();
                User SecondUser = (from x in _context.User
                                  where x.UserID == FreeUsers[i+1].UserID
                                  select x).First();

                FirstUser.AgreedMeetingID = NewAgreedMeeting.AgreedMeetingID;
                SecondUser.AgreedMeetingID = NewAgreedMeeting.AgreedMeetingID;

                _context.SaveChanges();
            }
            
            MailSender MailSender = new MailSender();

            foreach(User u in FreeUsers)
            {
                string SecondUsername = (from x in _context.User
                               where x.AgreedMeetingID == u.AgreedMeetingID && x.UserID != u.UserID
                               select x).First().Username;
                await MailSender.Send("jurica.smail@gmail.com", "Dragi " + u.Username, "Spojeni ste s korisnikom " + SecondUsername);
            }
        }

        public async Task DailyBatch()
        {
            // gledamo istekle sastanke
            List<Meeting> Meetings = await _context.Meeting.Where(m => m.EndDate < DateTime.Now)
                                                        .ToListAsync();

            if(Meetings.Count() > 0)
            {
                foreach (Meeting m in Meetings)
                {
                    await ConnectUsers(m.MeetingID);
                }
            }
        }
    }
}
