using Console;
using NUnit.Framework;
using System.Linq;

namespace Tests
{
    public class FileHelpersTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetFilesByNameRecursive()
        {
            var files = FileHelpers.GetFilesRecursiveByName("foo.txt");
            Assert.AreEqual(1, files.Count());
        }
    }
}