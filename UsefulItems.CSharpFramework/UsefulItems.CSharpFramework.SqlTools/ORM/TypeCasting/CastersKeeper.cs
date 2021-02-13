using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.ORM.TypeCasting
{
    public static class CastersKeeper
    {
        public static CasterToNullableInt CasterToNullableInt { get; private set; } = new CasterToNullableInt();

        public static CasterToInt CasterToInt { get; private set; } = new CasterToInt();

        public static CasterToNullableDateTime CasterToNullableDateTime { get; private set; } = new CasterToNullableDateTime();

        public static CasterToDateTime CasterToDateTime { get; private set; } = new CasterToDateTime();

        public static CasterToString CasterToString { get; private set; } = new CasterToString();

        public static CasterToNullableBool CasterToNullableBool { get; private set; } = new CasterToNullableBool();

        public static CasterToBool CasterToBool { get; private set; } = new CasterToBool();

        public static CasterToXElement CasterToXElement { get; private set; } = new CasterToXElement();
    }
}
