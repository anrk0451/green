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
		/// 获取收费业务已开发票数
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static int GetHaveInvoicePapers(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_getHaveInvoicePapers", new OracleParameter[] { op_fa001 });
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
		/// <summary>
		/// 获取墓区墓位总数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int TombTotal_stat(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_TombTotal_stat(:isOrig) from dual", new OracleParameter[] { op_rg001 }));
		}
		/// <summary>
		/// 获取墓区已售墓位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int TombSaled_stat(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_TombSaled_stat(:isOrig) from dual", new OracleParameter[] { op_rg001 }));
		}
		/// <summary>
		/// 获取墓区未售墓位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int TombUnsaled_stat(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_TombUnsaled_stat(:isOrig) from dual", new OracleParameter[] { op_rg001 }));
		}
		/// <summary>
		/// 获取墓区欠费墓位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int TombDebt_stat(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_TombDebt_stat(:isOrig) from dual", new OracleParameter[] { op_rg001 }));
		}
		/// <summary>
		/// 获取墓区预定墓位数
		/// </summary>
		/// <param name="rg001"></param>
		/// <returns></returns>
		public static int TombBookin_stat(string rg001)
		{
			OracleParameter op_rg001 = new OracleParameter("ic_rg001", OracleDbType.Varchar2, 10);
			op_rg001.Direction = ParameterDirection.Input;
			op_rg001.Value = rg001;

			return Convert.ToInt32(SqlAssist.ExecuteScalar("select pkg_report.fun_TombBookin_stat(:isOrig) from dual", new OracleParameter[] { op_rg001 }));
		}

		/// <summary>
		/// 判断 当前工作站是否允许 操作结算记录 1-允许操作 0-不允许
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="ws001"></param>
		/// <returns></returns>
		public static string CheckWorkStationCompare(string fa001, string ws001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			OracleParameter op_ws001 = new OracleParameter("ic_ws001", OracleDbType.Varchar2, 10);
			op_ws001.Direction = ParameterDirection.Input;
			op_ws001.Value = ws001;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_CheckWorkStationCompare(:ic_fa001,:ic_ws001) from dual", new OracleParameter[] { op_fa001, op_ws001 }).ToString();
		}
		/// <summary>
		/// 收费作废
		/// </summary>
		/// <param name="fa001"></param>
		/// <param name="reason"></param>
		/// <param name="handler"></param>
		/// <returns></returns>
		public static int FinanceRemove(string fa001, string reason, string handler)
		{
			//结算流水号
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;
 
			//作废原因
			OracleParameter op_reason = new OracleParameter("ic_reason", OracleDbType.Varchar2, 100);
			op_reason.Direction = ParameterDirection.Input;
			op_reason.Value = reason;

			//经办人
			OracleParameter op_handler = new OracleParameter("ic_handler", OracleDbType.Varchar2, 10);
			op_handler.Direction = ParameterDirection.Input;
			op_handler.Value = handler;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_FinanceRemove", new OracleParameter[] { op_fa001, op_reason, op_handler });
		}

		/// <summary>
		/// 是否办理过退费
		/// </summary>
		/// <param name="fa001"></param>
		/// <returns></returns>
		public static string HaveRefund(string fa001)
		{
			OracleParameter op_fa001 = new OracleParameter("ic_fa001", OracleDbType.Varchar2, 10);
			op_fa001.Direction = ParameterDirection.Input;
			op_fa001.Value = fa001;

			return SqlAssist.ExecuteScalar("select pkg_business.fun_haveRefund(:ic_fa001) from dual", new OracleParameter[] { op_fa001 }).ToString();
		}
		/// <summary>
		/// 映射服务商品
		/// </summary>
		/// <param name="itemId"></param>
		/// <returns></returns>
		public static string Mapper_item(string itemId)
		{
			OracleParameter op_itemId = new OracleParameter("ic_itemId", OracleDbType.Varchar2, 10);
			op_itemId.Direction = ParameterDirection.Input;
			op_itemId.Value = itemId;
			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_mapper_item", new OracleParameter[] { op_itemId });
			return re.ToString();
		}

		/// <summary>
		///	财务综合统计-笔数	
		/// </summary>
		/// <param name="sbegin"></param>
		/// <param name="send"></param>
		/// <returns></returns>
		public static int FinStat_bs(string sbegin,string send)
		{
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 10);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = sbegin;

			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 10);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = send;
			;
			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_finstat_bs", new OracleParameter[] { op_begin,op_end });
			return Convert.ToInt32(re.ToString());
		}

		/// <summary>
		///	财务综合统计-金额	
		/// </summary>
		/// <param name="sbegin"></param>
		/// <param name="send"></param>
		/// <returns></returns>
		public static decimal FinStat_je(string sbegin, string send)
		{
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 10);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = sbegin;

			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 10);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = send;
			;
			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_finstat_je", new OracleParameter[] { op_begin, op_end });
			return Convert.ToDecimal(re.ToString());
		}

		/// <summary>
		/// 设置 逝者附加信息
		/// </summary>
		/// <param name="ac001"></param>
		/// <returns></returns>
		public static int SetExtraInfo(string ac001)
		{
			//购墓流水号
			OracleParameter op_ac001 = new OracleParameter("ic_ac001", OracleDbType.Varchar2, 10);
			op_ac001.Direction = ParameterDirection.Input;
			op_ac001.Value = ac001;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_SetExtraInfo", new OracleParameter[] { op_ac001 });
		}
		/// <summary>
		/// 收款员统计
		/// </summary>
		/// <param name="s_begin"></param>
		/// <param name="s_end"></param>
		/// <returns></returns>
		public static int CashierStat(string uc001,string s_begin,string s_end)
		{
			//收款员
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc001;

			//统计起始日期
			OracleParameter op_begin = new OracleParameter("ic_begin", OracleDbType.Varchar2, 10);
			op_begin.Direction = ParameterDirection.Input;
			op_begin.Value = s_begin;

			//统计终止日期
			OracleParameter op_end = new OracleParameter("ic_end", OracleDbType.Varchar2, 10);
			op_end.Direction = ParameterDirection.Input;
			op_end.Value = s_end;

			return SqlAssist.ExecuteProcedure("pkg_report.prc_cashier_stat", new OracleParameter[] { op_uc001,op_begin,op_end });
		}
	}
}
