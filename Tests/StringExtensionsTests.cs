using Console;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class StringExtensionsTests
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
            int result = data.FindRowNumberOfFirstInstanceOf("And this is the second line ");
            Assert.AreEqual(2, result);
        }

        [Test]
        public void RowByIndex()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var expexted = "And this is the second line";
            var result = data.GetRowByIndex(2);
            Assert.AreEqual(expexted, result);
        }

        [Test]
        public void RowBySearchString()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var expexted = "[2] And this is the second line";
            var result = data.GetRowsBySearch("the second line");
            Assert.AreEqual(expexted, result.First());
        }
        [Test]
        public void RowsBySearchString()
        {
            var data = "This is the file foo \r\nAnd this is the second line \r\nThis is the third line";
            var result = data.GetRowsBySearch("this is");
            Assert.AreEqual(3, result.Count());
        }
    }
}