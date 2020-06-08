using DevExpress.XtraEditors;
using green.Misc;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace green.Action
{
    class PrintAction
    {
		private static string PADSTR = "!@#";
		private static string PADSTR2 = "$$$";

		class Send_PrintData
		{
			public string command { get; set; }
			public string data { get; set; }
			public string extra1 { get; set; }   //附加信息1
			public string extra2 { get; set; }   //附加信息2
			public string extra3 { get; set; }   //附加信息3
			public string extra4 { get; set; }   //附加信息4
			public string extra5 { get; set; }   //附加信息5

		}

		/// <summary>
		/// 打印墓位证书
		/// </summary>
		/// <param name="ac001"></param>
		public static void PrintCert(string ac001)
        {
			string s_certId = string.Empty;
			string s_position = string.Empty;
			StringBuilder sb_1 = new StringBuilder(200);

			OracleCommand oc_command = new OracleCommand("select * from v_print_cert where ac001 = :ac001", SqlAssist.conn);
			OracleParameter op_ac001 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			oc_command.Parameters.Add(op_ac001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				if (reader["AC113"] == null || reader["AC113"] is DBNull)
					sb_1.Append("" + PADSTR);								//安葬逝者姓名
				else
					sb_1.Append(reader["AC113"].ToString() + PADSTR);

				if (reader["AC112"] == null || reader["AC112"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者性别
				else
					sb_1.Append(reader["AC112"].ToString() + PADSTR);

				if (reader["AC114"] == null || reader["AC114"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者年龄
				else
					sb_1.Append(reader["AC114"].ToString() + PADSTR);

				if (reader["AC115"] == null || reader["AC115"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者籍贯
				else
					sb_1.Append(reader["AC115"].ToString() + PADSTR);

				if (reader["AC116"] == null || reader["AC116"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者生辰
				else
					sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["AC116"]) + PADSTR);

				if (reader["AC118"] == null || reader["AC118"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者死亡日期
				else
					sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["AC118"]) + PADSTR);

				if (reader["AC119"] == null || reader["AC119"] is DBNull)
					sb_1.Append("" + PADSTR);                               //安葬逝者 安葬日期
				else
					sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["AC119"]) + PADSTR);

				if (reader["AC049"] == null || reader["AC049"] is DBNull)
					sb_1.Append("" + PADSTR);                               //购墓日期
				else
					sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["AC049"]) + PADSTR);

				if (reader["AC050"] == null || reader["AC050"] is DBNull)
					sb_1.Append("" + PADSTR);                               //证书编号
				else
					sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["AC050"]) + PADSTR);

				sb_1.Append(reader["POSITION"].ToString() + PADSTR);

				Send_PrintData printData = new Send_PrintData();
				printData.command = "print_cert";
				printData.data = sb_1.ToString();
				Frm_main.socket.sendMsg(Tools.ConvertObjectToJson(printData));
			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}
 

        /// <summary>
        /// 打印管理费缴纳记录
        /// </summary>
        /// <param name="fa001"></param>
        public static void PrintPayRecord(string fa001)
        {
			OracleCommand oc_command = new OracleCommand("select * from v_print_payrecord where pr001 = :fa001", SqlAssist.conn);
			OracleParameter op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			oc_command.Parameters.Add(op_fa001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				StringBuilder sb_1 = new StringBuilder(100);
				sb_1.Append(reader["AC001"].ToString() + PADSTR);						      //购墓流水号
				sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["PR002"]) + PADSTR);       //缴费开始日期
				sb_1.Append(string.Format("{0:yyyy-MM-dd}", reader["PR003"]) + PADSTR);       //缴费终止日期
				sb_1.Append(reader["NUMS"].ToString() + PADSTR);                              //缴费年限
				sb_1.Append(reader["HANDLER"].ToString() + PADSTR);                           //经办人
				 

				Send_PrintData printData = new Send_PrintData();
				printData.command = "payrecord";
				printData.data = sb_1.ToString();
				printData.extra1 = reader["TIMES"].ToString();
				Frm_main.socket.sendMsg(Tools.ConvertObjectToJson(printData));
			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}

        /// <summary>
        /// 打印购墓协议
        /// </summary>
        /// <param name="ac001"></param>
        public static void PrintProtocol(string ac001)
        {
			OracleCommand oc_command = new OracleCommand("select * from v_print_protocol where ac001 = :ac001", SqlAssist.conn);
			OracleParameter op_ac001 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;
			oc_command.Parameters.Add(op_ac001);

			OracleDataReader reader = oc_command.ExecuteReader();
			if (reader.HasRows && reader.Read())
			{
				string s_szxm = string.Empty;
				StringBuilder sb_1 = new StringBuilder(100);

				sb_1.Append(reader["GUYNAME"].ToString() + PADSTR);     //安葬逝者姓名
				sb_1.Append(reader["AC005"].ToString() + PADSTR );      //联系地址
				//sb_1.Append(" " + PADSTR);                              //bitid
				sb_1.Append(reader["SALEPRICE"].ToString() + PADSTR);   //售价
				sb_1.Append(reader["AC003"].ToString() + PADSTR);       //购墓人
				sb_1.Append(reader["RELATION"].ToString() + PADSTR);    //与购墓人关系
				sb_1.Append(reader["REGION_NAME"].ToString() + PADSTR); //墓区
				sb_1.Append(reader["ROWER_NAME"].ToString() + PADSTR);  //墓区排
				sb_1.Append(reader["BITDESC"].ToString() + PADSTR);     //号位描述				 

				Send_PrintData printData = new Send_PrintData();
				printData.command = "procotol";
				printData.data = sb_1.ToString();
				Frm_main.socket.sendMsg(Tools.ConvertObjectToJson(printData));
			}
			else
			{
				XtraMessageBox.Show("未找到数据!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			reader.Dispose();
			oc_command.Dispose();
		}
		 
    }
}
