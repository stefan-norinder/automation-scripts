using Console;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class ContentHelpersTests
    {
        [SetUp]
        public void Setup()
        {
        }
        
        [TearDown]
        public void TearDown()
        {
        }

        [Test]
        public void AddRowFirst()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var expexted = "I'm first\r\nThis is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var result = ContentHelpers.AddRowFirst(data, "I'm first");
            Assert.AreEqual(expexted, result);
        }

        
    }
}