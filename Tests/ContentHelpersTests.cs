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
            var result = data.AddRowFirst("I'm first");
            Assert.AreEqual(expexted, result);
        }

        [Test]
        public void AddRowLast()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var expexted = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line\r\nI'm last";
            var result = data.AddRowLast("I'm last");
            Assert.AreEqual(expexted, result);
        }

        [Test]
        public void AddRowAtPosition()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var expexted = "This is the file foo \r\nHere I am\r\nAnd this is the second line \r\nThis is the third line";
            var result = data.InsertRowAtPosition("Here I am", 2);
            Assert.AreEqual(expexted, result);
        }

        [Test]
        public void FindRowNumber()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            int result = data.FindRowNumber("And this is the second line ");
            Assert.AreEqual(2, result);
        }
    }
}