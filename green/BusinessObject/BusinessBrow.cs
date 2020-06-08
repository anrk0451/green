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
using System.Text.RegularExpressions;
using green.Misc;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using green.xpo.orcl;
using DevExpress.Xpo;
using System.Web.UI.WebControls;
using System.Windows.Forms.VisualStyles;
using DevExpress.XtraPrinting;
using green.Action;

namespace green.BusinessObject
{
    public partial class BusinessBrow : BaseBusiness
    {
        public BusinessBrow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_TombSearch frm_1 = new Frm_TombSearch();
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string s_ac001 = frm_1.swapdata["ac001"].ToString();
                string s_ac003 = frm_1.swapdata["ac003"].ToString();
                string s_ac050 = frm_1.swapdata["ac050"].ToString();
                string s_rg001 = frm_1.swapdata["rg001"].ToString();
                string s_bi003 = frm_1.swapdata["bi003"].ToString();
                string s_criteria = string.Empty;
                if (s_ac001 != "%")
                {
                    s_criteria = "AC001='" + s_ac001 + "'";
                } else 
                {
                    s_criteria = @"AC001 like '" + s_ac001 + "' and " +
                                     "AC003 like '" + s_ac003 + "' and " +
                                     "AC050 like '" + s_ac050 + "' and " +
                                     "AC010 like '" + s_rg001 + "' and " +
                                     "BI003 like '" + s_bi003 + "'"; 
                }
                CriteriaOperator criteria = CriteriaOperator.Parse(s_criteria);
                xpCollection_ac01.Criteria = criteria;
                xpCollection_ac01.LoadingEnabled = true;
                this.Cursor = Cursors.Arrow;
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
        /// 转换登记类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "AC300")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "原始登记";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "正常登记";
            }
        }
        /// <summary>
        /// 快速查找
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void te_quicksearch_EditValueChanged(object sender, EventArgs e)
        {
            string s_text = te_quicksearch.EditValue.ToString();
            if (string.IsNullOrEmpty(s_text)) return;

            Regex rex = new Regex(@"^\d{11}$");
            Regex rex2 = new Regex(@"^\d{10}$");
            if (s_text.Substring(0,1) == "y" || rex.IsMatch(s_text))   //证书编号
            {
                xpCollection_ac01.Criteria = CriteriaOperator.Parse("AC050='" + s_text + "'");
                xpCollection_ac01.LoadingEnabled = true;
            }
            else if (rex2.IsMatch(s_text))                             //购墓编号 
            {
                xpCollection_ac01.Criteria = CriteriaOperator.Parse("AC001='" + s_text + "'");
                xpCollection_ac01.LoadingEnabled = true;
            }
            else if (Tools.IsHZ(s_text))
            {
                xpCollection_ac01.Criteria = CriteriaOperator.Parse("AC003 like '" + s_text + "%'");
                xpCollection_ac01.LoadingEnabled = true;
            } 
             
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(gridView1.FocusedRowHandle >= 0)
            {
                this.Edit(gridView1.FocusedRowHandle);
            }
        }

        private void Edit(int rowHandle)
        {
            string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
            Frm_checkinEdit frm_1 = new Frm_checkinEdit();
            frm_1.swapdata["ac001"] = s_ac001;

            V_AC01_REPORT ac01 = xpCollection_ac01[gridView1.GetDataSourceRowIndex(rowHandle)] as V_AC01_REPORT; 
            if (frm_1.ShowDialog() == DialogResult.OK)
            {      
                ac01.Session.Reload(ac01); 
            }
            frm_1.Dispose();
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow)
                {
                    Edit(gridView1.FocusedRowHandle);
                }
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }
        /// <summary>
        /// 缴管理费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            V_AC01_REPORT ac01 = null;  
            if (rowHandle >= 0)
            {
                ac01 = xpCollection_ac01[gridView1.GetDataSourceRowIndex(rowHandle)] as V_AC01_REPORT;
                Frm_PayManageFee frm_1 = new Frm_PayManageFee();
                frm_1.swapdata["ac01"] = ac01;
                if(frm_1.ShowDialog() == DialogResult.OK)
                {
                    ac01.Session.Reload(ac01);
                }
                frm_1.Dispose();
            }
        }
        /// <summary>
        /// 墓穴位置变更
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            V_AC01_REPORT ac01 = null;
            if (rowHandle >= 0)
            {
                ac01 = xpCollection_ac01[gridView1.GetDataSourceRowIndex(rowHandle)] as V_AC01_REPORT;
                Frm_tombTransfer frm_1 = new Frm_tombTransfer();
                frm_1.swapdata["ac01"] = ac01;
                if(frm_1.ShowDialog() == DialogResult.OK)
                {
                    ac01.Session.Reload(ac01);
                }
                frm_1.Dispose();
            }
        }
 
        /// <summary>
        /// 退墓
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            V_AC01_REPORT ac01 = null;
            if (rowHandle >= 0)
            {
                ac01 = xpCollection_ac01[gridView1.GetDataSourceRowIndex(rowHandle)] as V_AC01_REPORT;
                FA01 fa01 = unitOfWork1.GetObjectByKey<FA01>(ac01.AC048, true);
                if(fa01 != null)
                {
                    if(fa01.FA190 == '2' /*开具部分发票*/)
                    {
                        Tools.msg(MessageBoxIcon.Warning, "提示", "该购墓尚有未开具的发票!");
                        return;
                    }
                }

                Frm_tombRefund frm_1 = new Frm_tombRefund();
                frm_1.swapdata["ac01"] = ac01;
                if (frm_1.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshData();
                }
                frm_1.Dispose();
            }
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            V_AC01_REPORT ac01 = null;
            if (rowHandle >= 0)
            {
                ac01 = xpCollection_ac01[gridView1.GetDataSourceRowIndex(rowHandle)] as V_AC01_REPORT;                 

                Frm_tombQuit frm_1 = new Frm_tombQuit();
                frm_1.swapdata["ac01"] = ac01;
                if (frm_1.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshData();
                }
                frm_1.Dispose();
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        private void RefreshData()
        {
            this.Cursor = Cursors.WaitCursor;
            gridView1.BeginUpdate();
            UnitOfWork unitOfWork = new UnitOfWork();
            if (xpCollection_ac01.LoadingEnabled)
            {
                xpCollection_ac01.Session = unitOfWork;
                xpCollection_ac01.Reload();
            }
            gridView1.EndUpdate();
            this.Cursor = Cursors.Arrow;
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
        /// <summary>
        /// 打印购墓协议
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_ac001 = string.Empty;
            if(rowHandle >= 0)
            {
                if(XtraMessageBox.Show("现在打印【购墓协议】吗?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
                    PrintAction.PrintProtocol(s_ac001);
                }                
            }
        }
        /// <summary>
        /// 补打证书
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_ac001 = string.Empty;
            if (rowHandle >= 0)
            {
                if (XtraMessageBox.Show("现在打印【证书】吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
                    PrintAction.PrintCert(s_ac001);
                }                
            }
        }
        /// <summary>
        /// 补打缴费记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_ac001 = string.Empty;
            if(rowHandle >= 0)
            {
                s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
                Frm_payrecord frm_1 = new Frm_payrecord();
                frm_1.swapdata["ac001"] = s_ac001;
                frm_1.ShowDialog();
                frm_1.Dispose();
            }
            
        }
    }
}
