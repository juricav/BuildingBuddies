using BuildingBuddies.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
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
                new Meeting{Link="https://localhost:44315/Identity/Account/Register/fdsgef", Domain = "erstebank.com", CompanyName ="Erste banka", EndDate = DateTime.Parse("2018-07-06"), MeetingEnded = false },
                new Meeting{Link="https://localhost:44315/Identity/Account/Register/fhbrg", Domain = "otp.com", CompanyName ="OTP banka", EndDate = DateTime.Parse("2018-08-25"), MeetingEnded = false }
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

            // nema potrebe kad radimo nove
            //var agreedMeetings = new AgreedMeeting[]
            //{
            //    new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID, Link = "VfPjttgdLn"},
            //    new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID, Link = "rub29DFWRr"}
            //};
            //foreach(AgreedMeeting am in agreedMeetings)
            //{
            //    context.AgreedMeeting.Add(am);
            //}
            //context.SaveChanges();           
            
            var users = new User[]
            {
                new User{Email="email1@erstebank.com", UserName="ananas", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email2@erstebank.com", UserName="banana", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email3@erstebank.com", UserName="jabuka", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email4@erstebank.com", UserName="kruska", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email5@erstebank.com", UserName="gljiva", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email6@erstebank.com", UserName="sljiva", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email7@erstebank.com", UserName="perec", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email1@otp.com", UserName="kifla", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email2@otp.com", UserName="krafna", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email3@otp.com", UserName="piroska", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email4@otp.com", UserName="prstic", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email5@otp.com", UserName="prskalica", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email6@otp.com", UserName="kojot", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email7@otp.com", UserName="nosorog", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email8@otp.com", UserName="hijena", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email9@otp.com", UserName="pas", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/},
                new User{Email="email10@otp.com", UserName="mrkva", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID/*, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID*/}
            };            

            foreach (User u in users)
            {
                var password = new PasswordHasher<User>();
                var hashed = password.HashPassword(u, "DebelaZirafa1!");
                u.NormalizedEmail = u.Email.ToUpper();
                u.NormalizedUserName = u.UserName.ToUpper();
                u.EmailConfirmed = true;
                u.SecurityStamp = Guid.NewGuid().ToString("D");
                u.PasswordHash = hashed;
                context.User.Add(u);
            }
            context.SaveChanges();

            //var chatMessages = new ChatMessage[]
            //{
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-07 19:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "ananas").UserName, Message="khet dsadas", UserId = users.Single(u => u.UserName == "ananas").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-07 19:28", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "banana").UserName, Message="ou89oi8 dsadafgad", UserId = users.Single(u => u.UserName == "banana").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-07 20:11", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "ananas").UserName, Message="ggrwfgws", UserId = users.Single(u => u.UserName == "ananas").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-07 20:56", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "banana").UserName, Message="h7uie", UserId = users.Single(u => u.UserName == "banana").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-07 23:45", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "nosorog").UserName, Message="bh6uteh5ok", UserId = users.Single(u => u.UserName == "nosorog").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-08 07:26", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "hijena").UserName, Message="zh5eu65", UserId = users.Single(u => u.UserName == "hijena").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-08 14:07", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "nosorog").UserName, Message="buz5zr54ok", UserId = users.Single(u => u.UserName == "nosorog").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
            //    new ChatMessage{Time= DateTime.ParseExact("2018-08-09 08:33", "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture), Name= users.Single(u => u.UserName == "hijena").UserName, Message="u5u6ijičćpo", UserId = users.Single(u => u.UserName == "hijena").Id, AgreedMeetingID= agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID}
            //};
            //foreach(ChatMessage cm in chatMessages)
            //{
            //    context.ChatMessage.Add(cm);
            //}
            //context.SaveChanges();
    }
    }
}
