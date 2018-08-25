using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IHttpContextAccessor _iHttpContext;
        private readonly BuildingBuddiesContext _context;

        public ChatHub(IHttpContextAccessor iHttpContext, BuildingBuddiesContext context)
        {
            _iHttpContext = iHttpContext;
            _context = context;
        }

        public async Task SendMessage(string user, string message)
        {
            var usr = _iHttpContext.HttpContext.User.Identity.Name;
            await Clients.All.SendAsync("ReceiveMessage", usr, message);
        }

        public override Task OnConnectedAsync()
        {
            // dohvatiti username
            //var userName = _iHttpContext.HttpContext.User.Identity.Name;

            //var AgreedMeeting = await _context.AgreedMeeting.Where(am => am.Link.Contains(meetingLink)).FirstOrDefaultAsync();
            //var CorrectUser = await _context.User.Where(u => u.NormalizedUserName == userName.ToUpper() && u.AgreedMeetingID == AgreedMeeting.AgreedMeetingID).FirstOrDefaultAsync();

            string ConnectionId = Context.ConnectionId;

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            // maknuti ga iz aktivnih?

            return base.OnDisconnectedAsync(exception);
        }
    }
}