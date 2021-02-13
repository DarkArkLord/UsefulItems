using System.Data;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.Common.Parameter
{
    public enum ParameterValueType
    {
        Int = 0,
        String = 1,
        DateTime = 2
    }

    public static class ParameterValueTypeExtensions
    {
        internal static DbType ToDbType(this ParameterValueType type)
        {
            switch(type)
            {
                case ParameterValueType.Int:
                    return DbType.Int32;
                case ParameterValueType.String:
                    return DbType.String;
                case ParameterValueType.DateTime:
                    return DbType.DateTime;
            }
            return DbType.Object;
        }
    }
}
