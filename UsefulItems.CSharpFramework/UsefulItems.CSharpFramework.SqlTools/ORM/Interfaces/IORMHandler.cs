using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces
{
    public interface IORMHandler
    {
        object Handle(SqlDataReader reader);
    }
}
