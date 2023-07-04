using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace liquidador_web.Services
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration Configuration;

        public EmailSender(IConfiguration configuration){
            Configuration = configuration;
        }

        public Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient client = new SmtpClient(Configuration["Mail:SMTP"], int.Parse(Configuration["Mail:Port"]));
            client.EnableSsl = bool.Parse(Configuration["Mail:SSL"]);
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(Configuration["Mail:Account"], Configuration["Mail:Password"]);

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(Configuration["Mail:From_Address"]);
            mailMessage.To.Add(email);
            mailMessage.Body = htmlMessage;
            mailMessage.IsBodyHtml = true;
            mailMessage.Subject = subject;

            return client.SendMailAsync(mailMessage);
        }
    }
}
