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
using green.Action;

namespace green.Form
{
    public partial class Frm_InvoiceDisplay : MyDialog
    {
        private DataTable dt_invoice_info = new DataTable();
        private OracleDataAdapter invAdapter = new OracleDataAdapter("select * from v_have_invoiced where fa001 = :fa001", SqlAssist.conn);
        private OracleParameter op_fa001 = null;
        private string s_fa001 = string.Empty;

        public Frm_InvoiceDisplay()
        {
            InitializeComponent();
            op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
            op_fa001.Direction = ParameterDirection.Input;

            invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa001 });
        }

        private void Frm_InvoiceDisplay_Load(object sender, EventArgs e)
        {
            s_fa001 = this.swapdata["fa001"].ToString();
            op_fa001.Value = s_fa001;
            invAdapter.Fill(dt_invoice_info);

            gridControl1.DataSource = dt_invoice_info;
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_fpdm = string.Empty;
            string s_fphm = string.Empty;

            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("请先选择要打印的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach(int i in gridView1.GetSelectedRows())
            {
                s_fpdm = gridView1.GetRowCellValue(i, "INVOICECODE").ToString();
                s_fphm = gridView1.GetRowCellValue(i, "INVOICENUM").ToString();
                if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "打印发票", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TaxInvoice.PrintInvoice(s_fpdm,s_fphm, "0");
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            string s_fpdm = string.Empty;
            string s_fphm = string.Empty;

            if (gridView1.SelectedRowsCount == 0)
            {
                XtraMessageBox.Show("请先选择要打印的记录!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            foreach (int i in gridView1.GetSelectedRows())
            {
                s_fpdm = gridView1.GetRowCellValue(i, "INVOICECODE").ToString();
                s_fphm = gridView1.GetRowCellValue(i, "INVOICENUM").ToString();
                if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "打印清单", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    TaxInvoice.PrintInvoice(s_fpdm, s_fphm, "1");
                }
            }
        }
    }
}