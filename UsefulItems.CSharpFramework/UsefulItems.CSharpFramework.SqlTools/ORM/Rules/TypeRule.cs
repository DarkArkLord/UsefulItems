using UsefulItems.CSharpFramework.SqlTools.ORM.TypeCasting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.Rules
{
    public class TypeRule
    {
        protected List<PropertyRule> properties;
        public Type Type { get; protected set; }
        public IEnumerable<PropertyRule> Properties { get => properties; }

        public TypeRule(Type type)
        {
            Type = type;
            properties = new List<PropertyRule>();
        }

        protected void AddProperty(string property_name, string column_name, AbstractCaster caster)
        {
            PropertyRule rule = new PropertyRule
            {
                Info = Type.GetProperty(property_name),
                ColumnName = column_name,
                TypeCaster = caster
            };

            properties.Add(rule);
        }

        protected void AddIntProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToInt);

        protected void AddNullableIntProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToNullableInt);

        protected void AddDateTimeProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToDateTime);

        protected void AddNullableDateTimeProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToNullableDateTime);

        protected void AddStringProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToString);

        protected void AddBoolProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToBool);

        protected void AddNullableBoolProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToNullableBool);

        protected void AddXElementProperty(string property_name, string column_name) => AddProperty(property_name, column_name, CastersKeeper.CasterToXElement);
    }
}
