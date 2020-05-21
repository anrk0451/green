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
using green.xpo.orcl;
using green.Action;
using green.Misc;

namespace green.Form
{
    public partial class Frm_tombQuit : MyDialog
    {
        private V_AC01_REPORT ac01 = null;
        public Frm_tombQuit()
        {
            InitializeComponent();
        }

        private void Frm_tombQuit_Load(object sender, EventArgs e)
        {
            ac01 = this.swapdata["ac01"] as V_AC01_REPORT;
            if(ac01 != null)
            {
                te_ac001.EditValue = ac01.AC001;
                te_ac003.EditValue = ac01.AC003;
                te_position.EditValue = ac01.POSITION;
                de_ac049.EditValue = ac01.AC049;
            }
            else
            {
                sb_ok.Enabled = false;
                XtraMessageBox.Show("数据传输错误!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            memoEdit1.Focus();
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_reason = string.Empty;
            
            if (string.IsNullOrEmpty(memoEdit1.Text))
            {
                memoEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                memoEdit1.ErrorText = "请输入弃墓原因!";
                memoEdit1.Focus();
                return;
            }            
            if (XtraMessageBox.Show("本操作将不可撤销,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            
            s_reason = memoEdit1.EditValue.ToString();
            if (BusinessAction.TombQuit(ac01.AC001,s_reason,Envior.cur_userId) > 0)
            {
                XtraMessageBox.Show("办理成功!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}