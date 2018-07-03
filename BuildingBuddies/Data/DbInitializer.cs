using BuildingBuddies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                new Meeting{Link="www.buildingbuddies.com/1", Domain = "erstebank.com", CompanyName ="Erste banka", StartDate=DateTime.Parse("2018-07-01"), EndDate = DateTime.Parse("2018-07-20") },
                new Meeting{Link="www.buildingbuddies.com/2", Domain = "otp.com", CompanyName ="OTP banka", StartDate=DateTime.Parse("2018-08-01"), EndDate = DateTime.Parse("2018-08-20") }
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
                new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "erstebank.com").MeetingID},
                new AgreedMeeting{MeetingID=meetings.Single(m => m.Domain == "otp.com").MeetingID}
            };
            foreach(AgreedMeeting am in agreedMeetings)
            {
                context.AgreedMeeting.Add(am);
            }
            context.SaveChanges();

            var users = new User[]
            {
                new User{Domain="erstebank.com", Email="email1@erstebank.com", Username="ananas", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email2@erstebank.com", Username="banana", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email3@erstebank.com", Username="jabuka", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email4@erstebank.com", Username="kruska", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email5@erstebank.com", Username="gljiva", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email6@erstebank.com", Username="sljiva", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="erstebank.com", Email="email7@erstebank.com", Username="perec", Password="123456", DepartmentID=departments.Single(d => d.Name == "eSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "erstebank.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email1@otp.com", Username="kifla", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email2@otp.com", Username="krafna", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email3@otp.com", Username="piroska", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email4@otp.com", Username="prstic", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 1").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 1").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email5@otp.com", Username="prskalica", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email6@otp.com", Username="kojot", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email7@otp.com", Username="nosorog", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email8@otp.com", Username="hijena", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 2").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 2").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email9@otp.com", Username="pas", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
                new User{Domain="otp.com", Email="email10@otp.com", Username="mrkva", Password="123456", DepartmentID=departments.Single(d => d.Name == "oSlužba 3").DepartmentID, MeetingID=departments.Single(d => d.Name == "eSlužba 3").MeetingID, AgreedMeetingID=agreedMeetings.Single(am => am.MeetingID == meetings.Single(m => m.Domain == "otp.com").MeetingID).AgreedMeetingID},
            };
            foreach(User u in users)
            {
                context.User.Add(u);
            }
            context.SaveChanges();

            
        }
    }
}
