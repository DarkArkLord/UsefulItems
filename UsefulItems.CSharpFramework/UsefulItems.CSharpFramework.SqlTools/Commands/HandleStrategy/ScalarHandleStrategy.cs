using System.Data.SqlClient;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.HandleStrategy
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
