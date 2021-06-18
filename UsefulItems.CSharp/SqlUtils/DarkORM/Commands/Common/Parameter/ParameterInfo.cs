using System.Data.SqlClient;

namespace UsefulItems.CSharp.SqlUtils.DarkORM.Commands.Common.Parameter
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
