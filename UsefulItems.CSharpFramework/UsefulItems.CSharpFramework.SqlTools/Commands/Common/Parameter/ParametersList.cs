using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.Common.Parameter
{
    public class ParametersList
    {
        public Dictionary<string, ParameterInfo> Parameters { get; set; }

        public ParametersList()
        {
            Parameters = new Dictionary<string, ParameterInfo>();
        }

        public ParameterInfo this[string key] => Parameters[key];

        public void AddParameter(string name, ParameterType type, ParameterValueType value_type, object value = null)
        {
            ParameterInfo info = new ParameterInfo
            {
                Name = name,
                Direction = type,
                Type = value_type,
                HasValue = !(value is null),
                Value = value
            };

            Parameters.Add(info.Name, info);
        }

        public Dictionary<string, SqlParameter> ToSqlParameters()
        {
            Dictionary<string, SqlParameter> res = new Dictionary<string, SqlParameter>();

            foreach (string key in Parameters.Keys)
            {
                res.Add(key, Parameters[key].ToSqlParameter());
            }

            return res;
        }

        public void UpdateValues(Dictionary<string, SqlParameter> p)
        {
            foreach (string key in p.Keys)
            {
                ParameterInfo info = Parameters[key];
                SqlParameter sql = p[key];
                switch (info.Direction)
                {
                    case ParameterType.Input:
                        break;
                    case ParameterType.Output:
                    case ParameterType.InputOutput:
                        info.Value = sql.Value;
                        info.HasValue = true;
                        break;
                }
            }
        }
    }
}
