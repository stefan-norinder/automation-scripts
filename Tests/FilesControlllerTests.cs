using auto;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
;

namespace Tests
{
    public class FilesControlllerTests
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
        public void AddRowToEmptyCollectionFirstPoistion()
        {
            const string fileName = "foo";
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.GetFileNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new[] { "foo.txt" });
            fileServiceMock.Setup(x => x.Read(It.Is<string>(p => p == "foo.txt"), It.IsAny<bool>()))
                .Returns(string.Empty);
            var controller = new FilesController(fileServiceMock.Object);
            controller.AddRowFirst(fileName, "bar");
            fileServiceMock.Verify(x => x.Save(It.Is<IEnumerable<FilesWithRows>>(p => p.First().Rows.Count() == 1)));
        }

        [Test]
        public void AddRowToCollectionFirstPoistion()
        {
            const string fileName = "foo";
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.GetFileNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new[] { "foo.txt" });
            fileServiceMock.Setup(x => x.Read(It.Is<string>(p => p == "foo.txt"), It.IsAny<bool>()))
                .Returns("First row for now");
            var controller = new FilesController(fileServiceMock.Object);
            controller.AddRowFirst(fileName, "bar");
            fileServiceMock.Verify(x => x.Save(It.Is<IEnumerable<FilesWithRows>>(p => 
            p.First().Rows.Count() == 2 && 
            p.First().Rows.First() == "bar")));
        }

        [Test]
        public void AddRowToCollectionLastPoistion()
        {
            const string fileName = "foo";
            var fileServiceMock = new Mock<IFileService>();
            fileServiceMock.Setup(x => x.GetFileNames(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new[] { "foo.txt" });
            fileServiceMock.Setup(x => x.Read(It.Is<string>(p => p == "foo.txt"), It.IsAny<bool>()))
                .Returns("First row for now");
            var controller = new FilesController(fileServiceMock.Object);
            controller.AddRowLast(fileName, "bar"); fileServiceMock.Verify(x => x.Save(It.Is<IEnumerable<FilesWithRows>>(p =>
             p.First().Rows.Count() == 2 &&
             p.First().Rows.Last() == "bar")));
        }


    }
}
