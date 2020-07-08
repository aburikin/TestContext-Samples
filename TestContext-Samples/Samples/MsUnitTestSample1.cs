using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using System.IO;

namespace TestContext_Samples.Samples
{
    [TestClass]
    public class MsUnitTestSample1
    {
        public TestContext TestContext { get; set; }

        [TestMethod]
        [TestProperty("testId", "1AE6F4BB-8806-40AB-8E36-05E0D5E8032B")]
        public void TestMethod1()
        {
            //Arrange
            var testId = TestContext.Get<Guid>("testId");

            //Act
            var isEmpty = testId == Guid.Empty;

            //Asssert
            Assert.IsFalse(isEmpty);
        }

        [TestMethod]
        [TestProperty("prop1", "1AE6F4BB-8806-40AB-8E36-05E0D5E8032B")]
        [TestProperty("prop2", "1729")]
        [TestProperty("prop3", "4611686018427387903")]
        [TestProperty("prop4", "12/31/1999")]
        [TestProperty("prop5", "789.0123")]
        [TestProperty("prop6", "true")]
        public void TestMethod2()
        {
            //Arrange
            var guidTestValue = TestContext.Get<Guid>("prop1");
            var intTestValue = TestContext.Get<int>("prop2");
            var longTestValue = TestContext.Get<long>("prop3");
            var datetimeTestValue = TestContext.Get<DateTime>("prop4", new CultureInfo("en_US", false));
            var doubleTestValue = TestContext.Get<double>("prop5", new NumberFormatInfo() { NumberDecimalSeparator = "." });
            var boolTestValue = TestContext.Get<bool>("prop6");

            //Act
            //...

            //Asssert
            Assert.AreEqual(Guid.Parse("1AE6F4BB-8806-40AB-8E36-05E0D5E8032B"), guidTestValue);
            Assert.AreEqual(1729, intTestValue);
            Assert.AreEqual(4611686018427387903L, longTestValue);
            Assert.AreEqual(new DateTime(1999,12,31), datetimeTestValue);
            Assert.AreEqual(789.0123D, doubleTestValue);
            Assert.AreEqual(true, boolTestValue);
        }

        [TestMethod]
        [TestProperty("fileName1", @".\test_file1")]
        [TestProperty("fileName2", @".\test_file2")]
        [TestProperty("fileName3", @".\test_file3")]
        public void TestMethod3()
        {
            TestContext.GetMany<string>("fileName").ForEach(fileName =>
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
