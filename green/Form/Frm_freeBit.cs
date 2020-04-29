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
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using DevExpress.XtraTreeList;

namespace green.Form
{
    public partial class Frm_freeBit : MyDialog
    {
        private DataTable dt_rg01 = new DataTable("Rg01");
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;

        private OracleDataAdapter rg01Adapter = new OracleDataAdapter("select * from rg01 where rg002 in ('0','1') order by rg001", SqlAssist.conn);

        public Frm_freeBit()
        {
            InitializeComponent();
            rg01Adapter.Fill(dt_rg01);
        }

        private void Frm_freeBit_Load(object sender, EventArgs e)
        {
            treeList1.DataSource = dt_rg01;
            treeList1.ExpandToLevel(1);
        }
        /// <summary>
        /// 节点焦点改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

        }
    }
}