using BuildingBuddies.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace BuildingBuddies.Pages.MeetingChat
{
    public class ChatViewComponent : ViewComponent
    {
        private readonly BuildingBuddiesContext _context;
 
        public ChatViewComponent(BuildingBuddiesContext context)
        {
            _context = context;
        }
        
        public async Task<IViewComponentResult> InvokeAsync(string meetingLink)
        {
            var chats = await _context.ChatMessage.Where(cm => cm.AgreedMeeting.Link.Equals(meetingLink))
                                                  .OrderBy(cm => cm.Time)
                                                  .ToListAsync();

            return View(chats);
        }

    }
}
