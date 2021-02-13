using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.Common.Parameter
{
    public class ParameterInfo
    {
        public string Name { get; set; }
        public ParameterType Direction { get; set; }
        public ParameterValueType Type { get; set; }
        public bool HasValue { get; set; }
        public object Value { get; set; }

        public SqlParameter ToSqlParameter()
        {
            SqlParameter res = new SqlParameter
            {
                ParameterName = Name,
                Direction = Direction.ToDirection(),
                DbType = Type.ToDbType(),
            };

            if (HasValue)
            {
                res.Value = Value;
            }

            return res;
        }
    }
}
