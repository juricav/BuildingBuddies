using System;
using System.ComponentModel.DataAnnotations;

namespace BuildingBuddies.Models
{
    public class ChatItem
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime? Time { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
