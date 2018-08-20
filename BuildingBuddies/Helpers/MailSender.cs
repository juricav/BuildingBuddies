using System.Net.Mail;
using System.Threading.Tasks;

namespace BuildingBuddies.Helpers
{
    public class MailSender
    {
        public async Task Send(string reciever, string subject, string body)
        {
            //string Sender = "buildingbuddies1@gmail.com";
            string Sender = "service@buildingbuddies.com";

            var SmtpClient = new SmtpClient
            {
                //Host = "smtp.gmail.com",
                //Port = 587,
                //EnableSsl = true,
                //Credentials = new NetworkCredential(Sender, "DebelaZirafa1!")
                Host = "127.0.0.1",
                Port = 2500
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
