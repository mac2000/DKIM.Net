using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DKIM.Net.Tests.Dkim
{
    [TestClass]
    public class Body
    {
        [TestMethod]
        public void C()
        {
            //Assert.AreEqual(expected, DkimCanonicalizer.CanonicalizeBody(orig, type));
            Assert.AreEqual(Email.NewLine, DkimCanonicalizer.CanonicalizeBody(null, DkimCanonicalizationAlgorithm.Simple));
            Assert.AreEqual(Email.NewLine, DkimCanonicalizer.CanonicalizeBody("", DkimCanonicalizationAlgorithm.Simple));
            Assert.AreEqual("a" + Email.NewLine, DkimCanonicalizer.CanonicalizeBody("a", DkimCanonicalizationAlgorithm.Simple));
            Assert.AreEqual(@"a
b
c
", DkimCanonicalizer.CanonicalizeBody(@"a
b
c", DkimCanonicalizationAlgorithm.Simple));
            Assert.AreEqual(@"a

b

c
", DkimCanonicalizer.CanonicalizeBody(@"a

b

c
", DkimCanonicalizationAlgorithm.Simple));
        }
    }
}
