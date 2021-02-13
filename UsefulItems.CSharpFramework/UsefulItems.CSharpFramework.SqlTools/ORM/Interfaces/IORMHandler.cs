using System.Data.SqlClient;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces
{
    public interface IORMHandler
    {
        object Handle(SqlDataReader reader);
    }
}
