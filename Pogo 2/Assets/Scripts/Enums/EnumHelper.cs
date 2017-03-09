using System;
using System.Linq.Expressions;

namespace Assets.Scripts.Enums
{
    public static class EnumHelper
    {
        public static string EnumName<T>(T value)
        {
            return Enum.GetName(typeof(T), value);
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }
    }
}
