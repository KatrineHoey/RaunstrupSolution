using Raunstrup.Service.Infrastructure;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Raunstrup.Service.Domain.XUnitTest
{
    public class XUnitTestContact
    {
        [Fact]
        public void Email_Is_Send_Equal_True()
        {
            //Opretter et contact obejekt
            Contact contact = new Contact();
            contact.Name = "Test";
            contact.Email = "Test@test.com";
            contact.Subject = "Tester";
            contact.Message = "Dette er en test - true";

            //Kalder SendEmail() metoden
            DomainContactService cs = new DomainContactService();
            bool IsEmailSend = cs.SendEmail(contact);

            //Tjekker om der kommer true tilbage, altså om emailen er sendt. 
            Assert.True(IsEmailSend);
        }

        [Fact]
        public void Email_Is_Send_Equal_False()
        {
            //Opretter et contact obejekt
            Contact contact = new Contact();
            contact.Name = "Test";
            contact.Email = "Testtest.com"; //Emailen er ikke en gyldig email.
            contact.Subject = "Tester";
            contact.Message = "Dette er en test - false";

            //Kalder SendEmail() metoden
            DomainContactService cs = new DomainContactService();
            bool IsEmailSend = cs.SendEmail(contact);

            //Tjekker om der kommer false tilbage, altså at emailen ikke kan sendes. 
            Assert.False(IsEmailSend);
        }
    }
}
