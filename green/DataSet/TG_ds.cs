using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.DataSet
{
    /// <summary>
    /// 墓区结构数据集
    /// </summary>
    class TG_ds : System.Data.DataSet
    {
        public DataTable dt_grid { get; set; }   //图形化网格数据源
        public DataTable dt_bi01 { get; set; }   //墓位信息表
        public DataTable dt_rg01 { get; set; }   //墓区结构信息

        private OracleDataAdapter bi01Adapter;
        private OracleDataAdapter rg01Adapter;

        OracleCommandBuilder builder = null;
        /// <summary>
        /// 构造函数
        /// </summary>
        public TG_ds()
        {
            DataColumn BI001 = new DataColumn("BI001", typeof(string));  //墓位编号
            BI001.Unique = true;

            DataColumn BI002 = new DataColumn("BI002", typeof(int));          //墓位所在排顺序号
            DataColumn BI003 = new DataColumn("BI003", typeof(string));       //号位文字描述
            DataColumn BI005 = new DataColumn("BI005", typeof(string));       //墓型
            DataColumn BI01_PRICE = new DataColumn("PRICE", typeof(decimal)); //定价



            DataColumn BI030 = new DataColumn("BI030", typeof(int));     //墓位所在排顺序号
            DataColumn BI002 = new DataColumn("BI002", typeof(int));     //号位数字编号
            
            
            DataColumn BI007 = new DataColumn("BI007", typeof(string));  //价格锁 0-否 1-是
            DataColumn BI008 = new DataColumn("BI008", typeof(int));     //列数
            
            DataColumn BI010 = new DataColumn("BI010", typeof(string));  //寄存逝者编号
            DataColumn STATUS = new DataColumn("STATUS", typeof(string));//状态 0-未用 1-占用 9-空闲

            Bi01 = new DataTable("Bi01");
            Bi01.Columns.AddRange(new DataColumn[]
                {BI001,RG001_2,BI020,BI030,BI002,BI003,BI005,BI007,BI008,BI009,BI010,STATUS});
            Bi01.PrimaryKey = new DataColumn[] { BI001 };  //设置主键
            this.Tables.Add(Bi01);
        }
    }
}
