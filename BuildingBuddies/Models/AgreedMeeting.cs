using System.Collections.Generic;

namespace BuildingBuddies.Models
{
    public class AgreedMeeting
    {
        public int AgreedMeetingID { get; set; }
        public string Link { get; set; }

        public int MeetingID { get; set; }

        public Meeting Meeting { get; set; }

        private ICollection<User> users;

        public ICollection<User> GetUsers()
        {
            return users;
        }

        public void SetUsers(ICollection<User> value)
        {
            users = value;
        }

        public ICollection<ChatMessage> ChatMessages { get; set; }
    }
}
