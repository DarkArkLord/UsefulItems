using System.Reflection;
using UsefulItems.CSharpFramework.SqlTools.ORM.TypeCasting;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Rules
{
    public class PropertyRule
    {
        public PropertyInfo Info { get; set; }
        public string ColumnName { get; set; }
        public AbstractCaster TypeCaster { get; set; }
    }
}
