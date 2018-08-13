using System.Collections.Generic;

namespace BuildingBuddies.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string Name { get; set; }

        public int MeetingID { get; set; }

        public Meeting Meeting { get; set; }
        public ICollection<User> Users { get; set; }        
    }
}
