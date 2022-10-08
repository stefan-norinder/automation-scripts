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
            FileHelpers.CreateFile("inc.txt", "/TestData");
        }
        
        [TearDown]
        public void TearDown()
        {
            FileHelpers.DeleteFile("bar.txt", "/TestData");
        }

        [Test]
        public void CreateFile()
        {
            FileHelpers.CreateFile("bar.txt", "/TestData");
            Assert.IsTrue(FileHelpers.FileExists("/TestData/bar.txt"));
        }

        [Test]
        public void RemoveFile()
        {
            if (!FileHelpers.FileExists("/TestData/inc.txt")) throw new System.Exception("File to remove does'n exist");
            FileHelpers.DeleteFile("inc.txt", "/TestData");
            Assert.IsFalse(FileHelpers.FileExists("/TestData/inc.txt"));
        }

        [Test]
        public void GetFilesByNameRecursive()
        {
            var files = FileHelpers.GetFilesRecursiveByName("foo.txt");
            Assert.AreEqual(1, files.Count());
        }

        [Test]
        public void FileExists()
        {
            Assert.IsTrue(FileHelpers.FileExists("/TestData/foo.txt"));
        }
    }
}