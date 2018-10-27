using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuildingBuddies.Models
{
    public class Meeting
    {
        public int MeetingID { get; set; }
        public string Link { get; set; }
        [Display(Name = "Company domain")]
        public string Domain { get; set; }
        [Display(Name = "Company name")]
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public bool MeetingEnded { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "End of registrations")]
        public DateTime EndDate { get; set; }
        public string CreatorID { get; set; }

        public ICollection<User> Users { get; set; }
        public ICollection<Department> Departments { get; set; }
        public ICollection<AgreedMeeting> AgreedMeetings { get; set; }
    }
}
