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
	}
}
