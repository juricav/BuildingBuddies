using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BuildingBuddies.Helpers
{
    public class MailSender
    {
        public async Task Send(string reciever, string subject, string body)
        {
            string Sender = "buildingbuddies1@gmail.com";

            var SmtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 465,
                EnableSsl = true,
                Credentials = new NetworkCredential(Sender, "DebelaZirafa1!")
            };

            using (var Message = new MailMessage(Sender, reciever)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
                await SmtpClient.SendMailAsync(Message);
        }
    }
}
