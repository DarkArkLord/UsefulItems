using UsefulItems.CSharpFramework.SqlTools.ORM.Rules;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Handlers
{
    public static class PropertyHandler
    {
        private static object ReadFromReader(this PropertyRule rule, SqlDataReader reader)
        {
            return reader[rule.ColumnName];
        }

        private static object CastToCorrectType(this PropertyRule rule, object obj)
        {
            return rule.TypeCaster.Cast(obj);
        }

        private static void SetPropertyValue(this PropertyRule rule, object obj, object value)
        {
            rule.Info.SetValue(obj, value);
        }

        public static void Handle(this PropertyRule rule, SqlDataReader reader, object obj)
        {
            object t = rule.ReadFromReader(reader);
            t = rule.CastToCorrectType(t);
            rule.SetPropertyValue(obj, t);
        }
    }
}
