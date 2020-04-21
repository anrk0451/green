using green.Misc;
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
            ///实例化数据表
            ///1.Bi01
            DataColumn BI001 = new DataColumn("BI001", typeof(string));  //墓位编号
            BI001.Unique = true;

            DataColumn BI002 = new DataColumn("BI002", typeof(int));          //墓位所在排顺序号
            DataColumn BI003 = new DataColumn("BI003", typeof(string));       //号位文字描述
            DataColumn BI005 = new DataColumn("BI005", typeof(string));       //墓型
            DataColumn BI01_PRICE = new DataColumn("PRICE", typeof(decimal)); //定价
            DataColumn BI01_RG001 = new DataColumn("RG001", typeof(string));  //墓区编号
            DataColumn RE001 = new DataColumn("RE001", typeof(string));       //墓区排编号
            DataColumn AC001 = new DataColumn("AC001", typeof(string));       //购墓编号
            DataColumn STATUS = new DataColumn("STATUS", typeof(string));     //状态 1-未使用 2-已使用 3-预定 4-冻结

            dt_bi01 = new DataTable("Bi01");
            dt_bi01.Columns.AddRange(new DataColumn[]
                {BI001,BI002,BI003,BI005,BI01_PRICE,BI01_RG001,RE001,AC001,STATUS});
            dt_bi01.PrimaryKey = new DataColumn[] { BI001 };  //设置主键
            this.Tables.Add(dt_bi01);

            bi01Adapter = new OracleDataAdapter("select * from bi01", SqlAssist.conn);
            builder = new OracleCommandBuilder(bi01Adapter);

            ///2.Rg01
			dt_rg01 = new DataTable("Rg01");
            DataColumn RG001 = new DataColumn("RG001", typeof(string));  //结构编号
            DataColumn RG002 = new DataColumn("RG002", typeof(string));  //结构类型 0-顶级节点 1-墓区 2-排
            DataColumn RG003 = new DataColumn("RG003", typeof(string));  //结构单元描述
            DataColumn RG004 = new DataColumn("RG004", typeof(string));  //墓型
            DataColumn RG005 = new DataColumn("RG005", typeof(int));     //开始编号
            DataColumn RG006 = new DataColumn("RG006", typeof(int));     //结束编号
            DataColumn RG007 = new DataColumn("RG007", typeof(int));     //墓位顺序排列  0-左起 1-右起
            DataColumn RG009 = new DataColumn("RG009", typeof(string));  //父节点编号
            DataColumn RG01_PRICE = new DataColumn("PRICE", typeof(decimal));  //定价
            
            dt_rg01.Columns.AddRange(new DataColumn[]
                {RG001,RG002,RG003,RG004,RG005,RG006,RG007,RG009,RG01_PRICE});
            dt_rg01.PrimaryKey = new DataColumn[] { RG001 };  //设置主键

            this.Tables.Add(dt_rg01);
            rg01Adapter = new OracleDataAdapter("select * from rg01 order by rg001", SqlAssist.conn);
            builder = new OracleCommandBuilder(rg01Adapter);

            ///3.dt_grid
            
        }

        /// <summary>
        /// 填充 Rg01
        /// </summary>
        public void Fill_Rg01()
        {
            dt_rg01.Rows.Clear();
            rg01Adapter.Fill(dt_rg01);
        }

        public void Fill_Bi01()
        {
            dt_bi01.Rows.Clear();
            bi01Adapter.Fill(dt_bi01);
        }
    }
}
