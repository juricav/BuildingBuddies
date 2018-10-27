using System;

namespace BuildingBuddies.Models
{
    public class ChatMessageDirection
    {
        public DateTime? Time { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string Direction { get; set; }
    }
}
