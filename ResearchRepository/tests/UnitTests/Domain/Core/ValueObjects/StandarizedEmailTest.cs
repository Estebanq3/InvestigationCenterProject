using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using ResearchRepository.Domain.Core.ValueObjects;

namespace UnitTests.Domain.Core.ValueObjects
{
    public class StandarizedEmailTest
    {
        public static string groupName = "Grupo";
        public static string link = "https:/localhost:44331/";
        public static string userEmail = "test@ResearchRepository.com";
        public static string defaultFile = " DEFAULT_CONTENT ";
        public static string suscriptionEmailFile = "LINK_NOTICIA + NOMBRE_GRUPO";
        public static string verificationFile = "LINK_VERIFICACION";


        [Fact]
        public void setSubscriptionEmailContentTest()
        {
            //arrange
            StandarizedEmail email = new StandarizedEmail(suscriptionEmailFile);
            //act
            email.setSubscriptionEmailContent(groupName, link);
            //assert
            email.getContent().Should().Contain(groupName).And.Contain(link);

        }
        [Fact]
        public void setAccountConfirmationEmailTest()
        {

            //arrange 
            StandarizedEmail email = new StandarizedEmail(verificationFile);
            //act
            email.setAccountConfirmationEmail(userEmail);
            //assert
            email.getContent().Should().Contain(userEmail);
            

        }
        [Fact]
        public void setDefaultContentTest()
        {
            //arrange
            StandarizedEmail email = new StandarizedEmail(defaultFile);
            //act
            email.setDefaultContent(userEmail);
            //assert
            email.getContent().Should().Contain(userEmail);

        }
        [Fact]
        public void  getContentTest()
        {
            //arrange
            StandarizedEmail email = new StandarizedEmail(suscriptionEmailFile);
            //act
            //assert
            email.getContent().Should().BeEquivalentTo(suscriptionEmailFile);
        }
    }
}
