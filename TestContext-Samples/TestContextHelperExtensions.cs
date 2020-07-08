using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace TestContext_Samples
{
    public static class TestContextHelperExtensions
    {
        public static TestContextHelper<T1> Fill<T1, T2>(this TestContextHelper<T1> helper, Expression<Func<T1, T2>> expression, IFormatProvider formatProvider = null) where T1 : new()
        {
            var propertyInfo = expression.GetMemberInfo() as PropertyInfo;
            if (propertyInfo == null)
            {
                throw new ArgumentException("Expected PropertyInfo type as expression value");
            }

            var convertedValues = GetMany(helper, expression, formatProvider).ToArray(); ;

            for (var i = 0; i < convertedValues.Length; i++)
            {
                if (helper.Instances.Count < i + 1)
                {
                    helper.Instances.Add(new T1());
                }
                propertyInfo.SetValue(helper.Instances[i], convertedValues[i]);
            }

            return helper;
        }

        public static T2 Get<T1, T2>(this TestContextHelper<T1> helper, Expression<Func<T1, T2>> expression, IFormatProvider formatProvider = null)
            => TestContextExtensions.Convert<T2>(helper.TestContext.Get(expression.GetMemberName()), formatProvider);

        public static T Get<T>(this TestContextHelper<T> helper, string testAttributeName, IFormatProvider formatProvider = null)
            => TestContextExtensions.Convert<T>(helper.TestContext.Get(testAttributeName), formatProvider);

        public static IEnumerable<T2> GetMany<T1, T2>(this TestContextHelper<T1> helper, Expression<Func<T1, T2>> expression, IFormatProvider formatProvider = null)
        {
            foreach (var kvp in helper.TestContext.GetTestContextPropertiesStartsWith(expression.GetMemberName()))
                yield return TestContextExtensions.Convert<T2>(kvp.Value, formatProvider);
        }

        public static IEnumerable<T> GetMany<T>(this TestContextHelper<T> helper, string testAttributeName, IFormatProvider formatProvider = null)
        {
            foreach (var kvp in helper.TestContext.GetTestContextPropertiesStartsWith(testAttributeName))
                yield return TestContextExtensions.Convert<T>(kvp.Value, formatProvider);
        }

        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> enumeration, Action<T> action)
        {
            foreach (T item in enumeration)
            {
                action(item);
            }
            return enumeration;
        }

        public static string GetMemberName<T>(this Expression<T> expression) => GetMemberInfo(expression).Name;

        public static MemberInfo GetMemberInfo<T>(this Expression<T> expression)
        {
            switch (expression.Body)
            {
                case MemberExpression m:
                    return m.Member;
                case UnaryExpression u when u.Operand is MemberExpression m:
                    return m.Member;
                default:
                    throw new NotImplementedException(expression.GetType().ToString());
            }
        }
    }
}