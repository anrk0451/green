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
using green.Action;
using DevExpress.XtraPrinting;

namespace green.BusinessObject
{
	public partial class CashierStat : BaseBusiness
	{
		private DataTable dt_source = new DataTable();
		private OracleDataAdapter souAdapter = new OracleDataAdapter("select * from v_cashierStat order by uc001", SqlAssist.conn);

		public CashierStat()
		{
			InitializeComponent();
		}

		private void CashierStat_Load(object sender, EventArgs e)
		{
			gridControl1.DataSource = dt_source;
			bi_begin.EditValue = Tools.GetServerDate();
			bi_end.EditValue = bi_begin.EditValue;
		}
		/// <summary>
		/// 执行查询统计
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.DoSearch();
		}

		private void DoSearch()
		{
			string s_begin = string.Empty;
			string s_end = string.Empty;
			string s_uc001 = "%";
			if (bi_begin.EditValue == null)
				s_begin = "1900-01-01";
			else
				s_begin = Convert.ToDateTime(bi_begin.EditValue).ToString("yyyy-MM-dd");


			if (bi_end.EditValue == null)
				s_end = "2999-12-31";
			else
				s_end = Convert.ToDateTime(bi_end.EditValue).ToString("yyyy-MM-dd");

			if (BusinessAction.CashierStat(s_uc001, s_begin, s_end) == 1)
			{
				this.Cursor = Cursors.WaitCursor;
				gridView1.BeginUpdate();
				dt_source.Rows.Clear();
				souAdapter.Fill(dt_source);
				gridView1.EndUpdate();
				this.Cursor = Cursors.Arrow;

				gridColumn2.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn2.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn3.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn3.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn4.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn5.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn5.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn6.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn6.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn7.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn8.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn8.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn9.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn9.SummaryItem.DisplayFormat = "{0:N2}";

				gridColumn10.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn10.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn11.SummaryItem.DisplayFormat = "{0:N0}";

				gridColumn12.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
				gridColumn12.SummaryItem.DisplayFormat = "{0:N2}";

			}
		}

		private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.DoSearch();
		}
		/// <summary>
		/// 导出
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			SaveFileDialog fileDialog = new SaveFileDialog();
			fileDialog.Title = "导出Excel";
			fileDialog.Filter = "Excel文件(*.xlsx)|*.xlsx";

			DialogResult dialogResult = fileDialog.ShowDialog(this);
			if (dialogResult == DialogResult.OK)
			{
				DevExpress.XtraPrinting.XlsxExportOptions options = new DevExpress.XtraPrinting.XlsxExportOptions();
				options.TextExportMode = TextExportMode.Text;//设置导出模式为文本
				gridControl1.ExportToXlsx(fileDialog.FileName, options);
				XtraMessageBox.Show("导出成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
	}
}
