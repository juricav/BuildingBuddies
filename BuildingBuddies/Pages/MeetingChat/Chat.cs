using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace BuildingBuddies.Pages.MeetingChat
{
    public class Chat : PageModel
    {
        private readonly BuildingBuddiesContext _context;

        public Chat(BuildingBuddiesContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ChatItem Item { get; set; }

        public void OnGet()
        {
            if (Item == null)
            {
                Item = new ChatItem();
            }

            Item.Time = DateTime.Now;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Item.Id = 0; // id sastanka
            // id korisnika
            _context.ChatItem.Add(Item);
            _context.SaveChanges();

            return RedirectToPage("Chat");
        }
    }
}