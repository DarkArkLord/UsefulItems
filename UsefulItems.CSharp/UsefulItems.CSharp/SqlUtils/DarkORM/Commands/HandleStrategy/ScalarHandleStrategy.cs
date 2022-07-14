using System.Data.SqlClient;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.Commands.HandleStrategy
{
    public class ScalarHandleStrategy : IHandleStrategy
    {
        public object Result { get; set; }

        public void Execute(SqlCommand command)
        {
            Result = command.ExecuteScalar();
        }
    }
}
