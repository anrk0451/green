using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using green.BaseObject;
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using green.Domain;
using green.Dao;
using green.Action;

namespace green.Form
{
    public partial class Frm_operator : MyDialog
    {
        private DataTable dt_roles = new DataTable("Ro01");
        private OracleDataAdapter ro01Adapter = new OracleDataAdapter("select * from ro01 where status = '1' ", SqlAssist.conn);
        private string action = string.Empty;
        private Uc01 uc01 = null;
        private Uc01_dao uc01_dao = new Uc01_dao();

        public Frm_operator()
        {
            InitializeComponent();
        }

        private void Frm_operator_Load(object sender, EventArgs e)
        {
            //1.设置角色列表数据源
            clbx_roles.DataSource = dt_roles;
            clbx_roles.DisplayMember = "RO003";
            clbx_roles.ValueMember = "RO001";
            ro01Adapter.Fill(dt_roles);

            //2.检索信息
            string s_uc001 = string.Empty;
            action = this.swapdata["action"].ToString();
            if (action == "add")
            {
                this.Text = "新建";
                uc01 = new Uc01();
            }
            else if (this.swapdata["action"].ToString() == "edit")
            {
                this.Text = "编辑";
                s_uc001 = this.swapdata["uc001"].ToString();

                uc01 = uc01_dao.GetSingle(s => s.uc001 == s_uc001);

                txtedit_uc002.Text = uc01.uc002;
                txtedit_uc003.Text = uc01.uc003;
 
                txtedit_pwd.ReadOnly = true;
                txtedit_pwd2.ReadOnly = true; 

                Ur_Mapper_dao ur_Mapper_dao = new Ur_Mapper_dao();
                List<Ur_Mapper> mapper = ur_Mapper_dao.GetList(s => s.uc001 == s_uc001);
                if (mapper.Count > 0)
                {
                    for (int i = 0; i < clbx_roles.ItemCount; i++)
                    {
                        string ro001 = clbx_roles.GetItemValue(i).ToString();
                        if (mapper.FindIndex(x => x.ro001.Equals(ro001)) >= 0)
                        {
                            clbx_roles.SetItemChecked(i, true);
                        }
                    }
                }
            }

        }
        /// <summary>
        /// 关闭按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            //数据校验
            string s_uc002 = txtedit_uc002.Text;
            string s_uc003 = txtedit_uc003.Text;
            string s_uc004 = txtedit_pwd.Text;
            string s_uc004_2 = txtedit_pwd2.Text;

            if (String.IsNullOrEmpty(s_uc002))
            {
                txtedit_uc002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc002.ErrorText = "登录代码必须输入!";
                txtedit_uc002.Focus();
                return;
            }

            if (String.IsNullOrEmpty(s_uc003))
            {
                txtedit_uc003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_uc003.ErrorText = "姓名必须输入!";
                txtedit_uc003.Focus();
                return;
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                if (String.IsNullOrEmpty(s_uc004))
                {
                    txtedit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd.ErrorText = "密码必须输入!";
                    txtedit_pwd.Focus();
                    return;
                }
                else if (!String.Equals(s_uc004, s_uc004_2))
                {
                    txtedit_pwd2.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    txtedit_pwd2.ErrorText = "密码不一致!";
                    txtedit_pwd2.Focus();
                    return;
                }
            }


            /////// 保存过程 ///////
            uc01.uc002 = txtedit_uc002.Text;
            uc01.uc003 = txtedit_uc003.Text;
 
            List<string> ro001_list = new List<string>();
            foreach (DataRowView item in clbx_roles.CheckedItems)
            {
                ro001_list.Add(item["ro001"].ToString());
            }

            if (this.swapdata["action"].ToString() == "add")
            {
                uc01.uc001 = MiscAction.GetEntityPK("UC01");
                uc01.uc004 = Tools.EncryptWithMD5(s_uc004);
                if (MiscAction.CreateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    Tools.msg(MessageBoxIcon.Information, "提示", "保存成功!");          
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                if (MiscAction.UpdateOperator(uc01, ro001_list.ToArray()) > 0)
                {
                    Tools.msg(MessageBoxIcon.Information, "提示", "保存成功!");                     
                    DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}