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

        public async Task ConnectUsers(int meetingId)
        {
            List<User> FreeUsers = await _context.User.Where(u => u.MeetingID == meetingId && u.MeetingOrganizer == false)
                                                        .OrderBy(u => rng.Next())
                                                        .ToListAsync();

            if (FreeUsers.Count % 2 != 0)
            {
                FreeUsers.RemoveAt(FreeUsers.Count - 1); // mičemo zadnjeg da ih bude paran broj
            }

            for (int i = 0; i < FreeUsers.Count(); i += 2)
            {
                // generiramo AgreedMeeting i upisujemo njegov ID u 2 korisnika i šaljemo im mail
                AgreedMeeting AgreedMeeting = new AgreedMeeting
                {
                    MeetingID = meetingId
                };

                _context.AgreedMeeting.Add(AgreedMeeting);
                _context.SaveChanges();

                AgreedMeeting NewAgreedMeeting = _context.AgreedMeeting.Where(am => am.MeetingID == meetingId).LastOrDefault();

                User FirstUser = (from x in _context.User
                                  where x.Id == FreeUsers[i].Id
                                  select x).First();
                User SecondUser = (from x in _context.User
                                   where x.Id == FreeUsers[i + 1].Id && x.DepartmentID != FirstUser.DepartmentID && x.MeetingID == FirstUser.MeetingID
                                   select x).First();

                FirstUser.AgreedMeetingID = NewAgreedMeeting.AgreedMeetingID;
                SecondUser.AgreedMeetingID = NewAgreedMeeting.AgreedMeetingID;

                _context.SaveChanges();
            }

            MailSender MailSender = new MailSender();

            foreach (User u in FreeUsers)
            {
                string SecondUsername = (from x in _context.User
                                         where x.AgreedMeetingID == u.AgreedMeetingID && x.Id != u.Id
                                         select x).First().UserName;

                await MailSender.Send(u.Email,
                                    $"You have been connected with a buddy",
                                    $"Dear {u.UserName}, <br/>your buddy is {SecondUsername}! You can log in <a href='https://localhost:44315/signalr'>here</a> to agree on a meeting place and time.");
            }
        }

        public async Task DailyBatch()
        {
            // gledamo sastanke kojima je prošao dan kraja i nisu već obrađeni
            List<Meeting> Meetings = await _context.Meeting.Where(m => m.EndDate < DateTime.Now && m.MeetingEnded != true)
                                                        .ToListAsync();

            if (Meetings.Count() > 0)
            {
                foreach (Meeting m in Meetings)
                {
                    await ConnectUsers(m.MeetingID);

                    m.MeetingEnded = true;
                    _context.Meeting.Update(m);
                }
                _context.SaveChanges();
            }

            // dohvaća sastanke koji su istekli prije 2 tjedna i briše korisnike
            List<Meeting> ExpiredMeetings = await _context.Meeting.Where(m => m.EndDate >= m.EndDate.AddDays(14) && m.MeetingEnded == true)
                                                            .ToListAsync();

            if (ExpiredMeetings.Count() > 0)
            {
                foreach (var m in ExpiredMeetings)
                {
                    await DeleteUsers(m.MeetingID);
                }
                _context.SaveChanges();
            }
        }

        public async Task DeleteUsers(int meetingID)
        {
            List<User> UsersToDelete = await _context.User.Where(u => u.MeetingID == meetingID && u.MeetingOrganizer == false)
                                                        .OrderBy(u => rng.Next())
                                                        .ToListAsync();

            _context.User.RemoveRange(UsersToDelete);
        }
    }
}
