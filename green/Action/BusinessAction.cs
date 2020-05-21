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
    class BusinessAction	
    {
		/// <summary>
		/// 获取收费业务应开发票数
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int GetInvoicePapers(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_getInvoicePapers", new OracleParameter[] { op_fa001 });
			return Convert.ToInt32(re.ToString());
		}


		/// <summary>
		/// 返回销售项目规格型号
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemGGXH(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemGGXH(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}
		/// <summary>
		/// 获取服务或商品的税率
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static decimal GetTaxRate(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return Convert.ToDecimal(SqlAssist.ExecuteScalar("select pkg_business.fun_GetTaxRate(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString());
		}

		/// <summary>
		/// 返回销售项目计量单位
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemDW(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getItemDW(:itemId) from dual", new OracleParameter[] { op_itemId }).ToString();
		}

		/// <summary>
		/// 获取项目的 发票编码 (税务)
		/// </summary>
		/// <param name="serviceSalesType"></param>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string GetItemInvoiceCode(string serviceSalesType, string itemId)
		{
			//项目类别
			OracleParameter op_type = new OracleParameter("ic_serviceSalesType", OracleDbType.Varchar2, 3);
			op_type.Direction = ParameterDirection.Input;
			op_type.Value = serviceSalesType;

			//项目ID
			OracleParameter op_itemId = new OracleParameter("ic_salesItemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_GetInvoiceCode(:type,:itemId) from dual", new OracleParameter[] { op_type, op_itemId }).ToString();
		}
		/// <summary>
		/// 产生墓位证书编号
		/// </summary>
		/// <param name="isOrig"></param>
		/// <returns></returns>
		public static string GetCertNum(string isOrig)
		{
			OracleParameter op_isOrig = new OracleParameter("ic_isOrig", OracleDbType.Varchar2, 3);
			op_isOrig.Direction = ParameterDirection.Input;
			op_isOrig.Value = isOrig ;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_getCertNum(:isOrig) from dual", new OracleParameter[] { op_isOrig }).ToString();
		}

		public static int IsDebt(string ac001)
		{
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_business.fun_isDebt(:ic_ac001) from dual", new OracleParameter[] { op_ac001 }).ToString());
		}

		public static int ManageFee(string fa001, string ac001,decimal price,decimal nums,string handler,string ws001)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			//购墓流水号
			OracleParameter op_ac001= new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//单价
			OracleParameter op_price = new OracleParameter("in_price", OracleDbType.Decimal);
			op_price.Direction = ParameterDirection.Input;
			op_price.Value = price;

			//年限
			OracleParameter op_nums = new OracleParameter("in_nums", OracleDbType.Decimal);
			op_nums.Direction = ParameterDirection.Input;
			op_nums.Value = nums;

			//经办人
			OracleParameter op_pr100 = new OracleParameter("ic_pr100", OracleDbType.Varchar2,10);
			op_pr100.Direction = ParameterDirection.Input;
			op_pr100.Value = handler;

			//工作站编号
			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;
 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_ManageFee", new OracleParameter[] {op_fa001,op_ac001,op_price,op_nums,op_pr100,op_ws001 });
		}


		/// <summary>
		/// 购墓调整
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="newBi001"></param>
		/// <param name="reason"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int TombTransfer(string ac001,string newBi001,string reason,string handler)
		{
			//购墓流水号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			//新墓穴位置
			OracleParameter op_bi001 = new OracleParameter("ic_new_bi001", OracleDbType.Varchar2, 10);
			op_bi001.Direction = ParameterDirection.Input;
			op_bi001.Value = newBi001;

			//调整原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2,100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;
			 
			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;
 
			return SqlAssist.ExecuteProcedure("pkg_business.prc_TombTransfer", new OracleParameter[] { op_ac001,op_bi001,op_reason, op_handler });
		}

		/// <summary>
		/// 弃墓处理
		/// </summary>
		/// <param name="ac001"></param>
		/// <param name="reason"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int TombQuit(string ac001,string reason,string handler)
		{
			//购墓流水号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			 
			//调整原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_tombQuit", new OracleParameter[] { op_ac001, op_reason, op_handler });
		}

		/// <summary>
		/// 购墓预定强制刷新
		/// </summary>
		/// <returns></returns>
		public static int ForceRefreshBookin()
		{
			return SqlAssist.ExecuteProcedure("pkg_business.prc_ForceRefreshBookin", new OracleParameter[] { });
		}
		/// <summary>
		/// 墓位预定取消
		/// </summary>
		/// <returns></returns>
		public static int BookinCanceled(string bk001)
		{
			//经办人
			OracleParameter op_bk001 = new OracleParameter("ic_bk001", OracleDbType.Varchar2, 10);
			op_bk001.Direction = ParameterDirection.Input;
			op_bk001.Value = bk001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_BookinCanceled", new OracleParameter[] { op_bk001 });
		}

	}
}
