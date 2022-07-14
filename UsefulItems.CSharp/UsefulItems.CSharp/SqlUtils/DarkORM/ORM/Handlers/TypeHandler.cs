using System;
using System.Data.SqlClient;
using UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Interfaces;
using UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Rules;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Handlers
{
    public class TypeHandler : IORMHandler
    {
        public TypeRule Rule { get; private set; }

        public TypeHandler(TypeRule rule)
        {
            Rule = rule;
        }

        private object CreateTypeInstance()
        {
            return Activator.CreateInstance(Rule.Type);
        }

        public object Handle(SqlDataReader reader)
        {
            object t = CreateTypeInstance();

            foreach(var p in Rule.Properties)
            {
                p.Handle(reader, t);
            }

            return t;
        }
    }
}
