using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        public ChatMessage Item { get; set; }

        public void OnGet(string meetingLink)
        {
            if (Item == null)
            {
                Item = new ChatMessage();
            }

            // dohvat poruka vezanih za taj meeting

            Item.Time = DateTime.Now;
        }

        public async Task<IActionResult> OnPost(string meetingLink)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            AgreedMeeting SourceMeeting = await _context.AgreedMeeting.Where(am => am.Link == meetingLink).FirstOrDefaultAsync();

            Item.AgreedMeetingID = SourceMeeting.AgreedMeetingID;
            //Item.UserID = 1;

            _context.ChatMessage.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("Chat", new { meetingLink });
        }
    }
}