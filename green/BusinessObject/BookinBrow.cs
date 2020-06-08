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
using DevExpress.Data.Filtering;
using green.Form;
using green.Action;
using System.Windows.Forms.VisualStyles;
using green.Misc;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;

namespace green.BusinessObject
{
    public partial class BookinBrow : BaseBusiness
    {
        public BookinBrow()
        {
            InitializeComponent();
        }
        private void BookinBrow_Load(object sender, EventArgs e)
        {
            CriteriaOperator criteria = CriteriaOperator.Parse("STATUS='1'");
            xpCollection1.Criteria = criteria;
            xpCollection1.LoadingEnabled = true;
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
        /// 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_bookinSearch frm_1 = new Frm_bookinSearch();
            if(frm_1.ShowDialog() == DialogResult.OK)
            {
                //BusinessAction.ForceRefreshBookin();
                this.Cursor = Cursors.WaitCursor;
                string s_bk003 = frm_1.swapdata["bk003"].ToString();
                string s_begin = frm_1.swapdata["begin"].ToString();
                string s_end = frm_1.swapdata["end"].ToString();
                string s_status = frm_1.swapdata["status"].ToString();
                string s_criteria = @"BK003 LIKE '" + s_bk003 + "' and " +
                            "BK200>= #" + s_begin + "# and BK200<= #" + s_end + "# and " +
                            "STATUS like '" + s_status + "'";
                CriteriaOperator criteria = CriteriaOperator.Parse(s_criteria);
                xpCollection1.Criteria = criteria;
                xpCollection1.LoadingEnabled = true;
                this.Cursor = Cursors.Arrow;
            }
            frm_1.Dispose();
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "STATUS")
            {
                if (e.Value.ToString() == "1")
                    e.DisplayText = "未到期";
                else if (e.Value.ToString() == "2")
                    e.DisplayText = "已登记";
                else if (e.Value.ToString() == "3")
                    e.DisplayText = "已过期";
            }
        }
        /// <summary>
        /// 取消预定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if(rowHandle >= 0)
            {
                if(gridView1.GetRowCellValue(rowHandle,"STATUS").ToString() != "1")
                {
                    Tools.msg(MessageBoxIcon.Warning, "提示", "已经到期或登记的记录不能取消!");
                    return;
                }
                if (XtraMessageBox.Show("确认要取消预定吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;

                string s_bk001 = gridView1.GetRowCellValue(rowHandle, "BK001").ToString();
                if(BusinessAction.BookinCanceled(s_bk001) > 0)
                {
                    Tools.msg(MessageBoxIcon.Information, "提示", "取消预定成功!");
                    this.RefreshData();
                }

            }
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void RefreshData()
        {
            this.Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            UnitOfWork unitOfWork = new UnitOfWork();
            if (xpCollection1.LoadingEnabled)
            {
                xpCollection1.Session = unitOfWork;
                xpCollection1.Reload();
            }
            gridView1.EndUpdate();
            this.Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// 购墓登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_bk001 = string.Empty;
            if (rowHandle >= 0)
            {
                if(gridView1.GetRowCellValue(rowHandle,"STATUS").ToString() != "1")
                {
                    Tools.msg(MessageBoxIcon.Warning, "提示", "只有未到期的预定记录才可以进行登记!");
                    return;
                }
                s_bk001 = gridView1.GetRowCellValue(rowHandle, "BK001").ToString();
                Frm_checkin frm_1 = new Frm_checkin();
                frm_1.swapdata["action"] = "bookin";
                frm_1.swapdata["bk001"] = s_bk001;
                if(frm_1.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshData();
                }
                frm_1.Dispose();
            }
        }
        /// <summary>
        /// 查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem29_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!gridView1.IsFindPanelVisible)
                gridView1.ShowFindPanel();
            else
                gridView1.HideFindPanel();
        }

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
