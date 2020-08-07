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
using System.Configuration;
using green.Misc;
using DevExpress.Xpo;
using green.xpo.orcl;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;

namespace green.Form
{
    public partial class Frm_login : DevExpress.XtraEditors.XtraForm
    {
        public Frm_login()
        {
            InitializeComponent();
        }

        private void b_exit_Click(object sender, EventArgs e)
        {
            if (Envior.cur_userId == null)   //如果是登录
                Application.Exit();
            else                             //如果是重新登陆.... 
                this.Dispose();
        }

        private void Frm_login_Shown(object sender, EventArgs e)
        {
            this.Focus();

            string lastuser = ConfigurationManager.OpenExeConfiguration(Application.ExecutablePath).AppSettings.Settings["lastusername"].Value.ToString();
            if (!string.IsNullOrEmpty(lastuser))
            {
                textEdit_user.Text = lastuser;
                textEdit_pwd.Focus();
            }
            else
            {
                textEdit_user.Focus();
            }
        }

        private void b_ok_Click(object sender, EventArgs e)
        {
			string s_userCode, s_pwd;
			s_userCode = textEdit_user.Text;
			s_pwd = textEdit_pwd.Text;

			if (string.IsNullOrEmpty(s_userCode))
			{
				MessageBox.Show("请输入用户代码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textEdit_user.Focus();
				return;
			}
			if (string.IsNullOrEmpty(s_pwd))
			{
				MessageBox.Show("请输入密码!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				textEdit_pwd.Focus();
				return;
			}
			/////////////////////  检索 密码  ///////////////////////////////
			CriteriaOperator criteria = CriteriaOperator.Parse("UC002='" + s_userCode + "' and STATUS = '1'");
			xpCollection1.Criteria = criteria;
			xpCollection1.LoadingEnabled = true;
			 
			if (xpCollection1.Count <=0)
			{
				textEdit_user.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
				textEdit_user.ErrorText = "用户不存在!";
				return;
			}
			else  
			{
				UC01 uc01 = xpCollection1[0] as UC01;

				if(Tools.EncryptWithMD5(s_pwd) != uc01.UC004)
				{
					textEdit_pwd.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
					textEdit_pwd.ErrorText = "密码错误!";
					return;
				}
				else
				{
					Envior.cur_userId = uc01.UC001;
					Envior.cur_userName = uc01.UC003;

					Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
					config.AppSettings.Settings["lastusername"].Value = s_userCode;
					config.Save(ConfigurationSaveMode.Modified);

					/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

					DialogResult = DialogResult.OK;
					this.Close();
				}				
			}
			 
		}

		private void Frm_login_Load(object sender, EventArgs e)
		{

		}
	}
}