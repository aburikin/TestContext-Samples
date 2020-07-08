using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestContext_Samples
{
    public static class TestContextExtensions
    {
        public static object Get(this TestContext testContext, string testAttributeName)
            => testContext.Properties.Cast<KeyValuePair<string, object>>().First(kvp => kvp.Key.Equals(testAttributeName, StringComparison.InvariantCultureIgnoreCase)).Value;

        public static T Get<T>(this TestContext testContext, string testAttributeName, IFormatProvider formatProvider = null) 
            => Convert<T>(Get(testContext, testAttributeName), formatProvider);

        public static IEnumerable<T> GetMany<T>(this TestContext testContext, string testAttributeName, IFormatProvider formatProvider = null)
        {
            foreach (var kvp in GetTestContextPropertiesStartsWith(testContext, testAttributeName))
                yield return Convert<T>(kvp.Value, formatProvider);
        }

        public static IOrderedEnumerable<KeyValuePair<string, object>> GetTestContextPropertiesStartsWith(this TestContext testContext, string testAttributeName) 
            => testContext.Properties.Cast<KeyValuePair<string, object>>().Where(kvp => kvp.Key.StartsWith(testAttributeName, StringComparison.InvariantCultureIgnoreCase)).OrderBy(kvp => kvp.Key);

        public static TestContextHelper<T> For<T>(this TestContext testContext) 
            => new TestContextHelper<T>(testContext);

        public static T2 Convert<T2>(object value, IFormatProvider formatProvider = null)
        {
            object res = null;

            if (typeof(T2) == typeof(Guid)) res = Guid.Parse(value.ToString());
            else if (typeof(T2) == typeof(string)) res = System.Convert.ToString(value, formatProvider);
            else if (typeof(T2) == typeof(long)) res = System.Convert.ToInt64(value, formatProvider);
            else if (typeof(T2) == typeof(int)) res = System.Convert.ToInt32(value, formatProvider);
            else if (typeof(T2) == typeof(DateTime)) res = System.Convert.ToDateTime(value, formatProvider);
            else if (typeof(T2) == typeof(double)) res = System.Convert.ToDouble(value, formatProvider);
            else if (typeof(T2) == typeof(bool)) res = System.Convert.ToBoolean(value, formatProvider);
            else if (typeof(T2) == typeof(byte)) res = System.Convert.ToByte(value, formatProvider);
            else if (typeof(T2) == typeof(sbyte)) res = System.Convert.ToSByte(value, formatProvider);
            else if (typeof(T2) == typeof(float)) res = System.Convert.ToSingle(value, formatProvider);
            else if (typeof(T2) == typeof(ushort)) res = System.Convert.ToUInt16(value, formatProvider);
            else if (typeof(T2) == typeof(uint)) res = System.Convert.ToUInt32(value, formatProvider);
            else if (typeof(T2) == typeof(ulong)) res = System.Convert.ToUInt64(value, formatProvider);
            else if (typeof(T2) == typeof(char)) res = System.Convert.ToChar(value, formatProvider);
            else if (typeof(T2) == typeof(decimal)) res = System.Convert.ToDecimal(value, formatProvider);

            //Nullable
            else if (typeof(T2) == typeof(Guid?)) res = Guid.Parse(value.ToString());
            else if (typeof(T2) == typeof(string)) res = System.Convert.ToString(value, formatProvider);
            else if (typeof(T2) == typeof(long?)) res = System.Convert.ToInt64(value, formatProvider);
            else if (typeof(T2) == typeof(int?)) res = System.Convert.ToInt32(value, formatProvider);
            else if (typeof(T2) == typeof(DateTime?)) res = System.Convert.ToDateTime(value, formatProvider);
            else if (typeof(T2) == typeof(double?)) res = System.Convert.ToDouble(value, formatProvider);
            else if (typeof(T2) == typeof(bool?)) res = System.Convert.ToBoolean(value, formatProvider);
            else if (typeof(T2) == typeof(byte?)) res = System.Convert.ToByte(value, formatProvider);
            else if (typeof(T2) == typeof(sbyte?)) res = System.Convert.ToSByte(value, formatProvider);
            else if (typeof(T2) == typeof(float?)) res = System.Convert.ToSingle(value, formatProvider);
            else if (typeof(T2) == typeof(ushort?)) res = System.Convert.ToUInt16(value, formatProvider);
            else if (typeof(T2) == typeof(uint?)) res = System.Convert.ToUInt32(value, formatProvider);
            else if (typeof(T2) == typeof(ulong?)) res = System.Convert.ToUInt64(value, formatProvider);
            else if (typeof(T2) == typeof(char?)) res = System.Convert.ToChar(value, formatProvider);
            else if (typeof(T2) == typeof(decimal?)) res = System.Convert.ToDecimal(value, formatProvider);

            return (T2)res;
        }
    }
}
