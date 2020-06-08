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

namespace green.Form
{
    public partial class Frm_InvoiceInfo : MyDialog
    {
        private DataTable dt_invoice_info = new DataTable();
        private OracleDataAdapter invAdapter = new OracleDataAdapter("select * from v_have_invoiced where fa001 = :fa001", SqlAssist.conn);
        private OracleParameter op_fa001 = null;
        private string s_fa001 = string.Empty;

        public Frm_InvoiceInfo()
        {
            InitializeComponent();
            op_fa001 = new OracleParameter("fa001", OracleDbType.Varchar2, 10);
            op_fa001.Direction = ParameterDirection.Input;

            invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fa001 });
        }

        private void Frm_InvoiceInfo_Load(object sender, EventArgs e)
        {
            s_fa001 = this.swapdata["fa001"].ToString();
            op_fa001.Value = s_fa001;
            invAdapter.Fill(dt_invoice_info);

            gridControl1.DataSource = dt_invoice_info;
        }
    }
}