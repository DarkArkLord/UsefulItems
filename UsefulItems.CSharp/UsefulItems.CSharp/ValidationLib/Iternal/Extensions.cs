using System;
using System.Linq.Expressions;
using System.Reflection;

namespace UsefulItems.CSharp.ValidationLib.Iternal
{
    public static class Extensions
    {
		internal static void CheckNull(this object obj, string paramName, string message = "Can not be null")
		{
			if (obj == null)
			{
				throw new ArgumentNullException(paramName, message);
			}
		}

		internal static void CheckNull(this string str, string paramName, string message = "Can not be null")
		{
			if (str == null)
			{
				throw new ArgumentNullException(paramName, message);
			}

			if (string.IsNullOrEmpty(str))
			{
				throw new ArgumentException(message, paramName);
			}
		}

		#region Memders
		private static Expression RemoveUnary(Expression toUnwrap)
		{
			if (toUnwrap is UnaryExpression)
			{
				return ((UnaryExpression)toUnwrap).Operand;
			}

			return toUnwrap;
		}

		public static MemberInfo GetMember<T, TProp>(this Expression<Func<T, TProp>> expression)
		{
			var memberExp = RemoveUnary(expression.Body) as MemberExpression;

			if (memberExp == null)
			{
				return null;
			}

			Expression currentExpr = memberExp.Expression;

			while (true)
			{
				currentExpr = RemoveUnary(currentExpr);

				if (currentExpr != null && currentExpr.NodeType == ExpressionType.MemberAccess)
				{
					currentExpr = ((MemberExpression)currentExpr).Expression;
				}
				else
				{
					break;
				}
			}

			if (currentExpr == null || currentExpr.NodeType != ExpressionType.Parameter)
			{
				return null;
			}

			return memberExp.Member;
		}
		#endregion
	}
}
