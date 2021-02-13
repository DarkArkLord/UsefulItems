using System.Data.SqlClient;
using UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.HandleStrategy
{
    public class QueryHandleStrategy : IHandleStrategy
    {
        public IORMHandler Handler { get; set; }

        public object Result { get; set; }

        public void Execute(SqlCommand command)
        {
            SqlDataReader reader = command.ExecuteReader();
            Result = Handler.Handle(reader);
        }
    }
}
