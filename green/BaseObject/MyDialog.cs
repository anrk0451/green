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

namespace green.BaseObject
{
    public partial class MyDialog : DevExpress.XtraEditors.XtraForm
    {
        public Dictionary<string, Object> swapdata { get; set; }
        public MyDialog()
        {
            InitializeComponent();
            swapdata = new Dictionary<string, object>();
        }

		/// <summary>
		/// 回车转tab
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void BaseDialog_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (System.Convert.ToInt32(e.KeyChar) == 13)
			{
				System.Windows.Forms.SendKeys.Send("{tab}");
			}
		}
	}
}