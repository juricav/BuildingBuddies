using System.Collections.Generic;

namespace BuildingBuddies.Models
{
    public class AgreedMeeting
    {
        public int AgreedMeetingID { get; set; }
        public string Link { get; set; }

        public int MeetingID { get; set; }

        public Meeting Meeting { get; set; }
        public ICollection<User> Users { get; set; }
        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
