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
    class Ac01_ds : System.Data.DataSet
    {
        public DataTable Ac01 { get; set; }
        public DataTable Ac03 { get; set; }

        private OracleDataAdapter ac01Adapter = new OracleDataAdapter("",SqlAssist.conn);
        private OracleDataAdapter ac03Adapter = new OracleDataAdapter("", SqlAssist.conn);
        private OracleCommandBuilder builder = null;

        public Ac01_ds()
        {
            //1.初始化表Ac01
            DataColumn AC001 = new DataColumn("AC001", typeof(string));  //购墓编号
            AC001.Unique = true;

            DataColumn AC002 = new DataColumn("AC002", typeof(string));  //购墓人身份证号
            DataColumn AC003 = new DataColumn("AC003", typeof(string));  //购墓人或单位
            DataColumn AC004 = new DataColumn("AC004", typeof(string));  //联系电话
            DataColumn AC005 = new DataColumn("AC005", typeof(string));  //联系地址
            DataColumn AC010 = new DataColumn("AC010", typeof(string));  //墓位区域编号
            DataColumn AC012 = new DataColumn("AC012", typeof(string));  //墓位排号
            DataColumn AC015 = new DataColumn("AC015", typeof(string));  //墓位编号
            DataColumn AC020 = new DataColumn("AC020", typeof(decimal)); //墓位定价
            DataColumn AC022 = new DataColumn("AC022", typeof(decimal)); //墓位售价
            DataColumn AC038 = new DataColumn("AC038", typeof(int));     //免费管理年限
            DataColumn AC040 = new DataColumn("AC040", typeof(DateTime));//管理费到期时间
            DataColumn AC042 = new DataColumn("AC042", typeof(string));  //缴费状态 1-yes 0-no
            DataColumn AC048 = new DataColumn("AC048", typeof(string));  //结算流水号
            DataColumn AC049 = new DataColumn("AC049", typeof(DateTime));//购墓时间
            DataColumn AC100 = new DataColumn("AC100", typeof(string));  //经办人员
            DataColumn AC200 = new DataColumn("AC200", typeof(DateTime));//经办日期
            DataColumn AC250 = new DataColumn("AC250", typeof(string));  //备注
            DataColumn AC300 = new DataColumn("AC300", typeof(string));  //登记类型 1-正常登记 0-原始登记
            DataColumn STATUS = new DataColumn("STATUS", typeof(string));//当前状态 0-作废 1-正常 2-退墓处理 3-弃墓处理

            Ac01 = new DataTable("Ac01");
            Ac01.Columns.AddRange(new DataColumn[]
                {AC001,AC002,AC003,AC004,AC005,AC010,AC012,AC015,AC020,AC022,AC038,AC040,AC042,AC048,AC049,AC100,AC200,AC250,AC300,STATUS});
            Ac01.PrimaryKey = new DataColumn[] { AC001 };  //设置主键
            this.Tables.Add(Ac01);

            //初始化表Ac03
            Ac03 = new DataTable("Ac03");
            DataColumn AC111 = new DataColumn("AC111", typeof(string));       //安葬人员编号
            AC111.Unique = true;

            DataColumn Ac03_AC001 = new DataColumn("AC001", typeof(string));  //购墓编号
            DataColumn AC113 = new DataColumn("AC113", typeof(string));       //逝者姓名
            DataColumn AC112 = new DataColumn("AC112", typeof(string));       //逝者性别 0-男 1-女 
            DataColumn AC114 = new DataColumn("AC114", typeof(int));          //逝者年龄
            DataColumn AC115 = new DataColumn("AC115", typeof(string));       //逝者籍贯
            DataColumn AC116 = new DataColumn("AC116", typeof(DateTime));     //逝者生辰
            DataColumn AC117 = new DataColumn("AC117", typeof(string));       //逝者生辰中文版
            DataColumn AC118 = new DataColumn("AC118", typeof(DateTime));     //逝世时间
            DataColumn AC119 = new DataColumn("AC119", typeof(DateTime));     //安葬时间
            DataColumn AC120 = new DataColumn("AC120", typeof(string));       //葬式
            DataColumn AC130 = new DataColumn("AC130", typeof(string));       //与购墓人关系
            DataColumn AC199 = new DataColumn("AC199", typeof(string));       //安葬批次号 
            DataColumn Ac03_STATUS = new DataColumn("STATUS", typeof(string));//状态 1-正常 0-删除 

            Ac03.Columns.AddRange(new DataColumn[] {AC111,Ac03_AC001,AC113,AC112,AC114,AC115,AC116,AC117,AC118,AC119,AC120,AC130,AC199,Ac03_STATUS });
            Ac03.PrimaryKey = new DataColumn[] { AC111 };
            this.Tables.Add(Ac03);


        }
    }
}
