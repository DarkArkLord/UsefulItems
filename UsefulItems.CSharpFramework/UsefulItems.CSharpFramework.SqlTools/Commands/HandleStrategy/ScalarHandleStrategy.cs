using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
