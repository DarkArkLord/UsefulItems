using UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
