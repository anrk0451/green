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
using green.DataSet;
using DevExpress.XtraGrid.Views.Grid;
using green.Misc;
using green.Action;
using DevExpress.XtraGrid.Views.Base;

namespace green.BusinessObject
{
    public partial class SalesItemInfo : BaseBusiness
    {
        private int curIndex = 0;
        private Boolean LOCK = true;
        private GridView gv = null;
        private SalesItem_ds salesItem_ds = new SalesItem_ds();

        public SalesItemInfo()
        {
            InitializeComponent();
        }

        private void SalesItemInfo_Load(object sender, EventArgs e)
        {
			gridControl1.DataSource = salesItem_ds.Si01;
			gridControl2.DataSource = salesItem_ds.Gi01;

			 
			//设置初始选择
			imageListBoxControl1.SetSelected(0, true);
			curIndex = 0;

			//设置自动过滤(过滤掉删除行:此操作应该在数据集装入数据后)
			gridView2.ActiveFilter.Clear();
			gridView2.ActiveFilterString = "STATUS <> '0'";
			gridView1.ActiveFilter.Clear();
			gridView1.ActiveFilterString = "STATUS <> '0'";

			/// 设置排序列
			gridView2.Columns["SORTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
			gridView1.Columns["SORTID"].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
			gridView2.Focus();

			lookup_si099.DataSource = salesItem_ds.dt_invoiceItems;
			lookup_si099.DisplayMember = "TI003";
			lookup_si099.ValueMember = "TI001";


			lookup_gi099.DataSource = salesItem_ds.dt_invoiceItems;
			lookup_gi099.DisplayMember = "TI003";
			lookup_gi099.ValueMember = "TI001";

			salesItem_ds.iiAdapter.Fill(salesItem_ds.dt_invoiceItems);
		}
		/// <summary>
		/// 绘制行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
		private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
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
		/// 新增
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gv.AddNewRow();
			int rowno = gv.FocusedRowHandle;

			/////// 设置焦点 开始编辑 !!!
			if (curIndex == 0)
				gv.FocusedColumn = gv.Columns["SI003"];
			else
				gv.FocusedColumn = gv.Columns["GI003"];

			gv.ShowEditor();
		}

		private void imageListBoxControl1_SelectedIndexChanged(object sender, EventArgs e)
		{
			curIndex = imageListBoxControl1.SelectedIndex;
			 
			if (((salesItem_ds.Si01 as DataTable).GetChanges() != null || (salesItem_ds.Gi01 as DataTable).GetChanges() != null) && LOCK)
			{
				if (XtraMessageBox.Show("数据已经改变,是否保存?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				{
					if (Save())
					{
						curIndex = imageListBoxControl1.SelectedIndex;
						if (curIndex == 0)
						{
							gridControl1.Visible = true;
							gridControl2.Visible = false;

							gv = gridView1;
							salesItem_ds.Fill_Si01();
						}
						else
						{
							gridControl1.Visible = false;
							gridControl2.Visible = true;

							gv = gridView2;
							salesItem_ds.Fill_Gi01();
						}
					}
					else
					{
						LOCK = false;
						imageListBoxControl1.SetSelected(curIndex, true);
					}
				}
				else
				{
					curIndex = imageListBoxControl1.SelectedIndex;
					if (curIndex == 0)
					{
						gridControl1.Visible = true;
						gridControl2.Visible = false;
						gv = gridView1;
						salesItem_ds.Fill_Si01();
					}
					else
					{
						gridControl1.Visible = false;
						gridControl2.Visible = true;
						gv = gridView2;
						salesItem_ds.Fill_Gi01();
					}
				}
			}
			else if (LOCK)
			{
				curIndex = imageListBoxControl1.SelectedIndex;
				if (curIndex ==0)
				{
					gridControl1.Visible = true;
					gridControl2.Visible = false;
					gv = gridView1;
					salesItem_ds.Fill_Si01();
				}
				else
				{
					gridControl1.Visible = false;
					gridControl2.Visible = true;
					gv = gridView2;
					salesItem_ds.Fill_Gi01();
				}
			}
			else
			{
				LOCK = true;
			}			 
		}
		/// <summary>
		/// 保存过程
		/// </summary>
		/// <returns></returns>
		private Boolean Save()
		{
			gv.ClearColumnErrors();
			if (!gv.PostEditor()) return false;
			if (!gv.UpdateCurrentRow()) return false;

			///完整性检查 
			DataTable dt_source = curIndex == 0 ? salesItem_ds.Si01 : salesItem_ds.Gi01;
			foreach (DataRow dr in dt_source.Rows)
			{
				if (dr[1] == null || dr[1] is DBNull)
				{
					XtraMessageBox.Show("项目名称必须输入!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
					return false;
				}
			}

			///////////////////////////////////////////

			try
			{
				if (curIndex == 0)
					salesItem_ds.si01Adapter.Update(salesItem_ds.Si01);
				else
					salesItem_ds.gi01Adapter.Update(salesItem_ds.Gi01);

				Tools.msg(MessageBoxIcon.Information, "提示", "保存成功!");	 
				return true;
			}
			catch (Exception ee)
			{
				Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());			 
				return false;
			}
		}
		/// <summary>
		/// 新行初始化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			string newkey = string.Empty;
			newkey = MiscAction.GetEntityPK("GI01");
			gridView2.SetRowCellValue(e.RowHandle, "GI001", newkey);
			gridView2.SetRowCellValue(e.RowHandle, "STATUS", "1");
			gridView2.SetRowCellValue(e.RowHandle, "PRICE", 0.00);
			gridView2.SetRowCellValue(e.RowHandle, "SORTID", Convert.ToInt32(newkey));
		}
		/// <summary>
		/// 新行初始化
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView1_InitNewRow(object sender, InitNewRowEventArgs e)
		{
			string newkey = string.Empty;
			newkey = MiscAction.GetEntityPK("SI01");
			gridView1.SetRowCellValue(e.RowHandle, "SI001", newkey);
			gridView1.SetRowCellValue(e.RowHandle, "STATUS", "1");
			gridView1.SetRowCellValue(e.RowHandle, "PRICE", 0.00);
			gridView1.SetRowCellValue(e.RowHandle, "SORTID", Convert.ToInt32(newkey));
		}

		private void gridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName.Equals("GI003"))       //项目名称
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "项目名称不能为空!";
				}
				else
				{
					for (int i = 0; i < gridView2.RowCount - 1; i++)
					{
						if (i == (sender as ColumnView).FocusedRowHandle) continue;
						if (gridView2.GetRowCellValue(i, "GI003") == null) continue;

						//如果名字相同,则校验不通过!                        
						if (String.Equals(gridView2.GetRowCellValue(i, "GI003").ToString(), e.Value.ToString()))
						{
							e.Valid = false;
							e.ErrorText = "名称已经存在!";
							break;
						}
					}
				}
			}
			else if (colName.Equals("PRICE"))   //单价
			{
				if (Decimal.Parse(e.Value.ToString()) < 0)
				{
					e.Valid = false;
					e.ErrorText = "单价不能小于0!";
				}
			}
		}
		/// <summary>
		/// 行验证
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void gridView2_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (gridView2.GetRowCellValue(gridView2.FocusedRowHandle, "GI003") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView2.Columns["GI003"], "名称不能为空!");
			}
		}

		private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
		{
			if (gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "SI003") == null)
			{
				e.Valid = false;
				(sender as ColumnView).SetColumnError(gridView1.Columns["SI003"], "名称不能为空!");
			}
		}

		private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			if (gv.FocusedRowHandle >= 0)
			{
				if (XtraMessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
				{
					return;
				}
			}
			gv.SetFocusedRowCellValue("STATUS", "0");
			gv.UpdateCurrentRow();
		}
		/// <summary>
		/// 上移
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int row = gv.FocusedRowHandle;
			if (row <= 0) return;

			int prior_sortId = int.Parse(gv.GetRowCellValue(row - 1, "SORTID").ToString());
			int cur_sortId = int.Parse(gv.GetRowCellValue(row, "SORTID").ToString());

			gv.SetRowCellValue(row, "SORTID", prior_sortId);
			gv.SetRowCellValue(row - 1, "SORTID", cur_sortId);
		}
		/// <summary>
		/// 下移
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			int row = gv.FocusedRowHandle;
			if ((row >= gv.RowCount - 2) || row < 0) return;

			int next_sortId = int.Parse(gv.GetRowCellValue(row + 1, "SORTID").ToString());
			int cur_sortId = int.Parse(gv.GetRowCellValue(row, "SORTID").ToString());

			gv.SetRowCellValue(row, "SORTID", next_sortId);
			gv.SetRowCellValue(row + 1, "SORTID", cur_sortId);
		}
		/// <summary>
		/// 刷新数据
		/// </summary>
		private void RefreshData()
		{
			if (curIndex == 0)
			{
				gridView2.BeginUpdate();
				salesItem_ds.Si01.Rows.Clear();
				salesItem_ds.Fill_Si01();      
				gridView2.EndUpdate();
			}
			else
			{
				gridView1.BeginUpdate();
				salesItem_ds.Gi01.Rows.Clear();
				salesItem_ds.Fill_Gi01();
				gridView1.EndUpdate();
			}
		}

		private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			this.RefreshData();
		}

		private void gridView2_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "GI003")
			{
				if (e.Value != System.DBNull.Value)
				{
					gridView2.SetFocusedRowCellValue("GI088", Tools.GetPYString(e.Value.ToString().Trim()));
				}
				else
				{
					gridView2.SetFocusedRowCellValue("GI088", System.DBNull.Value);
				}
			}
		}

		private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			Save();
		}

		private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
		{
			gv.ShowFindPanel();
		}

		private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
		{
			if (e.Column.FieldName == "SI003")
			{
				if (e.Value != System.DBNull.Value && e.Value != null)
				{
					gridView1.SetFocusedRowCellValue("SI088", Tools.GetPYString(e.Value.ToString().Trim()));
				}
				else
				{
					gridView1.SetFocusedRowCellValue("SI088", System.DBNull.Value);
				}
			}
		}

		private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
		{
			string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
			if (colName.Equals("SI003"))       //服务名称
			{
				if (String.IsNullOrEmpty(e.Value.ToString()))
				{
					e.Valid = false;
					e.ErrorText = "服务项目名称不能为空!";
				}
				else
				{
					for (int i = 0; i < gridView1.RowCount - 1; i++)
					{
						if (i == (sender as ColumnView).FocusedRowHandle) continue;
						if (gridView1.GetRowCellValue(i, "SI003") == null) continue;

						//如果名字相同,则校验不通过!                        
						if (String.Equals(gridView1.GetRowCellValue(i, "SI003").ToString(), e.Value.ToString()))
						{
							e.Valid = false;
							e.ErrorText = "名称已经存在!";
							break;
						}
					}
				}
			}
			else if (colName.Equals("PRICE"))   //单价
			{
				if (Decimal.Parse(e.Value.ToString()) < 0)
				{
					e.Valid = false;
					e.ErrorText = "单价不能小于0!";
				}
			}
		}
	}
}
