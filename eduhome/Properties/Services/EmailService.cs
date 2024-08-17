using BackEndProject_Edu.Services.Interfaces;
using System.Net.Mail;

namespace BackEndProject_Edu.Services
{
    public class EmailService : IEmailService
    {
        public void SendEmail(List<string> emails, string body, string title, string subject)
        {
            MailMessage mail = new();
          
            foreach (var email in emails)
            {
                mail.To.Add(new MailAddress(email));
            }
            mail.Subject = subject;
            mail.IsBodyHtml = true;
            mail.Body = body;

            SmtpClient smtpClient = new()
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                
            };
            smtpClient.Send(mail);
        }
    }
}
