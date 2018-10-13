using BuildingBuddies.Helpers;
using BuildingBuddies.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRChat.Hubs
{
    public class ChatHub : Hub
    {
        private BuildingBuddiesContext _context;
        private readonly IHttpContextAccessor _iHttpContext;

        public ChatHub(BuildingBuddiesContext context, IHttpContextAccessor iHttpContext)
        {
            _context = context;
            _iHttpContext = iHttpContext;           
        }

        public User GetCurrentUser ()
        {
            string UserName = _iHttpContext.HttpContext.User.Identity.Name;
            User User = _context.User.Where(u => u.NormalizedUserName == UserName.ToUpper()).SingleOrDefault();

            return User;
        }
                
        public void SendMessage(string messageText)
        {
            User User = GetCurrentUser();

            User Reciever = _context.User.Where(r => r.AgreedMeetingID == User.AgreedMeetingID && r.Id != User.Id).FirstOrDefault();
            var RecieverId = Reciever.ConnectionID;

            // šalje se poruka pošiljatelju da ima svoju kopiju
            Clients.Client(User.ConnectionID).SendAsync("BroadcastMessage", User.UserName, messageText);
            // šaljemo drugom članu, ako ima connectionId
            if(RecieverId != null)
            {
                Clients.Client(RecieverId).SendAsync("BroadcastMessage", User.UserName, messageText);
            }
            
            // poruka se sprema u bazu
            var DbMessage = new ChatMessage
            {
                Message = messageText,
                Name = User.UserName,
                Time = DateTime.Now,
                AgreedMeetingID = User.AgreedMeetingID
            };
            
            _context.ChatMessage.Add(DbMessage);
            _context.SaveChanges();
        }
        
        public override Task OnConnectedAsync()
        {
            User User = GetCurrentUser();
            string ConnectionId = Context.ConnectionId;

            // spremanje connectionId u bazu
            User.ConnectionID = ConnectionId;
            _context.Update(User);
            _context.SaveChanges();

            // dohvat svih poruka za meetingId po redoslijedu
            var history = _context.ChatMessage.Where(m => m.AgreedMeeting.AgreedMeetingID == User.AgreedMeetingID).Select(cm =>
            new ChatMessage
            {
                Time = cm.Time,
                Name = cm.Name,
                Message = cm.Message
            }).ToList().OrderBy(cm => cm.Time);

            // slanje poruka za ispis povijesti
            Clients.All.SendAsync("History", history);

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception e)
        {
            // brisanje connectionID-a
            User User = GetCurrentUser();

            User.ConnectionID = string.Empty;
            _context.Update(User);
            _context.SaveChanges();

            return base.OnDisconnectedAsync(e);
        }        
    }
}