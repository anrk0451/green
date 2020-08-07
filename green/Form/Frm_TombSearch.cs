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
using System.Windows.Forms.VisualStyles;

namespace green.Form
{
    public partial class Frm_TombSearch : MyDialog
    {
        public Frm_TombSearch()
        {
            InitializeComponent();
        }

        
        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_TombSearch_Load(object sender, EventArgs e)
        {

        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            bool b_full = true;
            if (string.IsNullOrEmpty(te_ac001.Text))
            {
                this.swapdata["ac001"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["ac001"] = te_ac001.Text + "%";
            }
            if (string.IsNullOrEmpty(te_ac003.Text))
            {
                this.swapdata["ac003"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["ac003"] = te_ac003.Text + "%";
            }
            if (string.IsNullOrEmpty(te_ac050.Text))
            {
                this.swapdata["ac050"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["ac050"] = te_ac050.Text + "%";
            }
            if( le_region.EditValue == null || le_region.EditValue is DBNull)
            {
                this.swapdata["rg001"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["rg001"] = le_region.EditValue.ToString();
            }
            
            if (string.IsNullOrEmpty(te_bi003.Text))
            {
                this.swapdata["bi003"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["bi003"] = te_bi003.Text;
            }

            if (string.IsNullOrEmpty(te_ac113.Text))
            {
                this.swapdata["ac113"] = "%";
            }
            else
            {
                b_full = false;
                this.swapdata["ac113"] = te_ac113.Text + "%";
            }

            if (!string.IsNullOrEmpty(comboBoxEdit1.Text))
            {
                b_full = false;
                this.swapdata["range"] = comboBoxEdit1.Text;
            }





            if (b_full)
            {
                if (XtraMessageBox.Show("还未输入任何条件,将检索出所有记录,是否继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            }
 
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}