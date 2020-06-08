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
using green.xpo.orcl;
using green.Action;

namespace green.Form
{
    public partial class Frm_payrecord : MyDialog
    {
        private string ac001 = string.Empty;
        private DataTable dt_pr01 = new DataTable("PR01");
        private OracleDataAdapter pr01Adapter = new OracleDataAdapter("select * from V_PRINT_PAYRECORD where ac001 = :ac001", SqlAssist.conn);
        private OracleParameter op_ac001 = new OracleParameter("ac001", OracleDbType.Varchar2, 10);
 
        public Frm_payrecord()
        {
            InitializeComponent();
            pr01Adapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_ac001 });
        }

        private void Frm_payrecord_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_pr01;
            ac001 = this.swapdata["ac001"].ToString();
            op_ac001.Value = ac001;

            pr01Adapter.Fill(dt_pr01);

        }

         

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "PR004")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "免管理费";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "正常缴纳";
            }
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("请先选择要打印的缴费记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int row = gridView1.GetSelectedRows()[0];
            string fa001 = string.Empty;

            if (row >= 0)
            {
                XtraMessageBox.Show("现在打印第" + (row + 1).ToString() + "条记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                fa001 = gridView1.GetRowCellValue(row, "PR001").ToString();
                PrintAction.PrintPayRecord(fa001);
            }
        }
    }
}