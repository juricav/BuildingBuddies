using System;

namespace BuildingBuddies.Models
{
    public class ChatMessage
    {
        public int ChatMessageID { get; set; }        
        public DateTime? Time { get; set; }
        public string Name { get; set; }        
        public string Message { get; set; }
        
        public int? AgreedMeetingID { get; set; }
        
        public AgreedMeeting AgreedMeeting { get; set; } 
    }
}
