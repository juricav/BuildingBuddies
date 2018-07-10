using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Models
{
    public class Chat
    {
        public int ChatID { get; set; }
        public string MessageText { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Timestamp { get; set; }

        //public int AgreedMeetingID { get; set; }
        public int? UserID { get; set; }
        
        //public AgreedMeeting AgreedMeeting { get; set; }
        public User User { get; set; }      

    }
}
