using DevExpress.XtraEditors;
using green.Domain;
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
    class MiscAction
    {
        /// <summary>
        /// 实体键生成器
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetEntityPK(string entity)
        {

            OracleCommand cmd = new OracleCommand("pkg_business.fun_EntityPk", SqlAssist.conn);
            cmd.CommandType = System.Data.CommandType.StoredProcedure;

            OracleParameter returnValue = new OracleParameter("result", OracleDbType.Varchar2, 50);
            returnValue.Direction = ParameterDirection.ReturnValue;

            OracleParameter entityName = new OracleParameter("EntityName", OracleDbType.Varchar2, 50);
            entityName.Direction = ParameterDirection.Input;
            entityName.Size = 50;
            entityName.Value = entity;

            try
            {
                cmd.Parameters.Add(returnValue);
                cmd.Parameters.Add(entityName);
                cmd.ExecuteNonQuery();
            }
            catch (InvalidOperationException e)
            {
                Tools.msg(MessageBoxIcon.Error,"错误", "获取实体主键错误!\n" + e.ToString());
            }
            finally
            {
                cmd.Dispose();
            }

            return returnValue.Value.ToString();
        }

		/// <summary>
		/// 创建新的操作员
		/// </summary>
		/// <param name="uc01"></param>
		/// <param name="rolesarry"></param>
		/// <returns></returns>
		public static int CreateOperator(Uc01 uc01, string[] rolesarry)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc01.uc001;

			//用户代码
			OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
			op_uc002.Direction = ParameterDirection.Input;
			op_uc002.Value = uc01.uc002;

			//用户姓名
			OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
			op_uc003.Direction = ParameterDirection.Input;
			op_uc003.Value = uc01.uc003;

			//用户密码
			OracleParameter op_uc004 = new OracleParameter("ic_uc004", OracleDbType.Varchar2, 50);
			op_uc004.Direction = ParameterDirection.Input;
			op_uc004.Value = uc01.uc004;

			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
			op_roles_arry.Direction = ParameterDirection.Input;
			op_roles_arry.Value = string.Join("|", rolesarry);

			return SqlAssist.ExecuteProcedure("pkg_business.prc_CreateOperator", new OracleParameter[] { op_uc001, op_uc002, op_uc003, op_uc004, op_roles_arry });
		}

		/// <summary>
		/// 修改用户
		/// </summary>
		/// <returns></returns>
		public static int UpdateOperator(Uc01 uc01, string[] rolesarry)
		{
			//用户编号
			OracleParameter op_uc001 = new OracleParameter("ic_uc001", OracleDbType.Varchar2, 10);
			op_uc001.Direction = ParameterDirection.Input;
			op_uc001.Value = uc01.uc001;

			//用户代码
			OracleParameter op_uc002 = new OracleParameter("ic_uc002", OracleDbType.Varchar2, 50);
			op_uc002.Direction = ParameterDirection.Input;
			op_uc002.Value = uc01.uc002;

			//用户姓名
			OracleParameter op_uc003 = new OracleParameter("ic_uc003", OracleDbType.Varchar2, 50);
			op_uc003.Direction = ParameterDirection.Input;
			op_uc003.Value = uc01.uc003;
			 
			//用户角色数组
			OracleParameter op_roles_arry = new OracleParameter("ic_roles", OracleDbType.Varchar2, 500);
			op_roles_arry.Direction = ParameterDirection.Input;
			op_roles_arry.Value = string.Join("|", rolesarry);

			return SqlAssist.ExecuteProcedure("pkg_business.prc_UpdateOperator",
				new OracleParameter[] { op_uc001, op_uc002, op_uc003, op_roles_arry });
		}


		/// <summary>
		/// 获取墓位状态
		/// </summary>
		/// <param name="regionId"></param>
		/// <param name="bitDesc"></param>
		/// <returns></returns>
		public static string GetTombStatus(string rowerId,string bitDesc)
		{
			OracleParameter op_rowerId = new OracleParameter("ic_rowerId", OracleDbType.Varchar2, 10);
			op_rowerId.Direction = ParameterDirection.Input;
			op_rowerId.Value = rowerId;

			OracleParameter op_bitDesc = new OracleParameter("ic_bitDesc", OracleDbType.Varchar2, 50);
			op_bitDesc.Direction = ParameterDirection.Input;
			op_bitDesc.Value = bitDesc;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_GetTombStatus", new OracleParameter[] { op_rowerId, op_bitDesc });
			return re.ToString();
		}
		/// <summary>
		/// 获取系统参数1
		/// </summary>
		/// <param name="propId"></param>
		/// <returns></returns>
		public static decimal GetSysParaValue1(string propId)
		{
			OracleParameter op_propId = new OracleParameter("ic_propId", OracleDbType.Varchar2, 50);
			op_propId.Direction = ParameterDirection.Input;
			op_propId.Value = propId;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_GetSysParaValue1", new OracleParameter[] { op_propId});
			return Convert.ToDecimal(re.ToString());
		}
		/// <summary>
		/// 获取墓位位置
		/// </summary>
		/// <param name="bi001"></param>
		/// <returns></returns>
		public static string GetTombPosition(string bi001)
		{
			OracleParameter op_bi001 = new OracleParameter("ic_bi001", OracleDbType.Varchar2, 10);
			op_bi001.Direction = ParameterDirection.Input;
			op_bi001.Value = bi001;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_GetTombPosition", new OracleParameter[] { op_bi001 });
			return re.ToString();
		}

		/// <summary>
		/// 获取墓区排数
		/// </summary>
		/// <param name="regionId"></param>
		/// <returns></returns>
		public static int GetRowerCount(string regionId)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_getRowerCount", new OracleParameter[] { op_regionId });
			return Convert.ToInt32(re.ToString());
		}

		public static int GetMaxCols(string regionId)
		{
			OracleParameter op_regionId = new OracleParameter("ic_regionId", OracleDbType.Varchar2, 10);
			op_regionId.Direction = ParameterDirection.Input;
			op_regionId.Value = regionId;

			Object re = SqlAssist.ExecuteFunction("pkg_report.fun_getMaxCols", new OracleParameter[] { op_regionId });
			return Convert.ToInt32(re.ToString());
		}
		/// <summary>
		/// 根据排号和号位描述返回墓位号
		/// </summary>
		/// <param name="rowerId"></param>
		/// <param name="bitdesc"></param>
		/// <returns></returns>
		public static string GetTombId(string rowerId,string bitdesc)
		{
			OracleParameter op_rowerId = new OracleParameter("ic_rowerId", OracleDbType.Varchar2, 10);
			op_rowerId.Direction = ParameterDirection.Input;
			op_rowerId.Value = rowerId;

			OracleParameter op_bitdesc = new OracleParameter("ic_bitDesc", OracleDbType.Varchar2, 50);
			op_bitdesc.Direction = ParameterDirection.Input;
			op_bitdesc.Value = bitdesc;

			Object re = SqlAssist.ExecuteFunction("pkg_business.fun_GetTombID", new OracleParameter[] { op_rowerId,op_bitdesc });
			return re.ToString();
		}

		/// <summary>
		/// 保存税务发票基本信息
		/// </summary>
		/// <param name="url"></param>
		/// <param name="id"></param>
		/// <param name="appId"></param>
		/// <param name="addr"></param>
		/// <param name="bank"></param>
		/// <param name="fplx"></param>
		/// <param name="publicKey"></param>
		/// <param name="privateKey"></param>
		/// <param name="cashier"></param>
		/// <param name="checker"></param>
		/// <returns></returns>
		public static int SaveTaxInfo(string url, string id, string appId, string addr, string bank, string fplx, string publicKey, string privateKey, string cashier, string checker,decimal max)
		{
			//服务请求url
			OracleParameter op_url = new OracleParameter("ic_url", OracleDbType.Varchar2, 50);
			op_url.Direction = ParameterDirection.Input;
			op_url.Value = url;

			//纳税识别号
			OracleParameter op_id = new OracleParameter("ic_id", OracleDbType.Varchar2, 20);
			op_id.Direction = ParameterDirection.Input;
			op_id.Value = id;

			//AppId
			OracleParameter op_appId = new OracleParameter("ic_appId", OracleDbType.Varchar2, 50);
			op_appId.Direction = ParameterDirection.Input;
			op_appId.Value = appId;

			//地址、电话
			OracleParameter op_addr = new OracleParameter("ic_addr", OracleDbType.Varchar2, 100);
			op_addr.Direction = ParameterDirection.Input;
			op_addr.Value = addr;

			//银行、账号
			OracleParameter op_bank = new OracleParameter("ic_bank", OracleDbType.Varchar2, 100);
			op_bank.Direction = ParameterDirection.Input;
			op_bank.Value = bank;

			//发票类型
			OracleParameter op_fplx = new OracleParameter("ic_fplx", OracleDbType.Varchar2, 10);
			op_fplx.Direction = ParameterDirection.Input;
			op_fplx.Value = fplx;

			//公钥
			OracleParameter op_public = new OracleParameter("ic_public", OracleDbType.Varchar2, 20);
			op_public.Direction = ParameterDirection.Input;
			op_public.Value = publicKey;
			//私钥
			OracleParameter op_private = new OracleParameter("ic_private", OracleDbType.Varchar2, 20);
			op_private.Direction = ParameterDirection.Input;
			op_private.Value = privateKey;

			//收款人
			OracleParameter op_cashier = new OracleParameter("ic_cashier", OracleDbType.Varchar2, 20);
			op_cashier.Direction = ParameterDirection.Input;
			op_cashier.Value = cashier;

			//审核人
			OracleParameter op_checker = new OracleParameter("ic_checker", OracleDbType.Varchar2, 20);
			op_checker.Direction = ParameterDirection.Input;
			op_checker.Value = checker;

			//单张发票最大面额
			OracleParameter op_max = new OracleParameter("in_max", OracleDbType.Decimal);
			op_max.Direction = ParameterDirection.Input;
			op_max.Value = max;

			return SqlAssist.ExecuteProcedure("pkg_business.prc_SaveTaxInfo", new OracleParameter[] { op_url, op_id, op_appId, op_addr, op_bank, op_fplx, op_public, op_private, op_cashier, op_checker,op_max });
		}
	}
}
