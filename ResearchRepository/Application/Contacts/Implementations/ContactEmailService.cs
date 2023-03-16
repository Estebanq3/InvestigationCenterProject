using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResearchRepository.Application.Contacts
{

    public class ContactSenderOptions
    {
        public string SendGridKey { get; set; }
    }
    public class ContactEmailService : IContactEmailService
    {
        public ContactEmailService(IOptions<ContactSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;
        }

        public ContactSenderOptions Options { get; }

        public Task SendEmailAsync(List<string> emails, string userEmail, string subject, string message)
        {
            var client = new SendGridClient(Options.SendGridKey);
            var msg = new SendGridMessage()
            {
                From = new EmailAddress("gruposinvestigacionUCR@gmail.com", "Grupos Investigacion UCR"),
                Subject = subject,
                PlainTextContent = message,
                HtmlContent = message,
                ReplyTo = new EmailAddress(userEmail),
            };
            foreach(var email in emails)
            {
                msg.AddTo(new EmailAddress(email));
            }
            msg.AddCc(new EmailAddress(userEmail));
            

            // Disable click tracking.
            // See https://sendgrid.com/docs/User_Guide/Settings/tracking.html
            msg.SetClickTracking(false, false);

            return client.SendEmailAsync(msg);
        }
    }
}