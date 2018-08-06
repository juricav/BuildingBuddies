using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingBuddies.Models
{
    public class ChatMessage
    {
        [Key]
        public int ChatItemID { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        [Required]
        public string Name { get; set; }        

        [Required]
        public string Message { get; set; }

        public string UserID { get; set; }
        public string AgreedMeetingID { get; set; }

        public User User { get; set; }
        public AgreedMeeting AgreedMeeting { get; set; } 
    }
}
