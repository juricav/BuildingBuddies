using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.MeetingChat
{
    public class Chat : PageModel
    {
        // samo da imam primjer za pravi chat
        private readonly BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public Chat(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;
        }

        [BindProperty]
        public ChatMessage Item { get; set; }
        public List<ChatMessage> Messages { get; set; }

        public async Task<IActionResult> OnGetAsync(string meetingLink)
        {
            var userName = _iHttpContext.HttpContext.User.Identity.Name;

            var AgreedMeeting = await _context.AgreedMeeting.Where(am => am.Link.Contains(meetingLink)).FirstOrDefaultAsync();
            var CorrectUser = await _context.User.Where(u => u.NormalizedUserName == userName.ToUpper() && u.AgreedMeetingID == AgreedMeeting.AgreedMeetingID).FirstOrDefaultAsync();
            
            if(CorrectUser != null)
            {
                if (Item == null)
                {
                    Item = new ChatMessage();
                }

                // dohvat poruka vezanih za taj meeting
                Item.Time = DateTime.Now;

                return Page();
            }
            else
            {
                return Unauthorized();
            }            
        }

        public async Task<IActionResult> OnPost(string meetingLink)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Item.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            Item.Name = this.User.FindFirstValue(ClaimTypes.Name);

            AgreedMeeting SourceMeeting = await _context.AgreedMeeting.Where(am => am.Link == meetingLink).FirstOrDefaultAsync();

            Item.AgreedMeetingID = SourceMeeting.AgreedMeetingID;
            //Item.UserID = 1;
            Item.UserId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            
            _context.ChatMessage.Add(Item);
            await _context.SaveChangesAsync();

            return RedirectToPage("Chat", new { meetingLink });
        }
    }
}