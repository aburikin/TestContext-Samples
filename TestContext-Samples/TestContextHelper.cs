using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections;
using System.Collections.Generic;

namespace TestContext_Samples
{
    public class TestContextHelper<T> : IEnumerable<T>
    {
        public TestContext TestContext { get; }
        public TestContextHelper(TestContext testContext) => TestContext = testContext;

        public List<T> Instances { get; } = new List<T>();

        #region IEnumerable<T>

        public IEnumerator<T> GetEnumerator() => Instances.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => Instances.GetEnumerator();

        #endregion
    }
}
