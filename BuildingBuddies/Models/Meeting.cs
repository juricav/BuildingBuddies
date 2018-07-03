using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Models
{
    public class Meeting
    {
        public int MeetingID { get; set; }
        public string Link { get; set; }
        public string Domain { get; set; }
        public string CompanyName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<AgreedMeeting> AgreedMeetings { get; set; }
    }
}
