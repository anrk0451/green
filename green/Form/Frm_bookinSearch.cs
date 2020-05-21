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
using DevExpress.Data.XtraReports.Wizard.Presenters;

namespace green.Form
{
    public partial class Frm_bookinSearch : MyDialog
    {
        public Frm_bookinSearch()
        {
            InitializeComponent();
        }

        private void Frm_bookinSearch_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Tools.GetServerDate().AddMonths(-1);
            dateEdit2.EditValue = Tools.GetServerDate();
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_bk003 = string.Empty;
            string s_begin = string.Empty;
            string s_end = string.Empty;
            string s_status = string.Empty;

            if (string.IsNullOrEmpty(te_bk003.Text))
                s_bk003 = "%";
            else
                s_bk003 = te_bk003.Text + "%";

            if (string.IsNullOrEmpty(dateEdit1.Text))
                s_begin = "1900-01-01";
            else
                s_begin = dateEdit1.Text;

            if (string.IsNullOrEmpty(dateEdit2.Text))
                s_end = "2099-12-31";
            else
                s_end = dateEdit2.Text;

            switch (comboBoxEdit1.EditValue.ToString())
            {
                case "未过期":
                    s_status = "1";
                    break;
                case "全部":
                    s_status = "%";
                    break;
                case "已登记":
                    s_status = "2";
                    break;
                case "已过期":
                    s_status = "2";
                    break;
            }

            this.swapdata["bk003"] = s_bk003;
            this.swapdata["begin"] = s_begin;
            this.swapdata["end"] = s_end;
            this.swapdata["status"] = s_status;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        
    }
}