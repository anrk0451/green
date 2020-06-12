using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using green.BaseObject;
using Oracle.ManagedDataAccess.Client;
using green.Misc;

namespace green.BusinessObject
{
    public partial class InvoiceReport : BaseBusiness
    {
        private DataTable dt_invoice = new DataTable("INVOICE_REPORT");
        private OracleDataAdapter invAdapter = new OracleDataAdapter("select * from v_invoice_report where to_char(INVOICEDATE,'yyyy-mm-dd') between :begin and :end ", SqlAssist.conn);
        private OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 10);
        private OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 10);
        private DataView dv_inv1 = null;
        private DataView dv_inv2 = null;
        private DataView dv_inv3 = null;

        public InvoiceReport()
        {
            InitializeComponent();
            invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });

            dv_inv1 = new DataView(dt_invoice);
            dv_inv1.RowFilter = "";

            dv_inv2 = new DataView(dt_invoice);
            dv_inv2.RowFilter = "";

            gridControl1.DataSource = dv_inv1;
            gridControl2.DataSource = dv_inv2;
            gridControl3.DataSource = dv_inv3;


        }

        private void InvoiceReport_Load(object sender, EventArgs e)
        {
            bi_begin.EditValue = Tools.GetServerDate();
            bi_end.EditValue = Tools.GetServerDate();
        }
        /// <summary>
        /// 执行查询统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string s_begin = bi_begin.EditValue.ToString();
            string s_end = bi_end.EditValue.ToString();
            if (string.IsNullOrEmpty(s_begin)) s_begin = "1900-01-01";
            if (string.IsNullOrEmpty(s_end)) s_end = "2099-12-31";

            op_begin.Value = s_begin;
            op_end.Value = s_end;
        }
    }
}
