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
    public partial class Frm_checkin : MyDialog
    {
        public Frm_checkin()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 调用 选择墓位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void be_position_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            Frm_freeBit frm_free = new Frm_freeBit();
            if(frm_free.ShowDialog() == DialogResult.OK)
            {

            }
            frm_free.Dispose();
        }
    }
}