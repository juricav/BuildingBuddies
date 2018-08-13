using Microsoft.AspNetCore.Identity;

namespace BuildingBuddies.Models
{
    public class User : IdentityUser
    {
        public int? DepartmentID { get; set; }
        public int? MeetingID { get; set; }
        public int? AgreedMeetingID { get; set; }
        
        public Department Department { get; set; }
        public Meeting Meeting { get; set; }
        public AgreedMeeting AgreedMeeting { get; set; }
    }
}
