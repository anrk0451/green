using green.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.DataSet
{
    class DataDict_ds : System.Data.DataSet
    {
        public DataTable dt_st01 { get; set; }
        public DataView dv_mx { get; set; }
        public DataView dv_gx { get; set; }
        public DataView dv_zs { get; set; }
        /// <summary>
        /// 构造函数
        /// </summary>
        public DataDict_ds()
        {
            dt_st01 = new DataTable("St01");
            OracleDataAdapter st01Adapter = new OracleDataAdapter("select * from st01", SqlAssist.conn);
            st01Adapter.Fill(dt_st01);

            dv_mx = new DataView(dt_st01);
            dv_mx.RowFilter = "ST002='MTYPE'";    //墓型

            dv_zs = new DataView(dt_st01);
            dv_zs.RowFilter = "ST002='ZMODEL'";   //葬式

            dv_gx = new DataView(dt_st01);
            dv_gx.RowFilter = "ST002='RELATION'"; //关系
        }
    }
}
