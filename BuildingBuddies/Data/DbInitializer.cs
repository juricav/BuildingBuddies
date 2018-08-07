using BuildingBuddies.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BuildingBuddies.Data
{
    public class DbInitializer
    {
        public static void Initialize (BuildingBuddiesContext context)
        {
            //context.Database.EnsureCreated();

            // traži sastanke
            if(context.Meeting.Any())
            {
                return; // sastanci postoje- nije potreban seed
            }

            var meetings = new Meeting[]
            {
                new Meeting{Link="https://localhost:44315/Users/Create/fdsgef", Domain = "erstebank.com", CompanyName ="Erste banka", EndDate = DateTime.Parse("2018-07-06"), MeetingEnded = false },
                new Meeting{Link="https://localhost:44315/Users/Create/fhbrg", Domain = "otp.com", CompanyName ="OTP banka", EndDate = DateTime.Parse("2018-08-20"), MeetingEnded = false }
            };
            foreach(Meeting m in meetings)
            {
                context.Meeting.Add(m);
            }
            context.SaveChanges();

            var departments = new Department[]
            {
                new Department{Name="eSlužba 1", MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID},
                new Department{ Name="eSlužba 2", MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID},
                new Department{Name="eSlužba 3", MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID},
                new Department{Name="oSlužba 1", MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID},
                new Department{Name="oSlužba 2", MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID},
                new Department{Name="oSlužba 3", MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID},
            };
            foreach(Department d in departments)
            {
                context.Department.Add(d);
            }
            context.SaveChanges();

            var agreedMeetings = new AgreedMeeting[]
            {
                new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID, Link = "VfPjttgdLn"},
                new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID, Link = "rub29DFWRr"}
            };
            foreach(AgreedMeeting am in agreedMeetings)
            {
                context.AgreedMeeting.Add(am);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{Email="email1@erstebank.com", Username="ananas", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email2@erstebank.com", Username="banana", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email3@erstebank.com", Username="jabuka", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email4@erstebank.com", Username="kruska", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email5@erstebank.com", Username="gljiva", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email6@erstebank.com", Username="sljiva", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email7@erstebank.com", Username="perec", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Email="email1@otp.com", Username="kifla", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email2@otp.com", Username="krafna", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email3@otp.com", Username="piroska", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email4@otp.com", Username="prstic", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email5@otp.com", Username="prskalica", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email6@otp.com", Username="kojot", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email7@otp.com", Username="nosorog", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email8@otp.com", Username="hijena", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email9@otp.com", Username="pas", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Email="email10@otp.com", Username="mrkva", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
            };
            foreach(User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();

            var chatMessages = new ChatMessage[]
            {
                new ChatMessage{Time= DateTime.ParseExact("2018-08-07 19:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "ananas").Username, Message="khet dsadas", UserID = users.Single(u => u.Username == "ananas").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-07 19:28", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "banana").Username, Message="ou89oi8 dsadafgad", UserID = users.Single(u => u.Username == "banana").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-07 20:11", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "ananas").Username, Message="ggrwfgws", UserID = users.Single(u => u.Username == "ananas").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-07 20:56", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "banana").Username, Message="h7uie", UserID = users.Single(u => u.Username == "banana").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-07 23:45", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "nosorog").Username, Message="bh6uteh5ok", UserID = users.Single(u => u.Username == "nosorog").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-08 07:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "hijena").Username, Message="zh5eu65", UserID = users.Single(u => u.Username == "hijena").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-08 14:07", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "nosorog").Username, Message="buz5zr54ok", UserID = users.Single(u => u.Username == "nosorog").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new ChatMessage{Time= DateTime.ParseExact("2018-08-09 08:33", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.Username == "hijena").Username, Message="u5u6ijičćpo", UserID = users.Single(u => u.Username == "hijena").UserID, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID}
            };
            foreach(ChatMessage cm in chatMessages)
            {
                context.ChatMessage.Add(cm);
            }
            context.SaveChanges();
    }
    }
}
