using System.Data.SqlClient;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Interfaces
{
    public interface IORMHandler
    {
        object Handle(SqlDataReader reader);
    }
}
