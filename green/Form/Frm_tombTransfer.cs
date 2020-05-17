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
using DevExpress.XtraEditors.Controls;

namespace green.Form
{
    public partial class Frm_tombTransfer : MyDialog
    {
        private V_AC01_REPORT ac01 = null;
        private string s_new_bi001 = string.Empty;

        public Frm_tombTransfer()
        {
            InitializeComponent();
        }

        private void Frm_tombTransfer_Load(object sender, EventArgs e)
        {
            ac01 = this.swapdata["ac01"] as V_AC01_REPORT;
            if(ac01 != null)
            {
                te_ac001.EditValue = ac01.AC001;
                te_position.EditValue = ac01.POSITION;
                te_ac003.EditValue = ac01.AC003;
                te_price.EditValue = ac01.AC020;
            }
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void be_newposition_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Frm_freeBit frm_free = new Frm_freeBit();
            BI01 bi01 = null;

            if (frm_free.ShowDialog() == DialogResult.OK)
            {
                s_new_bi001 = frm_free.swapdata["bi001"].ToString();
                if(s_new_bi001 == ac01.AC015)
                {
                    Tools.msg(MessageBoxIcon.Warning, "提示", "新墓穴位置与原来相同!");
                    return;
                }
                bi01 = session1.GetObjectByKey(typeof(BI01), s_new_bi001) as BI01;
                if (bi01 != null)
                {
                    if (bi01.PRICE != ac01.AC020) 
                    { 
                        if(XtraMessageBox.Show("新墓穴位置与原位置定价不同,是否继续?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }
                    be_newposition.Text = MiscAction.GetTombPosition(bi01.BI001);
                    te_new_price.EditValue = bi01.PRICE;
                }
            }
            frm_free.Dispose();
        }

        private void be_newposition_DoubleClick(object sender, EventArgs e)
        {
            this.be_newposition_ButtonClick(sender, new ButtonPressedEventArgs(be_newposition.Properties.Buttons[0]));
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(s_new_bi001))
            {
                be_newposition.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                be_newposition.ErrorText = "请先选择一个墓穴位置!";
                return;
            }
            if(string.IsNullOrEmpty(te_new_price.Text) || Convert.ToDecimal(te_new_price.Text) <= 0)
            {
                be_newposition.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                be_newposition.ErrorText = "新墓穴位置尚未定价!";
                return;
            }

            if(BusinessAction.TombTransfer(ac01.AC001,s_new_bi001,me_reason.Text,Envior.cur_userId) > 0)
            {
                Tools.msg(MessageBoxIcon.Information, "提示", "办理成功!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}