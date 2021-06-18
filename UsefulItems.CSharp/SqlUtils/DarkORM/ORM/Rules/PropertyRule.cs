using System.Reflection;
using UsefulItems.CSharp.SqlUtils.DarkORM.ORM.TypeCasting;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.ORM.Rules
{
    public class PropertyRule
    {
        public PropertyInfo Info { get; set; }
        public string ColumnName { get; set; }
        public AbstractCaster TypeCaster { get; set; }
    }
}
