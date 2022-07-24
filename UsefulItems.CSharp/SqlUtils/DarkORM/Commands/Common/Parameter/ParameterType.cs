using System.Data;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.Commands.Common.Parameter
{
    public enum ParameterType
    {
        Input = 0,
        Output = 1,
        InputOutput = 2
    }

    public static class ParameterTypeExtensions
    {
        internal static ParameterDirection ToDirection(this ParameterType type)
        {
            switch(type)
            {
                case ParameterType.Input:
                    return ParameterDirection.Input;
                case ParameterType.Output:
                    return ParameterDirection.Output;
            }
            return ParameterDirection.InputOutput;
        }
    }
}
