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
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid;

namespace green.BusinessObject
{
    public partial class InvoiceReport : BaseBusiness
    {
        private DataTable dt_invoice = new DataTable("INVOICE_REPORT");
        private DataTable dt_detail = new DataTable("DETAIL");
        private OracleDataAdapter invAdapter = new OracleDataAdapter("select * from v_invoice_report where to_char(INVOICEDATE,'yyyy-mm-dd') between :begin and :end ", SqlAssist.conn);
        private OracleDataAdapter detailAdapter = new OracleDataAdapter("select * from v_invoiceDetail_report where fp001 = :fp001", SqlAssist.conn);


        private OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 10);
        private OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 10);
        private OracleParameter op_fp001 = new OracleParameter("fp001", OracleDbType.Varchar2, 10);

        private DataView dv_inv1 = null;
        private DataView dv_inv2 = null;
        private DataView dv_inv3 = null;

        private string s_base_filter1 = " JSHJ >=0 AND FLAG = '1' ";
        private string s_base_filter2 = " JSHJ <0 AND FLAG = '1' ";
        private string s_base_filter3 = " FLAG = '0' ";

        public InvoiceReport()
        {
            InitializeComponent();
            invAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
            detailAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_fp001 });

            dv_inv1 = new DataView(dt_invoice);
            dv_inv2 = new DataView(dt_invoice);
            dv_inv3 = new DataView(dt_invoice);
             
            gridControl1.DataSource = dv_inv1;
            gridControl2.DataSource = dv_inv2;
            gridControl3.DataSource = dv_inv3;
            gridControl4.DataSource = dt_detail;
             
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
            string s_begin = Convert.ToDateTime(bi_begin.EditValue).ToString("yyyy-MM-dd");
            string s_end = Convert.ToDateTime(bi_end.EditValue).ToString("yyyy-MM-dd");
            if (string.IsNullOrEmpty(s_begin)) s_begin = "1900-01-01";
            if (string.IsNullOrEmpty(s_end)) s_end = "2099-12-31";

            op_begin.Value = s_begin;
            op_end.Value = s_end;

            this.RefreshData();

        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            this.Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            gridView2.BeginUpdate();
            gridView3.BeginUpdate();

            dt_invoice.Rows.Clear();
            invAdapter.Fill(dt_invoice);

            string s_filter1 = s_base_filter1;
            string s_filter2 = s_base_filter2;
            string s_filter3 = s_base_filter3;

            if (Convert.ToBoolean(bi_only.EditValue))
            {
                s_filter1 += " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
                s_filter2 += " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
                s_filter3 += " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
            }

            dv_inv1.RowFilter = s_filter1;
            dv_inv2.RowFilter = s_filter2;
            dv_inv3.RowFilter = s_filter3;

            ///价税合计
            gridColumn_jshj.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj.SummaryItem.DisplayFormat = "{0:N2}";

            gridColumn_jshj2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj2.SummaryItem.DisplayFormat = "{0:N2}";

            gridColumn_jshj3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj3.SummaryItem.DisplayFormat = "{0:N2}";


            /////汇总信息            
            lc_zs_ps.Text = gridView1.RowCount.ToString();
            lc_zs_je.Text = gridView1.RowCount==0? "0.00" : string.Format("{0:N2}",gridView1.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_zf_ps.Text = gridView3.RowCount.ToString();
            lc_zf_je.Text = gridView3.RowCount == 0 ? "0.00" : string.Format("{0:N2}", gridView3.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_fs_ps.Text = gridView2.RowCount.ToString();
            lc_fs_je.Text = gridView2.RowCount == 0 ? "0.00" : string.Format("{0:N2}", gridView2.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_total_ps.Text = (gridView1.RowCount + gridView2.RowCount).ToString();
            lc_total_je.Text = string.Format("{0:N2}", Convert.ToDecimal(gridView1.Columns["JSHJ"].SummaryItem.SummaryValue) + Convert.ToDecimal(gridView2.Columns["JSHJ"].SummaryItem.SummaryValue));

            gridView1.EndUpdate();
            gridView2.EndUpdate();
            gridView3.EndUpdate();
            this.Cursor = Cursors.Arrow;
        }

         
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
		{
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
        }
        /// <summary>
        /// 选择 只看自己
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void bi_only_EditValueChanged(object sender, EventArgs e)
		{
            if (dt_invoice.Rows.Count == 0) return;
            string s_filter1 = string.Empty;    
            string s_filter2 = string.Empty;   
            string s_filter3 = string.Empty;  
            if (Convert.ToBoolean(bi_only.EditValue))   ///只看自己
			{
                s_filter1 = s_base_filter1 + " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
                s_filter2 = s_base_filter2 + " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
                s_filter3 = s_base_filter3 + " AND INFOINVOICER ='" + Envior.cur_userId + "' ";
            }
			else
			{
                s_filter1 = s_base_filter1;
                s_filter2 = s_base_filter2;
                s_filter3 = s_base_filter3;
            }

            dv_inv1.RowFilter = s_filter1;
            dv_inv2.RowFilter = s_filter2;
            dv_inv3.RowFilter = s_filter3;

            ///价税合计
            gridColumn_jshj.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj.SummaryItem.DisplayFormat = "{0:N2}";

            gridColumn_jshj2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj2.SummaryItem.DisplayFormat = "{0:N2}";

            gridColumn_jshj3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
            gridColumn_jshj3.SummaryItem.DisplayFormat = "{0:N2}";


            /////汇总信息            
            lc_zs_ps.Text = gridView1.RowCount.ToString();
            lc_zs_je.Text = gridView1.RowCount == 0 ? "0.00" : string.Format("{0:N2}", gridView1.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_zf_ps.Text = gridView3.RowCount.ToString();
            lc_zf_je.Text = gridView3.RowCount == 0 ? "0.00" : string.Format("{0:N2}", gridView3.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_fs_ps.Text = gridView2.RowCount.ToString();
            lc_fs_je.Text = gridView2.RowCount == 0 ? "0.00" : string.Format("{0:N2}", gridView2.Columns["JSHJ"].SummaryItem.SummaryValue);

            lc_total_ps.Text = (gridView1.RowCount + gridView2.RowCount).ToString();
            lc_total_je.Text = string.Format("{0:N2}", Convert.ToDecimal(gridView1.Columns["JSHJ"].SummaryItem.SummaryValue) + Convert.ToDecimal(gridView2.Columns["JSHJ"].SummaryItem.SummaryValue));


        }

		private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            this.RefreshData();
		}

		private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
            if (dt_invoice.Rows.Count == 0) return;
            if(xtraTabControl1.SelectedTabPageIndex == 0)
			{
                if (gridView1.IsFindPanelVisible)
                    gridView1.HideFindPanel();
                else
                    gridView1.ShowFindPanel();
			}
            else if(xtraTabControl1.SelectedTabPageIndex == 1)
			{
                if (gridView2.IsFindPanelVisible)
                    gridView2.HideFindPanel();
                else
                    gridView2.ShowFindPanel();
            }
            else if (xtraTabControl1.SelectedTabPageIndex == 2)
            {
                if (gridView3.IsFindPanelVisible)
                    gridView3.HideFindPanel();
                else
                    gridView3.ShowFindPanel();
            }
        }

		
        /// <summary>
        /// 检索明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
            int rowHandle = gridView1.FocusedRowHandle;
            string s_fp001 = string.Empty;
            string s_invnum = string.Empty;
            if(rowHandle >= 0)
			{
                s_fp001 = gridView1.GetRowCellValue(rowHandle, "FP001").ToString();
                s_invnum = gridView1.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                dt_detail.Rows.Clear();
                op_fp001.Value = s_fp001;
                detailAdapter.Fill(dt_detail);
                gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
			}
		}
        /// <summary>
        /// 导出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            GridControl grid = null;
			switch (xtraTabControl1.SelectedTabPageIndex)
			{
                case 0:
                    grid = gridControl1;
                    break;
                case 1:
                    grid = gridControl2;
                    break;
                case 2:
                    grid = gridControl3;
                    break;
			}
           
            SaveFileDialog fileDialog = new SaveFileDialog();
            fileDialog.Title = "导出Excel";
            fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

            DialogResult dialogResult = fileDialog.ShowDialog(this);
            if (dialogResult == DialogResult.OK)
            {
                DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
                options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
                grid.ExportToXlsx(fileDialog.FileName, options);
                XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

		private void gridView2_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
            int rowHandle = gridView2.FocusedRowHandle;
            string s_fp001 = string.Empty;
            string s_invnum = string.Empty;
            if (rowHandle >= 0)
            {
                s_fp001 = gridView2.GetRowCellValue(rowHandle, "FP001").ToString();
                s_invnum = gridView2.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                dt_detail.Rows.Clear();
                op_fp001.Value = s_fp001;
                detailAdapter.Fill(dt_detail);
                gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
            }
        }

		private void gridView3_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
		{
            int rowHandle = gridView3.FocusedRowHandle;
            string s_fp001 = string.Empty;
            string s_invnum = string.Empty;
            if (rowHandle >= 0)
            {
                s_fp001 = gridView3.GetRowCellValue(rowHandle, "FP001").ToString();
                s_invnum = gridView3.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                dt_detail.Rows.Clear();
                op_fp001.Value = s_fp001;
                detailAdapter.Fill(dt_detail);
                gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
            }
        }

        /// <summary>
        /// 选择标签改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
		private void xtraTabControl1_TabIndexChanged(object sender, EventArgs e)
		{
            XtraMessageBox.Show(xtraTabControl1.SelectedTabPageIndex.ToString());
		}

		private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
		{
            dt_detail.Rows.Clear();
            gridView4.ViewCaption = "发票明细";
            int i_index = xtraTabControl1.SelectedTabPageIndex;
            int rowHandle;
            string s_fp001 = string.Empty;
            string s_invnum = string.Empty;
            if (i_index == 0)
			{
                rowHandle = gridView1.FocusedRowHandle;
                if(rowHandle >= 0)
				{
                    s_fp001 = gridView1.GetRowCellValue(rowHandle, "FP001").ToString();
                    s_invnum = gridView1.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                    op_fp001.Value = s_fp001;
                    detailAdapter.Fill(dt_detail);
                    gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
                }                
            }
            else if(i_index == 1)
			{
                rowHandle = gridView2.FocusedRowHandle;
                if(rowHandle >= 0)
				{
                    s_fp001 = gridView2.GetRowCellValue(rowHandle, "FP001").ToString();
                    s_invnum = gridView2.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                    op_fp001.Value = s_fp001;
                    detailAdapter.Fill(dt_detail);
                    gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
                }                
            }
            else if(i_index == 2)
			{
                rowHandle = gridView3.FocusedRowHandle;
				if (rowHandle >= 0)
				{
                    s_fp001 = gridView3.GetRowCellValue(rowHandle, "FP001").ToString();
                    s_invnum = gridView3.GetRowCellValue(rowHandle, "INVOICENUM").ToString();
                    op_fp001.Value = s_fp001;
                    detailAdapter.Fill(dt_detail);
                    gridView4.ViewCaption = "发票明细" + "【" + s_invnum + "】";
                }                
            }
        }
	}
}
