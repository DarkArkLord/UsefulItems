using System.Data.SqlClient;
using UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Interfaces;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.Commands.HandleStrategy
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
