using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace TestContext_Samples.Samples
{
    [TestClass]
    public class MsUnitTestSample2
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty(nameof(FileDescriptor.Name), @".\test_file1")]
        public void TestMethod1()
        {
            //Arrange
            var fileName = TestContext.For<FileDescriptor>().Get(fd => fd.Name);
            var fileInfo = new FileInfo(fileName);

            //Act
            var isExists = fileInfo.Exists;

            //Asssert
            Assert.IsFalse(isExists);
        }

        [TestMethod]
        [TestProperty(nameof(FileDescriptor.Name) + "1", @".\test_file1")]
        [TestProperty(nameof(FileDescriptor.Name) + "2", @".\test_file2")]
        [TestProperty(nameof(FileDescriptor.Name) + "3", @".\test_file3")]
        public void TestMethod2()
        {
            TestContext.For<FileDescriptor>().GetMany(fd => fd.Name).ForEach(fileName =>
            {
                //Arrange
                var f = new FileInfo(fileName);

                //Act
                var isExists = f.Exists;

                //Asssert
                Assert.IsFalse(isExists);
            });
        }
    }
}
