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
using green.Misc;

namespace green.Form
{
    public partial class Frm_report_tombCancel : MyDialog
    {
        public Frm_report_tombCancel()
        {
            InitializeComponent();
        }

        private void Frm_report_tombCancel_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Tools.GetServerDate().AddMonths(-1);
            dateEdit2.EditValue = Tools.GetServerDate();
            te_ac003.Focus();
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["ac003"] = string.IsNullOrEmpty(te_ac003.Text) ? "%" : te_ac003.Text;
            this.swapdata["dbegin"] = dateEdit1.EditValue == null ? "1900-01-01" : dateEdit1.Text;
            this.swapdata["dend"] = dateEdit2.EditValue == null ? "2999-12-31" : Convert.ToDateTime(dateEdit2.EditValue).AddDays(1).ToShortDateString();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}