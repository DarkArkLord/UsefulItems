using UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM
{
    public abstract class BaseORM<HandleResultType> : IORMHandler
    {
        protected T ReadElement<T>(SqlDataReader reader, IORMHandler handler)
        {
            return (T)handler.Handle(reader);
        }

        protected IEnumerable<T> ReadElements<T>(SqlDataReader reader, IORMHandler handler)
        {
            while (reader.Read())
            {
                yield return ReadElement<T>(reader, handler);
            }
        }

        public abstract IEnumerable<HandleResultType> Handle(SqlDataReader reader);

        object IORMHandler.Handle(SqlDataReader reader)
        {
            return Handle(reader);
        }
    }
}
