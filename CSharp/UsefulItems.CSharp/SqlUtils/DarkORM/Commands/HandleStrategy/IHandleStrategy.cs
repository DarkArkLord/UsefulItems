using System.Data.SqlClient;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.Commands.HandleStrategy
{
    public interface IHandleStrategy
    {
        void Execute(SqlCommand command);
    }
}
