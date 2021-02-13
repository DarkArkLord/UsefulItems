using System.Data.SqlClient;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.HandleStrategy
{
    public interface IHandleStrategy
    {
        void Execute(SqlCommand command);
    }
}
