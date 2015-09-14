using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DKIM.Net.Tests
{
    [TestClass]
    public class WhiteSpaceTests
    {
        [TestMethod]
        public void IsWhiteSpace()
        {
            Assert.AreEqual(true, ' '.IsWhiteSpace());
            Assert.AreEqual(true, '\t'.IsWhiteSpace());
            Assert.AreEqual(true, '\n'.IsWhiteSpace());
            Assert.AreEqual(true, '\r'.IsWhiteSpace());
            Assert.AreEqual(false, 'a'.IsWhiteSpace());
            Assert.AreEqual(false, '1'.IsWhiteSpace());
            Assert.AreEqual(false, '*'.IsWhiteSpace());
        }

        [TestMethod]
        public void ReduceWhiteSpace()
        {
            Assert.AreEqual(" a b c", "  a     b    c  ".ReduceWitespace());
            Assert.AreEqual(" a b c", @"      a      


     b   

c    

".ReduceWitespace());
        }

        [TestMethod]
        public void RemoveWhiteSpace()
        {
            Assert.AreEqual("abc", "  a     b    c  ".RemoveWhitespace());
            Assert.AreEqual("abc", @"      a      


     b   

c    

".RemoveWhitespace());
        }
    }
}
