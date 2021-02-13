using System.Data.SqlClient;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.HandleStrategy
{
    public class NonQueryHandleStrategy : IHandleStrategy
    {
        public int Result { get; set; }

        public void Execute(SqlCommand command)
        {
            Result = command.ExecuteNonQuery();
        }
    }
}
