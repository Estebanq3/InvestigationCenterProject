using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ResearchRepository.Domain.Core.ValueObjects
{
    public class StandarizedEmail
    {
        private string content= "";

        public StandarizedEmail(string newContent) //for tests and specific content
        {
            content = newContent;
        }
        public StandarizedEmail(int emailType)
        {
            switch (emailType)
            {
                case 1: //A new has just been published
                    content = File.ReadAllText("../WebApplication_ResearchRepository/wwwroot/emails/NewsEmail.html");
                    break;
                case 2: //An account confirmation email
                    content = File.ReadAllText("../WebApplication_ResearchRepository/wwwroot/emails/AccountVerification.html");
                    break;
                default:
                    content = File.ReadAllText("../WebApplication_ResearchRepository/wwwroot/emails/DefaultEmail.html");
                    break;
                //case 3: monos email
                
            }
        }

        public void setSubscriptionEmailContent(string groupName, string link)
        {
            content = content.Replace("NOMBRE_GRUPO", groupName);
            content = content.Replace("LINK_NOTICIA", link);
        }

        public void setAccountConfirmationEmail(string emailTokenURL)
        {
            content = content.Replace("LINK_VERIFICACION",emailTokenURL);
        }

        public void setDefaultContent(string newContent)
        {
            content = content.Replace("DEFAULT_CONTENT",newContent);
        }
        public string getContent()
        {
            return content;
        }

    }
}

