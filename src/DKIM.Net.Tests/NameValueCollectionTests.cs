using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Specialized;

namespace DKIM.Net.Tests
{
    [TestClass]
    public class NameValueCollectionTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Null()
        {
            var orig = new NameValueCollection();
            orig = null;

            orig.Prepend("name", "value");
        }

        [TestMethod]
        public void Empty()
        {
            var orig = new NameValueCollection();

            orig.Prepend("name", "value");

            Assert.AreEqual(1, orig.Count);
            Assert.AreEqual("value", orig[0]);
        }

        [TestMethod]
        public void SimgleItemNameDoesNotExist()
        {
            var orig = new NameValueCollection {{"n", "v"}};

            orig.Prepend("name", "value");

            Assert.AreEqual(2, orig.Count);
            Assert.AreEqual("value", orig[0]);
            Assert.AreEqual("v", orig[1]);
        }

        [TestMethod]
        public void SimgleItemNameDoesExist()
        {
            var orig = new NameValueCollection {{"name", "v"}};

            orig.Prepend("name", "value");

            Assert.AreEqual(1, orig.Count);
            Assert.AreEqual("value", orig[0]);
        }


        [TestMethod]
        public void MiltipleItemNameDoesExistAndAtStart()
        {
            var orig = new NameValueCollection {{"name", "v"}, {"name2", "v2"}};

            orig.Prepend("name", "value");

            Assert.AreEqual(2, orig.Count);
            Assert.AreEqual("value", orig[0]);
            Assert.AreEqual("v2", orig[1]);
        }


        [TestMethod]
        public void MiltipleItemNameDoesExistAndNotAtStart()
        {
            var orig = new NameValueCollection {{"name2", "v2"}, {"name", "v"}};

            orig.Prepend("name", "value");

            Assert.AreEqual(2, orig.Count);
            Assert.AreEqual("value", orig[0]);
            Assert.AreEqual("v2", orig[1]);
        }
    }
}
