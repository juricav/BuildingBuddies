using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }

        public int MeetingID { get; set; }
        
        public Meeting Meeting { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
