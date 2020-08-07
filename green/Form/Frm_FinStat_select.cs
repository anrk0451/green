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

namespace green.Form
{
	public partial class Frm_FinStat_select : MyDialog
	{
		public Frm_FinStat_select()
		{
			InitializeComponent();
		}

		private void sb_cancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void sb_ok_Click(object sender, EventArgs e)
		{
			int i_index = Convert.ToInt32(radioGroup1.EditValue);
			this.swapdata["index"] = i_index;
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void Frm_FinStat_select_Load(object sender, EventArgs e)
		{
			radioGroup1.EditValue = 0;
		}
	}
}