using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;

namespace DKIM.Net.Tests.MailMessage
{
    [TestClass]
    public class MailMessageDataTests
    {
        [TestMethod]
        public void BasicEmail()
        {
            var msg = new System.Net.Mail.MailMessage();
            msg.To.Add(new MailAddress("jb@domain.com", "Jim Bob"));
            msg.From = new MailAddress("joe.bloggs@domain.com", "Joe Bloggs");
            msg.Subject = "Test Message";
            msg.Body = "A simple message";

            var text = msg.GetText();

            Assert.IsFalse(string.IsNullOrEmpty(text));
        }
    }
}
