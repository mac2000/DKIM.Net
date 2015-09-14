using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Net.Mail;

namespace DKIM.Net.Tests.MailMessage
{
    [TestClass]
    public class MailMessageSendTests
    {
        public string PrivateKey = @"-----BEGIN RSA PRIVATE KEY-----
MIICWwIBAAKBgQC7tQiBtxdeRdH8XFGlyxf8qjJoyOJVfJrCbvHJVJG+ZiGzemZz
HvhurYEAj/2N9eL2qQWN3PzdJX4s7cFt2Wvdmj/MOGmIwPP6x2224ooSUp7FAeG3
H/ODMoAqDyXsdIC8mTj7YKpKCUki7/m3VDACSiOhfNxeAkykalHG/bjUuwIDAQAB
AoGAVGFkSpOY8KsoY27I0WQEC3QjJxGvFUjndSJUlPHsdpAI9FrAtV2lxnM+u5b/
H5L6jXGb6pL+JRfWqbHTs2L65qRlSnv9S+priPgryXHY/cORtBNgdMwNfjMJhPxE
CY3vw4KBL2L0IxRqoZeVsmu8g1cLKMrLVRXAcF7rWJnR8yECQQDzd1+iMTKu1c+4
FzZn17dscCVbeWBvvIXvkNRpa1dnadBEGqBxIYQMLUeqAsGAyF1aHZUWruZfOFeu
4Qlqo9MRAkEAxV7QR0l2xYresNwNrWnK9lB7F8HCogxoplp3dbyd++8NVSxhhWWQ
PVgBPozRiycSgbuUzEKeVtDrr2zi9ryTCwJAZZH+nr6ho1jl4Komc2oGRsH+g8v+
VH807T3hr90tSKJXVaI6HxhZa28Uf7PIoH52m5rN0PnEeCMcSYPulsOj0QJAMUXH
R1Suwwg1Kf/1pio4Eh/ravXjSiNA6O7CzfDFnASE1pOa0PuW88mJnfz3vv6FH0Ae
GJQ1BUVo4UWUr7ZKGwJANCEGFsILGGdMaZhmuZcUqphoR7um0Sa7sQQHiUzQuJZe
6GaqmpEox9En9rUPIkRog8wpvWPtb3njTqmTbk8VMg==
-----END RSA PRIVATE KEY-----";

        public System.Net.Mail.MailMessage Message { get; set; }
        public SmtpClient Smtp { get; set; }
        public DkimSigner DkimSigner { get; set; }
        public DomainKeySigner DomainKeySigner { get; set; }

        
        static string[] GetHeaders(string headers)
        {
            return headers == null ? null : headers.Split(',');
        }

        [TestInitialize]
        public void TestInitialize()
        {
            Message = new System.Net.Mail.MailMessage();
            Message.To.Add(new MailAddress("jim@acme.com", "Jim Bob"));
            Message.From = new MailAddress("joe@acme.com", "Joe Bloggs");
            Message.Subject = "Test DKIM Message";
            Message.Body = "A simple message";

            Smtp = new SmtpClient();
            Smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
            Smtp.PickupDirectoryLocation = Path.GetTempPath();

            DkimSigner = new DkimSigner(PrivateKeySigner.Create(PrivateKey), "acme.com", "dkim", GetHeaders("From,To,Subject"));
            DomainKeySigner = new DomainKeySigner(PrivateKeySigner.Create(PrivateKey), "acme.com", "dkim", GetHeaders("From,To,Subject"));
        }

        [TestMethod]
        public void Valid_send_sign_DKIM()
        {
            Message.DkimSign(DkimSigner);

            Smtp.Send(Message);
        }

        [TestMethod]
        public void Valid_send_sign_DomainKey()
        {
            Message.DomainKeySign(DomainKeySigner);

            Smtp.Send(Message);
        }

        [TestMethod]
        public void Valid_send_sign_DKIM_then_DomainKey()
        {
            Message.DkimSign(DkimSigner);
            Message.DomainKeySign(DomainKeySigner);

            Smtp.Send(Message);
        }

        [TestMethod]
        public void Valid_send_sign_DomainKey_then_DKIM()
        {
            Message.DomainKeySign(DomainKeySigner);
            Message.DkimSign(DkimSigner);

            Smtp.Send(Message);
        }
    }
}
