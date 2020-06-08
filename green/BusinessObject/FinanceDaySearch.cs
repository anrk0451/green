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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Xpo;
using green.Action;
using green.Misc;
using green.xpo.orcl;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraPrinting;

namespace green.BusinessObject
{
    public partial class FinanceDaySearch : BaseBusiness
    {
        private DataTable dt_detail = new DataTable("DETAIL");
        private OracleDataAdapter deAdapter =
            new OracleDataAdapter("select * from v_findetail where sa010 = :sa010", SqlAssist.conn);
        private OracleParameter op_sa010 = null;



        public FinanceDaySearch()
        {
            InitializeComponent();
            op_sa010 = new OracleParameter("sa010", OracleDbType.Varchar2, 10);
            op_sa010.Direction = ParameterDirection.Input;
            deAdapter.SelectCommand.Parameters.AddRange(new OracleParameter[] { op_sa010 });
        }

        private void FinanceDaySearch_Load(object sender, EventArgs e)
        {
            gridControl2.DataSource = dt_detail;
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
        /// 代码值转换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "FA002")  //收费类型
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "购墓";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "服务祭品";
                else if (e.Value.ToString() == "2")
                    e.DisplayText = "管理费";
            }
            else if(e.Column.FieldName.ToUpper() == "FA190")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "未开票";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "已开票";
                else if (e.Value.ToString() == "2")
                    e.DisplayText = "部分开票";
                else if (e.Value.ToString() == "3")
                    e.DisplayText = "财政发票";
            }
        }
        /// <summary>
        /// 查询条件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.ShowCondition();
        }

        
        private void ShowCondition()
        {
            Frm_finDaySearch frm_1 = new Frm_finDaySearch();
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                this.Cursor = Cursors.WaitCursor;
                string s_fa002 = frm_1.swapdata["fa002"].ToString();
                string s_fa003 = frm_1.swapdata["fa003"].ToString();
                string s_begin = frm_1.swapdata["dbegin"].ToString();
                string s_end = frm_1.swapdata["dend"].ToString();
                string s_criteria = "FA002 like '" + s_fa002 + "' and FA003 like '" + s_fa003 + "'  and " +
                    "FA200>= #" + s_begin + "# and FA200< #" + s_end + "#";
                if (Convert.ToBoolean(toggle_onlyme.EditValue))
                {
                    s_criteria += " and FA100 ='" + Envior.cur_userId + "'";
                }

                CriteriaOperator criteria = CriteriaOperator.Parse(s_criteria);
                xpCollection1.Criteria = criteria;
                xpCollection1.LoadingEnabled = true;
                this.Cursor = Cursors.Arrow;
            }
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

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
        /// 发票作废
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            if (rowHandle >= 0)
            {
                string s_handler = gridView1.GetRowCellValue(rowHandle, "FA100").ToString();
                //权限检查
                //if (!AppAction.CheckRight("收费作废", s_handler)) return;

                string s_reason = string.Empty;
                string s_ac001 = gridView1.GetRowCellValue(rowHandle, "AC001").ToString();
                string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                string s_fa190 = gridView1.GetRowCellValue(rowHandle, "FA190").ToString();
                string s_fpinfo = string.Empty;

                if(s_fa190 == "3" /* 原财政发票 */)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "不能作废以前财政发票收费!");
                    return;
                }

                if (XtraMessageBox.Show("确认要作废吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;

                //检查与开票所在工作站是否一致!!!
                if (BusinessAction.CheckWorkStationCompare(s_fa001, Envior.WORKSTATIONID) == "0")
                {
                    XtraMessageBox.Show("此笔收费发票不是在当前工作站开具,不能继续!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Frm_RemoveFinReason frm_reason = new Frm_RemoveFinReason();
                if (frm_reason.ShowDialog() == DialogResult.OK)
                {
                    s_reason = frm_reason.swapdata["reason"].ToString();
                }
                frm_reason.Dispose();
 
                int re = BusinessAction.FinanceRemove(s_fa001, s_reason, Envior.cur_userId);
                ///作废成功,开始作废发票
                if (re > 0)
                {
                    this.RefreshData();
                    XtraMessageBox.Show("收费作废成功!如果本次收费已开具发票,点击【确定】开始作废已开具发票!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    CriteriaOperator criteria = CriteriaOperator.Parse("FA001='" + s_fa001 + "'");
                    XPCollection<FP01> xpCollection_fp01 = new XPCollection<FP01>(PersistentCriteriaEvaluationBehavior.BeforeTransaction, unitOfWork1, criteria);
                    foreach (FP01 fp01 in xpCollection_fp01)
                    {
                        s_fpinfo = "发票代码:" + fp01.INVOICECODE + "~t" + "发票号:" + fp01.INVOICENUM;
                        if (TaxInvoice.Remove(s_fa001, Envior.cur_userName) > 0) //发票作废成功
                        {
                            //修改发票作废日志
                            if(TaxInvoice.TaxRemove_log(s_fa001, Envior.cur_userName, s_reason) < 0)
                            {
                                XtraMessageBox.Show("记录发票日志错误,请与管理员联系!\r\n" + s_fpinfo, "提示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        else
                        {
                            XtraMessageBox.Show("未能作废税务发票,请在【税神通】中作废指定票据!\r\n" + s_fpinfo, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                     
                }

            }
        }
        /// <summary>
        /// 补开发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_fa001 = string.Empty;
            string s_fa003 = string.Empty;

            if(rowHandle >= 0)
            {
                if(gridView1.GetRowCellValue(rowHandle,"FA190").ToString() == "1")
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "该收费记录已经开具发票!");
                    return;
                }else if(gridView1.GetRowCellValue(rowHandle, "FA190").ToString() == "3" /*原财政发票*/)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "不能补开以前财政发票收费!");
                    return;
                }
 
                s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                //获取税务客户信息
                s_fa003 = gridView1.GetRowCellValue(rowHandle, "FA003").ToString();

                Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_fa003);
                if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                CriteriaOperator criteria = CriteriaOperator.Parse("FA001='" + s_fa001 + "' and FLAG='2'");
                XPCollection<FP01> xpCollection_fp01 = new XPCollection<FP01>(PersistentCriteriaEvaluationBehavior.BeforeTransaction, unitOfWork1, criteria);
                foreach (FP01 fp01 in xpCollection_fp01)
                {
                    if (TaxInvoice.GetNextInvoiceNo() > 0)
                    {
                        if (XtraMessageBox.Show("下一张税票代码:" + Envior.NEXT_BILL_CODE + "\r\n" + "票号:" + Envior.NEXT_BILL_NUM + ",是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            TaxInvoice.Invoice(fp01.FP001, clientInfo);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 打印税务发票
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            int papers = 0;
            string s_fa001 = string.Empty;
            if (rowHandle >= 0)
            {
                s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                papers = BusinessAction.GetHaveInvoicePapers(s_fa001);
                if(papers == 0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "此收费记录尚未开具发票!");
                    return;
                }
                else if(papers == 1)  //一张发票
                {
                    using (OracleDataReader reader = SqlAssist.ExecuteReader("select * from fp01 where fa001='" + s_fa001 + "' and flag = '1' and rownum < 2"))
                    {
                        reader.Read();
                        string s_fpdm = reader["INVOICECODE"].ToString();
                        string s_fphm = reader["INVOICENUM"].ToString();
                        if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            TaxInvoice.PrintInvoice(s_fpdm, s_fphm, "0");
                        }
                    }
                }
                else if(papers > 1 )   //多张发票
                {
                    Frm_InvoiceDisplay frm_1 = new Frm_InvoiceDisplay();
                    frm_1.swapdata["fa001"] = s_fa001;
                    frm_1.ShowDialog();
                    frm_1.Dispose();
                }
            }
        }
        /// <summary>
        /// 打印发票清单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            int papers = 0;
            string s_fa001 = string.Empty;
            if (rowHandle >= 0)
            {
                s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                papers = BusinessAction.GetHaveInvoicePapers(s_fa001);
                if (papers == 0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "此收费记录尚未开具发票!");
                    return;
                }
                else if (papers == 1)  //一张发票
                {
                    using (OracleDataReader reader = SqlAssist.ExecuteReader("select * from fp01 where fa001='" + s_fa001 + "' and flag = '1' and rownum < 2"))
                    {
                        reader.Read();
                        string s_fpdm = reader["INVOICECODE"].ToString();
                        string s_fphm = reader["INVOICENUM"].ToString();
                        if (XtraMessageBox.Show("发票代码:" + s_fpdm + "\r\n" + "发票号码:" + s_fphm + "\r\n是否继续?", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            TaxInvoice.PrintInvoice(s_fpdm, s_fphm, "1");
                        }
                    }
                }
                else if (papers > 1)   //多张发票
                {
                    Frm_InvoiceDisplay frm_1 = new Frm_InvoiceDisplay();
                    frm_1.swapdata["fa001"] = s_fa001;
                    frm_1.ShowDialog();
                    frm_1.Dispose();
                }
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

        /// <summary>
		/// 检索明细
		/// </summary>
		/// <param name="rowHandle"></param>
		private void RetrieveDetail(int rowHandle)
        {
            if (rowHandle >= 0)
            {
                string s_fa001 = gridView1.GetRowCellValue(rowHandle, "FA001").ToString();
                op_sa010.Value = s_fa001;
                gridView2.BeginUpdate();
                dt_detail.Rows.Clear();
                deAdapter.Fill(dt_detail);
                gridView2.EndUpdate();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (e.FocusedRowHandle >= 0)
            {
                this.RetrieveDetail(e.FocusedRowHandle);
            }
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            try
            {
                if (e.Button == MouseButtons.Left && e.Clicks == 1)
                {
                    //判断光标是否在行范围内  
                    if (hInfo.InRow && hInfo.Column != null && hInfo.Column.FieldName.ToUpper() == "INVNUM"  )
                    {
                        string s_fa001 = gridView1.GetRowCellValue(hInfo.RowHandle, "FA001").ToString();
                        if(BusinessAction.GetHaveInvoicePapers(s_fa001) > 1)  //如果已开发票数量大于1
                        {
                            Frm_InvoiceInfo frm_1 = new Frm_InvoiceInfo();
                            frm_1.swapdata["fa001"] = s_fa001;
                            frm_1.ShowDialog();
                            frm_1.Dispose();
                        }
                    }
                }
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LogUtils.Error(ee.ToString());
            }
        }

        private void toggle_onlyme_EditValueChanged(object sender, EventArgs e)
        {
            if (Convert.ToBoolean(toggle_onlyme.EditValue))
                xpCollection1.Filter = CriteriaOperator.Parse("FA100 ='" + Envior.cur_userId + "'");
            else
                xpCollection1.Filter = null;
        }
        /// <summary>
        /// 退费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }
    }
}
