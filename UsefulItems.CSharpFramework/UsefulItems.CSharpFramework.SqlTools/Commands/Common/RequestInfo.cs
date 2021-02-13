using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UsefulItems.CSharpFramework.SqlTools.Commands.Common
{
    public class RequestInfo
    {
        public string CommandText { get; set; }
        public CommandType CommandType { get; set; }

        public static RequestInfo CreateRequest(string request)
        {
            return new RequestInfo
            {
                CommandText = request,
                CommandType = CommandType.Text
            };
        }

        public static RequestInfo CreateProcedure(string procedure)
        {
            return new RequestInfo
            {
                CommandText = procedure,
                CommandType = CommandType.StoredProcedure
            };
        }
    }
}
