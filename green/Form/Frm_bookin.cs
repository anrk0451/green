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
using DevExpress.XtraEditors.Controls;
using green.Misc;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.Xpo;

namespace green.Form
{
    public partial class Frm_bookin : MyDialog
    {
        private string s_bi001 = string.Empty;
        private BI01 bi01 = null;
        private BK01 bk01 = null;

        public Frm_bookin()
        {
            InitializeComponent();
        }

        private void buttonEdit1_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Frm_freeBit frm_free = new Frm_freeBit();
            if (frm_free.ShowDialog() == DialogResult.OK)
            {
                s_bi001 = frm_free.swapdata["bi001"].ToString();
                bi01 = session1.GetObjectByKey(typeof(BI01), s_bi001) as BI01;
                if (bi01 != null)
                {
                    be_position.Text = MiscAction.GetTombPosition(bi01.BI001);              
                }
            }
            frm_free.Dispose();
        }

        private void be_position_DoubleClick(object sender, EventArgs e)
        {
            this.buttonEdit1_ButtonClick(sender, new ButtonPressedEventArgs(be_position.Properties.Buttons[0]));
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(s_bi001) || bi01 == null)
            {
                be_position.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                be_position.ErrorText = "请先选择一个墓位!";
                return;
            }
            else if (string.IsNullOrEmpty(te_bk003.Text))
            {
                te_bk003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_bk003.ErrorText = "请输入预定人姓名!";
                te_bk003.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(te_bk005.Text))
            {
                te_bk005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_bk005.ErrorText = "请输入联系电话!";
                te_bk005.Focus();
                return;
            }
            else if (DateTime.Compare(Convert.ToDateTime(dateEdit1.EditValue.ToString()), Tools.GetServerDate()) < 0)
            {
                dateEdit1.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                dateEdit1.ErrorText = "预定截至日期必须大于当前日期!";
                dateEdit1.Focus();
                return;
            }
            try
            {
                session1.BeginTransaction();
                //1.保存 bi01
                bi01.STATUS = '3';    //使用情况  1-未使用 2-已使用 3-预定 4-冻结
                bi01.Save();
                //2.记录预定记录
                bk01 = new BK01(session1);
                bk01.BK001 = MiscAction.GetEntityPK("BK01");
                bk01.BK003 = te_bk003.Text;
                bk01.BK005 = te_bk005.Text;
                bk01.BI001 = bi01.BI001;                             //墓位编号
                bk01.BK009 = Convert.ToDateTime(dateEdit1.Text);     //预留截至日期
                bk01.BK100 = Envior.cur_userId;
                bk01.BK200 = Tools.GetServerDate().Date;
                bk01.STATUS = '1';
                bk01.Save();

                session1.CommitTransaction();
                Tools.msg(MessageBoxIcon.Information, "提示", "墓位预定成功!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ee)
            {
                session1.RollbackTransaction();
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());
            }



        }

        private void Frm_bookin_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Tools.GetServerDate().AddMonths(1);
        }
    }
}