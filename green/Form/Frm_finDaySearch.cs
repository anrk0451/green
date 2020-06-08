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
    public partial class Frm_finDaySearch : MyDialog
    {
        public Frm_finDaySearch()
        {
            InitializeComponent();
        }

        private void Frm_finDaySearch_Load(object sender, EventArgs e)
        {
            dateEdit1.EditValue = Tools.GetServerDate();
            dateEdit2.EditValue = dateEdit1.EditValue;
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            this.swapdata["dbegin"] = dateEdit1.EditValue.ToString();
            this.swapdata["dend"] = Convert.ToDateTime(dateEdit2.EditValue).AddDays(1).ToString();
            
            this.swapdata["fa003"] = string.IsNullOrEmpty(textEdit1.Text) ? "%" : textEdit1.Text;
            switch (comboBoxEdit1.Text)
            {
                case "全部":
                    this.swapdata["fa002"] = "%";
                    break;
                case "购墓":
                    this.swapdata["fa002"] = "0";
                    break;
                case "服务祭品":
                    this.swapdata["fa002"] = "1";
                    break;
                case "管理费":
                    this.swapdata["fa002"] = "1";
                    break;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}