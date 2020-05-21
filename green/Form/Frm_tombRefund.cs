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
using DevExpress.Data.Filtering;
using green.Action;
using green.Misc;
using DevExpress.Xpo;

namespace green.Form
{
    public partial class Frm_tombRefund : MyDialog
    {
        private V_AC01_REPORT ac01 = null;
        private decimal dec_sales = decimal.Zero;
        private decimal dec_tomb = decimal.Zero;    //退墓费
             
        public Frm_tombRefund()
        {
            InitializeComponent();
        }

        private void Frm_tombRefund_Load(object sender, EventArgs e)
        {
            ac01 = this.swapdata["ac01"] as V_AC01_REPORT;
            if(ac01 != null)
            {
                te_ac001.EditValue = ac01.AC001;
                te_ac003.EditValue = ac01.AC003;
                te_position.EditValue = ac01.POSITION;
                te_price.EditValue = ac01.AC022;        //售价
                te_refund.EditValue = ac01.AC022;
                lc_sum.Text = string.Format("{0:c}", ac01.AC022);
            }
            else
            {
                XtraMessageBox.Show("检索数据失败!","提示",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                sb_ok.Enabled = false;
            }

            xpCollection1.Criteria = CriteriaOperator.Parse("SA010='" + ac01.AC048 + "' and SA002 <> '0'");
            xpCollection1.LoadingEnabled = true;

            dec_tomb = ac01.AC022;
            lc_sum.Text = string.Format("{0:c}", dec_sales + dec_tomb);
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            dec_sales = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.IsRowSelected(i)) dec_sales += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
            }          
            lc_sum.Text = string.Format("{0:c}", dec_sales + dec_tomb);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_fa001 = string.Empty;
            FA01 fa01 = null;
            SA01 sa01 = null;
            BI01 bi01 = null;
            AC01 ac01_m = null;
            REFUND refund = null;
            int i_invoice_num;

            if (XtraMessageBox.Show("确认要进行退墓操作,本操作将不可撤销,是否继续?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            if(decimal.TryParse(te_refund.Text,out dec_tomb))
            {
                if(dec_tomb<=0 || dec_tomb > ac01.AC022)
                {
                    te_refund.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_refund.ErrorText = "退费金额错误!";
                    return;
                }
            }
            else
            {
                te_refund.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_refund.ErrorText = "请输入退购墓费!";
                return;
            }
            try 
            {
                s_fa001 = MiscAction.GetEntityPK("FA01");               

                //2.保存销售记录
                sa01 = new SA01(unitOfWork1);
                sa01.SA001 = MiscAction.GetEntityPK("SA01");   //销售流水号
                sa01.AC001 = ac01.AC001;                       //购墓流水号
                sa01.SA002 = '0';                              //服务或商品类别 0-购墓费 1-商品或服务 2-管理费      
                sa01.SA003 = "购墓费";
                sa01.SA004 = ac01.AC015;                       //墓位编号
                sa01.SA005 = '0';                              //业务类型 0-购墓 1-临时销售
                sa01.PRICE = dec_tomb;                         //单价
                sa01.NUMS = -1;                                //数量
                sa01.SA007 = 0- dec_tomb;                      //金额
                sa01.SA006 = sa01.SA007;                       //原始单价
                sa01.SA008 = '1';                              //结算状态 1-已结算 0-未结算
                sa01.SA010 = s_fa001;                          //结算流水号
                sa01.SA020 = 'T';                              //发票类型
                sa01.SA025 = MiscAction.GetSysParaValue1("tomb_taxrate");  //税率
                sa01.SA100 = Envior.cur_userId;                //经办人
                sa01.SA200 = Tools.GetServerDate();            //经办日期
                sa01.STATUS = '1';                             //状态 1-正常 0-删除
                sa01.Save();

                for(int i= 0; i < gridView1.RowCount; i++)
                {
                    if (!gridView1.IsRowSelected(i)) continue;                  
                    sa01 = new SA01(unitOfWork1);
                    sa01.SA001 = MiscAction.GetEntityPK("SA01");                                       //销售流水号
                    sa01.AC001 = ac01.AC001;                                                           //购墓流水号
                    sa01.SA002 = '1';                                                                  //服务或商品类别 0-购墓费 1-商品或服务 2-管理费      
                    sa01.SA003 = gridView1.GetRowCellValue(i, "SA003").ToString();                     //项目名称
                    sa01.SA004 = gridView1.GetRowCellValue(i, "SA004").ToString();                     //项目编号
                    sa01.SA005 = '0';                                                                  //业务类型 0-购墓 1-临时销售
                    sa01.PRICE =  Convert.ToDecimal(gridView1.GetRowCellValue(i, "PRICE"));            //单价
                    sa01.NUMS = 0 - Convert.ToDecimal(gridView1.GetRowCellValue(i, "NUMS"));           //数量
                    sa01.SA007 = 0 - Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));         //金额
                    sa01.SA006 = sa01.SA007;                                                           //原始单价
                    sa01.SA008 = '1';                                                                  //结算状态 1-已结算 0-未结算
                    sa01.SA010 = s_fa001;                                                              //结算流水号
                    sa01.SA020 = 'T';                                                                  //发票类型
                    sa01.SA025 = BusinessAction.GetTaxRate(sa01.SA004);                                //税率
                    sa01.SA100 = Envior.cur_userId;                                                    //经办人
                    sa01.SA200 = Tools.GetServerDate();                                                //经办日期
                    sa01.STATUS = '1';                                                                 //状态 1-正常 0-删除
                    sa01.Save();
                }

                //3.保存退费日志
                refund = new REFUND(unitOfWork1);
                refund.RF001 = s_fa001;
                refund.RF003 = te_reason.Text;
                refund.RF004 = 0 - (dec_tomb + dec_sales);
                refund.RF100 = Envior.cur_userId;
                refund.RF200 = Tools.GetServerDate();
                refund.RF300 = ac01.AC048;                               //原结算流水号
                refund.Save();

                ///4.保存缴费记录
                fa01 = new FA01(unitOfWork1);
                fa01.FA001 = s_fa001;                   //结算流水号
                fa01.FA002 = '0';                       //结算类型 0-购墓 1-服务祭品 2-管理费
                fa01.FA003 = ac01.AC003;                //购墓人
                fa01.FA004 = 0 - (dec_tomb + dec_sales);//金额
                fa01.FA100 = Envior.cur_userId;         //经办人
                fa01.FA200 = Tools.GetServerDate();     //经办日期
                fa01.STATUS = "1";                      //状态
                fa01.AC001 = ac01.AC001;
                fa01.WS001 = Envior.WORKSTATIONID;      //工作站
                fa01.Save();

                //5.修改ac01
                ac01_m = unitOfWork1.GetObjectByKey<AC01>(ac01.AC001, true);
                ac01_m.STATUS = '2';                                     //状态-退墓
                ac01_m.Save();

                //6.修改号位状态
                bi01 = unitOfWork1.GetObjectByKey<BI01>(ac01.AC015, true);
                if(bi01 != null)
                {
                    bi01.AC001 = "";
                    bi01.STATUS = '1';
                    bi01.Save();
                }
                unitOfWork1.CommitChanges();
                i_invoice_num = BusinessAction.GetInvoicePapers(s_fa001);
                if(i_invoice_num ==0)
                {
                    Tools.msg(MessageBoxIcon.Information, "提示", "退墓成功!");                    
                }
                else
                {
                    if (XtraMessageBox.Show("退墓办理成功!\r\n" + "本次业务共需要" + i_invoice_num.ToString() + "张发票,现在开具吗?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb_ok.Enabled = false;
                        //获取税务客户信息
                        string s_ac003 = te_ac003.Text;
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
                }
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ee )
            {                
                unitOfWork1.RollbackTransaction();
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());
            }

        }

        private void te_refund_EditValueChanged(object sender, EventArgs e)
        {
            if(decimal.TryParse(te_refund.Text,out dec_tomb))
            {
                if(dec_tomb <=0 || dec_tomb > ac01.AC022)
                {
                    te_refund.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_refund.ErrorText = "请输入正确的退墓金额!";
                    return;
                }
                lc_sum.Text = string.Format("{0:c}", dec_sales + dec_tomb);
            }
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}