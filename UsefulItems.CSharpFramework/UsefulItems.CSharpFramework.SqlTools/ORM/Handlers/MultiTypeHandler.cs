using System.Collections.Generic;
using System.Data.SqlClient;
using UsefulItems.CSharpFramework.SqlTools.ORM.Interfaces;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Handlers
{
    public class MultiTypeHandler : IORMHandler
    {
        public string TypeColumnName { get; private set; }

        public Dictionary<string, TypeHandler> Handlers { get; private set; }

        public MultiTypeHandler(string type_column_name)
        {
            TypeColumnName = type_column_name;
            Handlers = new Dictionary<string, TypeHandler>();
        }

        public void AddTypeHandler(string column_content, TypeHandler handler)
        {
            Handlers.Add(column_content, handler);
        }

        public object Handle(SqlDataReader reader)
        {
            string key = reader[TypeColumnName] as string;
            TypeHandler handler = Handlers[key];
            return handler.Handle(reader);
        }
    }
}
