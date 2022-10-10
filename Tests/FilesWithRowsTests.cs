using Console;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Tests
{
    public class FilesWithRowsTests
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
        public void AddRowToCollectionFirstPoistion()
        {
            var list = new List<FilesWithRows>()
            {
                new FilesWithRows { File = "Foo", Rows = new []{ "one", "two" } },
            };
            list.AddRowFirst("Zero");
            Assert.AreEqual(3, list.First().Rows.Count());
            Assert.AreEqual("Zero", list.First().Rows.First());
        }

        [Test]
        public void AddRowToCollectionLast()
        {
            var list = new List<FilesWithRows>()
            {
                new FilesWithRows { File = "Foo", Rows = new []{ "one", "two" } },
            };
            list.AddRowLast("Zero");
            Assert.AreEqual(3, list.First().Rows.Count());
            Assert.AreEqual("Zero", list.First().Rows.Last());
        }

        [Test]
        public void ReplaceRowInCollection()
        {
            var list = new List<FilesWithRows>()
            {
                new FilesWithRows { File = "Foo", Rows = new []{ "one", "two" } },
            };
            list.ReplaceRow("one", "Zero");
            Assert.AreEqual(2, list.First().Rows.Count());
            Assert.AreEqual("Zero", list.First().Rows.First());
        }

        [Test]
        public void RemoveRowInCollection()
        {
            var list = new List<FilesWithRows>()
            {
                new FilesWithRows { File = "Foo", Rows = new []{ "one", "two" } },
            };
            list.RemoveRow("one");
            Assert.AreEqual(1, list.First().Rows.Count());
            Assert.AreEqual("two", list.First().Rows.First());
        }

        [Test]
        public void AddRowToEmptyCollectionLast()
        {
            var list = new List<FilesWithRows>()
            {
            };
            list.AddRowLast( "Zero");
            Assert.IsEmpty(list);
        }

        [Test]
        public void AddRowToEmptyRowCollectionLast()
        {
            var list = new List<FilesWithRows>()
            {
                new FilesWithRows { File = "Foo", Rows = Array.Empty<string>() },
            };
            list.AddRowLast("Zero");
            Assert.AreEqual(1, list.First().Rows.Count());
            Assert.AreEqual("Zero", list.First().Rows.Last());
        }
    }
}
