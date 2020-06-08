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
using green.xpo.orcl;
using green.Action;
using green.Misc;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace green.Form
{
    public partial class Frm_TempSales : MyDialog
    {
        private string s_fa001 = string.Empty;
        private decimal dec_sum = decimal.Zero;

        public Frm_TempSales()
        {
            InitializeComponent();
        }

        private void Frm_TempSales_Load(object sender, EventArgs e)
        {
            te_cuname.Focus();
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
        /// 变更事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "SA004" && (e.Value != DBNull.Value))   //选择服务或商品
            {
                V_ALL_VALIDITEM v_allItem = unitOfWork1.GetObjectByKey<V_ALL_VALIDITEM>(e.Value);
                if (v_allItem != null)
                {
                    gridView1.SetRowCellValue(e.RowHandle, "SA003", v_allItem.ITEM_NAME);  //项目名称
                    gridView1.SetRowCellValue(e.RowHandle, "PRICE", v_allItem.PRICE);      //单价
                    gridView1.SetRowCellValue(e.RowHandle, "SA006", v_allItem.PRICE);      //原始单价
                    gridView1.SetRowCellValue(e.RowHandle, "SA025", v_allItem.TAXRATE);    //税率          
                    //计算小计金额
                    decimal dec_price = decimal.Zero;
                    int i_nums = 0;
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                    {
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                    }
                }

            }
            else if (e.Column.FieldName.ToUpper() == "PRICE")                            //修改单价
            {
                decimal dec_price = decimal.Zero;
                int i_nums;
                if (e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }
                else
                {
                    gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }

            }
            else if (e.Column.FieldName.ToUpper() == "NUMS")
            {
                decimal dec_price = decimal.Zero;
                int i_nums;
                if (e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }
                else
                    gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);

            }
        }
        /// <summary>
        /// 新行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_sa001 = MiscAction.GetEntityPK("SA01");
            gridView1.SetRowCellValue(rowHandle, "SA001", s_sa001);           //销售流水号
            gridView1.SetRowCellValue(rowHandle, "SA002", '1');               //销售项目类型 1-商品或服务
            gridView1.SetRowCellValue(rowHandle, "STATUS", "1");        
            gridView1.SetRowCellValue(rowHandle, "SA005", "1");               //销售类型 1-临时性销售
            gridView1.SetRowCellValue(rowHandle, "SA008", "0");               //结算标志 0-未结算
            gridView1.SetRowCellValue(rowHandle, "SA100", Envior.cur_userId); //经办人
            gridView1.SetRowCellValue(rowHandle, "SA200", Tools.GetServerDate()); //经办日期
            gridView1.SetRowCellValue(rowHandle, "NUMS", 1);                  //数量
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            if (gridView1.FocusedColumn.FieldName.ToUpper() == "SA004")
            {
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    if (i == (sender as ColumnView).FocusedRowHandle) continue;
                    if (gridView1.GetRowCellValue(i, "SA004") == null) continue;

                    //如果项目相同,则校验不通过!                        
                    if (String.Equals(gridView1.GetRowCellValue(i, "SA004").ToString(), e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = "项目已经存在!";
                        break;
                    }
                }
            }
            else if (gridView1.FocusedColumn.FieldName.ToUpper() == "NUMS")
            {
                int i_num;
                if (int.TryParse(e.Value.ToString(), out i_num))
                {
                    if (i_num <= 0)
                    {
                        e.ErrorText = "请输入数量!";
                        e.Valid = false;
                        return;
                    }
                }
            }
            else if (gridView1.FocusedColumn.FieldName.ToUpper() == "PRICE")
            {
                decimal dec_1 = decimal.Zero;
                if (decimal.TryParse(e.Value.ToString(), out dec_1))
                {
                    if (dec_1 < 0)
                    {
                        e.ErrorText = "请输入正确的价格!";
                        e.Valid = false;
                        return;
                    }
                }
            }
        }

        private void groupControl3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            string s_button = e.Button.Properties.ToolTip.ToString();
            if (s_button == "增加")
            {
                gridView1.AddNewRow();
                int rowno = gridView1.FocusedRowHandle;
                /////// 设置焦点 开始编辑 !!!
                gridView1.FocusedColumn = gridView1.Columns["SA004"];
                gridView1.ShowEditor();
            }
            else if (s_button == "删除")
            {
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (!checkBeforeSave()) return;
            try
            {
                s_fa001 = MiscAction.GetEntityPK("FA01");
 
                //保存销售记录
                foreach (SA01 sa01 in xpCollection_sa01)
                {
                    sa01.SA008 = '1';                   //结算状态 
                    sa01.SA010 = s_fa001;               //结算流水号
                    sa01.SA020 = 'T';                   //发票类型
                    sa01.SA100 = Envior.cur_userId;     //经办人
                    sa01.SA200 = Tools.GetServerDate(); //经办日期
                    dec_sum += sa01.SA007;
                }
                //财务收费信息
                FA01 fa01 = new FA01(unitOfWork1);
                fa01.FA001 = s_fa001;                               //缴费流水号
                fa01.FA002 = '0';                                   //收费类型 0-购墓
                fa01.FA003 = te_cuname.Text;                        //缴费人
                fa01.FA004 = dec_sum;                               //收费金额
                fa01.FA190 = '0';                                   //开票标志 0-未开票
                fa01.FA100 = Envior.cur_userId;                     //收费人
                fa01.FA180 = "";                                    //备注
                fa01.FA200 = Tools.GetServerDate();                 //缴费时间
                fa01.STATUS = "1";                                  //状态
                fa01.WS001 = Envior.WORKSTATIONID;                  //工作站标识
                fa01.Save();
 
                unitOfWork1.CommitTransaction();

                //////////保存完成 //////////
                int i_papers = BusinessAction.GetInvoicePapers(s_fa001);
                if (XtraMessageBox.Show("办理成功!\r\n" + "本次结算共需要" + i_papers.ToString() + "张发票,现在开具吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sb_ok.Enabled = false;
                    //获取税务客户信息
                    string s_ac003 = te_cuname.Text;
                    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_ac003);
                    if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                    TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                    CriteriaOperator criteria = CriteriaOperator.Parse("FA001='" + s_fa001 + "'");
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

                this.Close();
            }
            catch (Exception ee) 
            {
                unitOfWork1.RollbackTransaction();
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());
            }
        }

        /// <summary>
        /// 保存前检查
        /// </summary>
        /// <returns></returns>
        private bool checkBeforeSave()
        {
            if (string.IsNullOrEmpty(te_cuname.Text))
            {
                te_cuname.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_cuname.ErrorText = "请输入交款人姓名!";
                te_cuname.Focus();
                return false;
            }
            else if(xpCollection_sa01.Count == 0)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "请先选择项目!");
                return false;
            }
 
            if (!gridView1.PostEditor()) return false;
            if (!gridView1.UpdateCurrentRow()) return false;         

            //检查服务祭品是否输入完整
            foreach (SA01 s in xpCollection_sa01)
            {
                if (string.IsNullOrEmpty(s.SA004))
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入项目名称!");
                    return false;
                }
                else if (s.PRICE <= 0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入价格!");
                    return false;
                }
                else if (s.NUMS <= 0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入数量!");
                    return false;
                }
            }

            return true;
        }
    }
}