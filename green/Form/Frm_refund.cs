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
using DevExpress.Data.Filtering;
using green.Misc;
using green.xpo.orcl;
using green.Action;
using DevExpress.Xpo;

namespace green.Form
{
    public partial class Frm_refund : MyDialog
    {
        private string s_fa001 = string.Empty;
        private FA01 fa01 = null;
        private REFUND refund = null;

        public Frm_refund()
        {
            InitializeComponent();
        }

        private void Frm_refund_Load(object sender, EventArgs e)
        {
            s_fa001 = this.swapdata["fa001"].ToString();
            string s_criteria = string.Empty;

            s_criteria = "SA010 = '" + s_fa001 + "'" ;
            CriteriaOperator criteria = CriteriaOperator.Parse(s_criteria);
            xpCollection1.Criteria = criteria;
            xpCollection1.LoadingEnabled = true;


            fa01 = unitOfWork1.GetObjectByKey<FA01>(s_fa001);
            if(fa01 == null)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "检索数据错误!");
                sb_ok.Enabled = false;
            }
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            if (e.Action == CollectionChangeAction.Add)
            {
                int row = gridView1.FocusedRowHandle;
                if (gridView1.GetRowCellValue(row, "SA002").ToString() == "0")
                {
                    XtraMessageBox.Show("如果退墓请按退墓办理!", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.UnselectRow(row);
                }
            }
            else if (e.Action == CollectionChangeAction.Refresh && gridView1.SelectedRowsCount > 0)
            {
                gridView1.BeginUpdate();
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (gridView1.GetRowCellValue(i, "SA002").ToString() == "0")
                    {
                        gridView1.UnselectRow(i);
                    }
                }
                gridView1.EndUpdate();
            }

            this.Calc_Hj();

        }
        /// <summary>
        /// 计算退费总金额
        /// </summary>
        private void Calc_Hj()
        {
            decimal dec_fee = decimal.Zero;
            foreach (int i in gridView1.GetSelectedRows())
            {
                dec_fee += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
            }
            te_total.EditValue = dec_fee;
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            decimal dec_total = decimal.Zero;
            string s_new_fa001 = string.Empty;
            SA01 sa01 = null;
            FA01 fa01_new = null;

            if (gridView1.GetSelectedRows().Length == 0)
            {
                Tools.msg(MessageBoxIcon.Exclamation, "提示", "请先选择要退费的项目!");
                return;
            }
            
            try
            {
                dec_total = Convert.ToDecimal(te_total.EditValue);
                s_new_fa001 = MiscAction.GetEntityPK("FA01");

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (!gridView1.IsRowSelected(i)) continue;
                    sa01 = new SA01(unitOfWork1);
                    sa01.SA001 = MiscAction.GetEntityPK("SA01");                                       //销售流水号
                    sa01.AC001 = fa01.AC001;                                                           //购墓流水号
                    sa01.SA002 = fa01.FA002 == '2' ? '2':'1';                                          //服务或商品类别 0-购墓费 1-商品或服务 2-管理费      
                    sa01.SA003 = gridView1.GetRowCellValue(i, "SA003").ToString();                     //项目名称
                    sa01.SA004 = gridView1.GetRowCellValue(i, "SA004").ToString();                     //项目编号
                    sa01.SA005 = fa01.FA002;                                                           //业务类型 0-购墓 1-临时销售
                    sa01.PRICE = Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE"));             //单价
                    sa01.NUMS = 0 - Convert.ToDecimal(gridView1.GetRowCellValue(i, "NUMS"));           //数量
                    sa01.SA007 = 0 - Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));         //金额
                    sa01.SA006 = sa01.SA007;                                                           //原始单价
                    sa01.SA008 = '1';                                                                  //结算状态 1-已结算 0-未结算
                    sa01.SA010 = s_new_fa001;                                                          //结算流水号
                    sa01.SA020 = 'T';                                                                  //发票类型
                    sa01.SA025 = BusinessAction.GetTaxRate(sa01.SA004);                                //税率
                    sa01.SA100 = Envior.cur_userId;                                                    //经办人
                    sa01.SA200 = Tools.GetServerDate();                                                //经办日期
                    sa01.STATUS = '1';                                                                 //状态 1-正常 0-删除
                    sa01.Save();
                }

                //2.保存退费日志
                refund = new REFUND(unitOfWork1);
                refund.RF001 = s_new_fa001;
                refund.RF003 = te_reason.Text;
                refund.RF004 = 0 - dec_total;
                refund.RF100 = Envior.cur_userId;
                refund.RF200 = Tools.GetServerDate();
                refund.RF300 = s_fa001;                              //原结算流水号
                refund.Save();

                ///3.保存缴费记录
                fa01_new = new FA01(unitOfWork1);
                fa01_new.FA001 = s_new_fa001;               //结算流水号
                fa01_new.FA002 = fa01.FA002;                //结算类型 0-购墓 1-服务祭品 2-管理费
                fa01_new.FA003 = fa01.FA003;                //缴款人
                fa01_new.FA004 = 0 - dec_total;             //金额
                fa01_new.FA190 = '0';                       //开票标志 0-未开票
                fa01_new.FA100 = Envior.cur_userId;         //经办人
                fa01_new.FA200 = Tools.GetServerDate();     //经办日期
                fa01_new.STATUS = "1";                      //状态
                fa01_new.AC001 = fa01.AC001;
                fa01_new.WS001 = Envior.WORKSTATIONID;      //工作站
                fa01_new.Save();

                unitOfWork1.CommitTransaction();

                int i_invoice_num = BusinessAction.GetInvoicePapers(s_new_fa001);
                if (i_invoice_num == 0)
                {
                    Tools.msg(MessageBoxIcon.Information, "提示", "退费办理成功!");
                }
                else
                {
                    if (XtraMessageBox.Show("退费办理成功!\r\n" + "本次业务共需要" + i_invoice_num.ToString() + "张发票,现在开具吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb_ok.Enabled = false;
                        //获取税务客户信息
                        string s_fa003 = fa01.FA003;
                        Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_fa003);
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
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee)
            {
                unitOfWork1.RollbackTransaction();
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());
            }

        }
    }
}