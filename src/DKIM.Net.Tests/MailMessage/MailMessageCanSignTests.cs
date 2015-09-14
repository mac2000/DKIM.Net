using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Mail;
using System.Reflection;
using System.Net.Mime;
using System.IO;

namespace DKIM.Net.Tests.MailMessage
{
    [TestClass]
    public class MailMessageCanSignTests
    {
        [TestMethod]
        public void Can_sign()
        {
            var msg = new System.Net.Mail.MailMessage("from@domain.com", "to@domain.com", "subject", "body");

            Assert.IsTrue(msg.CanSign());
        }

        [TestMethod]
        public void Cannot_sign_multiple_alt_views()
        {
            var msg = new System.Net.Mail.MailMessage("from@domain.com", "to@domain.com", "subject", "body");

            var htmlView = AlternateView.CreateAlternateViewFromString("<p>some html</p>", new ContentType(@"text/html"));
            msg.AlternateViews.Add(htmlView);

            Assert.IsFalse(msg.CanSign());
        }

        [TestMethod]
        public void Cannot_sign_attachment()
        {
            var msg = new System.Net.Mail.MailMessage("from@domain.com", "to@domain.com", "subject", "body");

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\MailMessage\Attachment.htm";
            var attachment = new Attachment(path, new ContentType(@"text/html"));
            msg.Attachments.Add(attachment);

            Assert.IsFalse(msg.CanSign());
        }
    }
}
