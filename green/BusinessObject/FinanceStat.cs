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
using Newtonsoft.Json;
using DevExpress.XtraReports.UI;
using green.Report;
using DevExpress.DataAccess.Json;
using green.Form;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting;

namespace green.BusinessObject
{
	public partial class FinanceStat : BaseBusiness
	{
		private DataTable dt_tomb = new DataTable("TOMB");
		private OracleDataAdapter tombAdapter = 
			new OracleDataAdapter("select * from v_report_saleTomb where to_char(fa200,'yyyy-mm-dd') between :begin and :end", SqlAssist.conn);

		private DataTable dt_goodsService = new DataTable();
		private OracleDataAdapter gserviceAdapter =
			new OracleDataAdapter("select sa004,sa003,sum(nums) nums,sum(sa007) sa007 from v_report_goodService where to_char(fa200,'yyyy-mm-dd') between :begin and :end group by sa004,sa003", SqlAssist.conn);

		private DataTable dt_managefee = new DataTable();
		private OracleDataAdapter mfeeAdapter =
			new OracleDataAdapter("select * from v_report_managefee where to_char(fa200,'yyyy-mm-dd') between :begin and :end", SqlAssist.conn);

		private OracleParameter op_begin = new OracleParameter("begin", OracleDbType.Varchar2, 10);		
		private OracleParameter op_end = new OracleParameter("end", OracleDbType.Varchar2, 10);
		 
		public FinanceStat()
		{
			InitializeComponent();
		}

		private void FinanceStat_Load(object sender, EventArgs e)
		{
			bi_begin.EditValue = Tools.GetServerDate();
			bi_end.EditValue = bi_begin.EditValue;

			tombAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			gserviceAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });
			mfeeAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_begin, op_end });

			gridControl1.DataSource = dt_tomb;
			gridControl2.DataSource = dt_goodsService;
			gridControl3.DataSource = dt_managefee;
		}

		private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			DoSearch();
		}

		/// <summary>
		/// 执行查询
		/// </summary>
		private void DoSearch()
		{
			string s_begin = string.Empty;
			string s_end = string.Empty;

			if (string.IsNullOrEmpty(bi_begin.EditValue.ToString()))
				s_begin = "1900-01-01";
			else
				s_begin = Convert.ToDateTime(bi_begin.EditValue).ToString("yyyy-MM-dd");

			if (string.IsNullOrEmpty(bi_end.EditValue.ToString()))
				s_end = "1900-01-01";
			else
				s_end = Convert.ToDateTime(bi_end.EditValue).ToString("yyyy-MM-dd");

			op_begin.Value = s_begin;
			op_end.Value = s_end;

			this.Cursor = Cursors.WaitCursor;
			
			//1.售墓
			dt_tomb.Rows.Clear();
			tombAdapter.Fill(dt_tomb);

			gridColumn4.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			gridColumn4.SummaryItem.DisplayFormat = "{0:N2}";

			//2.服务祭品
			dt_goodsService.Rows.Clear();
			gserviceAdapter.Fill(dt_goodsService);

			gridColumn7.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			gridColumn7.SummaryItem.DisplayFormat = "{0:N2}";

			//3.管理费
			dt_managefee.Rows.Clear();
			mfeeAdapter.Fill(dt_managefee);

			gridColumn11.SummaryItem.SummaryType = DevExpress.Data.SummaryItemType.Sum;
			gridColumn11.SummaryItem.DisplayFormat = "{0:N2}";
 
			//计算汇总数据
			decimal dec_tomb = decimal.Zero;
			decimal dec_tomb_refund = decimal.Zero;
			decimal dec_sgoods = decimal.Zero;
			decimal dec_sgoods_refund = decimal.Zero;
			decimal dec_managefee = decimal.Zero;
			decimal dec_managefee_refund = decimal.Zero;

			int i_tomb = 0;
			int i_tomb_refund = 0;
			for (int i = 0; i < gridView1.RowCount; i++)
			{
				if(Convert.ToDecimal(gridView1.GetRowCellValue(i,"AC022")) >= 0)
				{
					dec_tomb += Convert.ToDecimal(gridView1.GetRowCellValue(i, "AC022"));
					i_tomb++;
				}
				else if(Convert.ToDecimal(gridView1.GetRowCellValue(i, "AC022")) < 0)
				{
					dec_tomb_refund += Convert.ToDecimal(gridView1.GetRowCellValue(i, "AC022"));
					i_tomb_refund++;
				}
			}
			te_tomb_bs.EditValue = i_tomb;
			te_tomb_je.EditValue = dec_tomb;
			te_tombrefund_bs.EditValue = i_tomb_refund;
			te_tombrefund_je.EditValue = dec_tomb_refund;

			for (int i = 0; i < gridView2.RowCount; i++)
			{
				if (Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007")) >= 0)
				{
					dec_sgoods += Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007"));
				}
				else if (Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007")) < 0)
				{
					dec_sgoods_refund += Convert.ToDecimal(gridView2.GetRowCellValue(i, "SA007"));
				}
			}
			te_sgoods.EditValue = dec_sgoods;
			te_sgoods_refund.EditValue = dec_sgoods_refund;

			for (int i = 0; i < gridView3.RowCount; i++)
			{
				if (Convert.ToDecimal(gridView3.GetRowCellValue(i, "SA007")) >= 0)
				{
					dec_managefee += Convert.ToDecimal(gridView3.GetRowCellValue(i, "SA007"));
				}
				else if (Convert.ToDecimal(gridView3.GetRowCellValue(i, "SA007")) < 0)
				{
					dec_managefee_refund += Convert.ToDecimal(gridView3.GetRowCellValue(i, "SA007"));
				}
			}
			te_managefee.EditValue = dec_managefee;
			te_managefee_refund.EditValue = dec_managefee_refund;

			te_bs.EditValue = BusinessAction.FinStat_bs(s_begin, s_end);
			te_je.EditValue = BusinessAction.FinStat_je(s_begin, s_end);

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

		private void gridView2_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
		{
			//if(e.Column.FieldName.ToUpper() == "SA004")
			//{
			//	e.DisplayText = BusinessAction.Mapper_item(e.Value.ToString());
			//}
		}

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
		/// 导出 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			//string s_json = string.Empty;
			//s_json = JsonConvert.SerializeObject(dt_managefee);
			//textEdit1.Text = s_json;
			Frm_FinStat_select frm_1 = new Frm_FinStat_select();
			if(frm_1.ShowDialog() == DialogResult.OK)
			{
				int i_index = Convert.ToInt32(frm_1.swapdata["index"]);
				GridControl grid = null;
				if (i_index == 0)
					grid = gridControl1;
				else if (i_index == 1)
					grid = gridControl2;
				else if (i_index == 2)
					grid = gridControl3;

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
			frm_1.Dispose();
		}
		/// <summary>
		/// 打印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if(gridView1.RowCount + gridView2.RowCount + gridView3.RowCount <= 0)
			{
				XtraMessageBox.Show("没有数据!","提示",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			//1.打印售墓
			if(gridView1.RowCount > 0)
			{
				using (FinStat_report1 report = new FinStat_report1())
				{
					var jsonDataSource = new JsonDataSource();

					jsonDataSource.JsonSource = new CustomJsonSource(JsonConvert.SerializeObject(dt_tomb));

					report.DataSource = jsonDataSource;
					report.Parameters[0].Value = op_begin.Value + "至" + op_end.Value;
					report.Parameters[1].Value = gridView1.RowCount;

					report.RequestParameters = false;    //禁止显示参数确认窗口

					report.Print();
				}
			}


			//2打印服务祭品
			if (gridView2.RowCount > 0)
			{
				using (FinStat_report2 report = new FinStat_report2())
				{
					var jsonDataSource = new JsonDataSource();

					jsonDataSource.JsonSource = new CustomJsonSource(JsonConvert.SerializeObject(dt_goodsService));

					report.DataSource = jsonDataSource;
					report.Parameters[0].Value = op_begin.Value + "至" + op_end.Value;

					report.RequestParameters = false;    //禁止显示参数确认窗口

					report.Print();
				}
			}


			//3管理费
			if (gridView3.RowCount > 0)
			{
				using (FinStat_report3 report = new FinStat_report3())
				{
					var jsonDataSource = new JsonDataSource();

					jsonDataSource.JsonSource = new CustomJsonSource(JsonConvert.SerializeObject(dt_managefee));

					report.DataSource = jsonDataSource;
					report.Parameters[0].Value = op_begin.Value + "至" + op_end.Value;
					report.Parameters[1].Value = gridView3.RowCount;

					report.RequestParameters = false;    //禁止显示参数确认窗口

					report.Print();
				}
			}
		}

		private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_json = string.Empty;
			s_json = JsonConvert.SerializeObject(dt_goodsService);
			textEdit1.Text = s_json;
		}

		private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			string s_json = string.Empty;
			s_json = JsonConvert.SerializeObject(dt_managefee);
			textEdit1.Text = s_json;
		}
	}
}
