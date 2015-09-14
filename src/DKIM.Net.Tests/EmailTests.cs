using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace DKIM.Net.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void NewLine()
        {
            Assert.AreEqual(Environment.NewLine, Email.NewLine);
        }



        [TestMethod]
        public void Test1()
        {

            const string content = @"key1:Value1
Key2 : VALue2
Key3:Folded
 Value

start of email body
";

            var email = Email.Parse(content);

            foreach (var h in email.Headers)
            {
                Console.WriteLine();
                Console.WriteLine("--- header start ---");
                Console.WriteLine(h.Key);
                Console.WriteLine(h.Value.Key);
                Console.WriteLine("folded : " + h.Value.FoldedValue);
                Console.WriteLine(h.Value.Value);
                Console.WriteLine("--- header end ---");
            }


            Console.WriteLine("--- body start ---");
            Console.WriteLine(email.Body);
            Console.WriteLine("--- body end ---");

        }
    }
}
