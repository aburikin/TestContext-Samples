using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace TestContext_Samples.Samples
{
    public class FileDescriptor
    {
        public Guid FileVersionId { get; set; }
        public string Extension { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public int AccessPolicy { get; set; }

        public override string ToString()
        {
            return $"Id: {FileVersionId}; Ext: {Extension}; Name: {Name}; Created: {CreatedOn}; AccessPolicy: {AccessPolicy};";
        }
    }

    [TestClass]
    public class MsUnitTestSample3
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty(nameof(FileDescriptor.FileVersionId), "673C9C2D-A29E-4ACC-90D4-67C52FBA84E4")]
        [TestProperty(nameof(FileDescriptor.Extension), ".bin")]
        [TestProperty(nameof(FileDescriptor.Name), "ProfileData")]
        [TestProperty(nameof(FileDescriptor.CreatedOn), "12/31/1999")]
        [TestProperty(nameof(FileDescriptor.AccessPolicy), "5")]
        public void TestMethod1()
        {
            //Arrange
            var fileInfo = TestContext.For<FileDescriptor>()
                                        .Fill(fi => fi.FileVersionId)
                                        .Fill(fi => fi.Extension)
                                        .Fill(fi => fi.Name)
                                        .Fill(fi => fi.CreatedOn, new CultureInfo("en-US", false))
                                        .Fill(fi => fi.AccessPolicy).Single();

            //Act
            var fileInfoString = fileInfo.ToString();

            //Assert
            Assert.AreEqual("Id: 673c9c2d-a29e-4acc-90d4-67c52fba84e4; Ext: .bin; Name: ProfileData; Created: 31.12.1999 0:00:00; AccessPolicy: 5;", fileInfoString);
        }

        [TestMethod]
        //Case 1
        [TestProperty(nameof(FileDescriptor.FileVersionId), "673C9C2D-A29E-4ACC-90D4-67C52FBA84E4")]
        [TestProperty(nameof(FileDescriptor.Extension), ".bin")]
        [TestProperty(nameof(FileDescriptor.Name), "ProfileData")]
        [TestProperty(nameof(FileDescriptor.CreatedOn), "12/31/1999")]
        [TestProperty(nameof(FileDescriptor.AccessPolicy), "5")]
        //Case 2
        [TestProperty(nameof(FileDescriptor.FileVersionId) + "1", "E1E02553-AAE9-407E-8830-F187FFA4C633")]
        [TestProperty(nameof(FileDescriptor.Extension) + "1", ".pdf")]
        [TestProperty(nameof(FileDescriptor.Name) + "1", "ProfileReadme")]
        [TestProperty(nameof(FileDescriptor.CreatedOn) + "1", "07/08/2020")]
        [TestProperty(nameof(FileDescriptor.AccessPolicy) + "1", "2")]
        public void TestMethod2()
        {
            //Arrange
            TestContext.For<FileDescriptor>()
                        .Fill(fi => fi.FileVersionId)
                        .Fill(fi => fi.Extension)
                        .Fill(fi => fi.Name)
                        .Fill(fi => fi.CreatedOn, new CultureInfo("en-US", false))
                        .Fill(fi => fi.AccessPolicy).ForEach(fileInfo =>
                        {
                            //Act
                            var fileInfoString = fileInfo.ToString();

                            //Assert
                            Assert.AreEqual($"Id: {fileInfo.FileVersionId}; Ext: {fileInfo.Extension}; Name: {fileInfo.Name}; Created: {fileInfo.CreatedOn}; AccessPolicy: {fileInfo.AccessPolicy};", fileInfoString);
                        });
        }
    }
}
