using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using ResearchRepository.Application.Contacts.Implementations;
using ResearchRepository.Application.Contacts;
using Moq;
using Microsoft.Extensions.Options;

namespace UnitTests.Application.Contacts
{
    public class ContactsEmailServiceTest
    {
        //Arrange
        private static readonly string _email = "Email";
        private static readonly string _subject = "Asunto";
        private static string _message = "Mensaje";
        ContactSenderOptions senderOptions = new ContactSenderOptions() { SendGridKey = "Key Mocked" };
        Mock<IOptions<ContactSenderOptions>> _options = new();

        private List<string> _getEmails()
        {
            List<string> emails = new List<string>();
            for(int i = 0; i < 5; i++)
            {
                emails.Add("Email"+i);
            }
            return emails;
        }

        [Fact]
        public async void SendEmailAsyncCanSendEmail()
        {
            //arrange
            _options.Setup(opt => opt.Value).Returns(senderOptions);
            var emailService = new ContactEmailService(_options.Object);
            List<string> emails = _getEmails();
            
            //act
            await emailService.SendEmailAsync(emails, _email, _subject, _message);
        }
    }
}
