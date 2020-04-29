using green.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.Action
{
    /// <summary>
    /// 代码转文本类
    /// </summary>
    class MapperID
    {
        public static string Mapper_mx(string mx)
        {
            OracleParameter op_mx = new OracleParameter("ic_mx", OracleDbType.Varchar2, 10);
            op_mx.Direction = ParameterDirection.Input;
            op_mx.Value = mx;
            Object re = SqlAssist.ExecuteFunction("pkg_report.fun_mapper_mx", new OracleParameter[] { op_mx });
            return re.ToString();
        }
    }
}
