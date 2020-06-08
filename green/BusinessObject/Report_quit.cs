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
using green.Form;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using DevExpress.XtraPrinting;

namespace green.BusinessObject
{
    public partial class Report_quit : BaseBusiness
    {
        public Report_quit()
        {
            InitializeComponent();
        }

        private void Report_quit_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_report_quit frm_1 = new Frm_report_quit();
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                string s_criteria = " AC003 LIKE '" + frm_1.swapdata["ac003"].ToString() + "' and " +
                                    "QT200>= #" + frm_1.swapdata["dbegin"].ToString() + "# and QT200< #" + frm_1.swapdata["dend"].ToString() + "#";
                CriteriaOperator criteria = CriteriaOperator.Parse(s_criteria);
                xpCollection1.Criteria = criteria;
                xpCollection1.LoadingEnabled = true;
            }
            frm_1.Dispose();
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
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            Session session = new Session();
            if (xpCollection1.LoadingEnabled)
            {
                xpCollection1.Session = session;
                xpCollection1.Reload();
            }
            gridView1.EndUpdate();
            this.Cursor = Cursors.Arrow;
        }

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
