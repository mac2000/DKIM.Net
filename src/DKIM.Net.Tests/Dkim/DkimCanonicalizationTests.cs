using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DKIM.Net.Tests.Dkim
{
    [TestClass]
    public class DkimCanonicalizationTests
    {
        [TestMethod]
        public void Canonicalization2()
        {

            var emailText = @"A: X
B:Y	
	Z  

 C 
D 	 E


";

            var canonicalizedHeaders = @"a:X
b:Y Z
";

            var canonicalizedBody = @" C
D E
";

            var type = DkimCanonicalizationAlgorithm.Relaxed;


            var email = Email.Parse(emailText);

            Assert.AreEqual(canonicalizedBody, DkimCanonicalizer.CanonicalizeBody(email.Body, type), "body " + type);
            Assert.AreEqual(canonicalizedHeaders, DkimCanonicalizer.CanonicalizeHeaders(email.Headers, type, false, "A", "B"), "headers " + type);
            Assert.AreEqual(emailText, email.Raw);
        }

        [TestMethod]
        public void Canonicalization3()
        {
            var emailText = @"A: X
B:Y	
	Z  

 C 
D 	 E


";

            var canonicalizedHeaders = @"A: X
B:Y	
	Z  
";

            var canonicalizedBody = @" C 
D 	 E
";

            var type = DkimCanonicalizationAlgorithm.Simple;


            var email = Email.Parse(emailText);

            Assert.AreEqual(canonicalizedBody, DkimCanonicalizer.CanonicalizeBody(email.Body, type), "body " + type);
            Assert.AreEqual(canonicalizedHeaders, DkimCanonicalizer.CanonicalizeHeaders(email.Headers, type, false, "A", "B"), "headers " + type);
            Assert.AreEqual(emailText, email.Raw);
        }
    }
}

