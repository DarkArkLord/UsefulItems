using UsefulItems.CSharpFramework.SqlTools.ORM.TypeCasting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Rules
{
    public class PropertyRule
    {
        public PropertyInfo Info { get; set; }
        public string ColumnName { get; set; }
        public AbstractCaster TypeCaster { get; set; }
    }
}
