namespace BuildingBuddies.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        // generator random usernameova
        public string Username { get; set; }
        // dodati hash
        public string Password { get; set; }
        
        public int? DepartmentID { get; set; }
        public int MeetingID { get; set; }
        public int? AgreedMeetingID { get; set; }
        
        public Department Department { get; set; }
        public Meeting Meeting { get; set; }
        public AgreedMeeting AgreedMeeting { get; set; }
        //public ICollection<Chat> Chats { get; set; }
    }
}
