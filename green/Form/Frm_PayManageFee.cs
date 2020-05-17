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
using DevExpress.Data.Filtering;
using DevExpress.Xpo;

namespace green.Form
{
    public partial class Frm_PayManageFee : MyDialog
    {
        V_AC01_REPORT ac01 = null;
        public Frm_PayManageFee()
        {
            InitializeComponent();
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_PayManageFee_Load(object sender, EventArgs e)
        {
            ac01 = this.swapdata["ac01"] as V_AC01_REPORT;
            if(ac01 != null)
            {
                te_ac001.EditValue = ac01.AC001;
                te_position.EditValue = ac01.POSITION;
                te_expire.EditValue = ac01.AC040;       //管理费到期日期
                te_price.EditValue = MiscAction.GetSysParaValue1("MANAGEFEE_PRICE");
                te_nums.EditValue = MiscAction.GetSysParaValue1("MANAGEFEE_NUMS");
            }
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            decimal dec_price = decimal.Zero;
            decimal dec_nums = decimal.Zero;
            string s_fa001 = string.Empty;

            if (string.IsNullOrEmpty(te_price.Text))
            {
                te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_price.ErrorText = "管理费单价必须输入!";
                return;
            }
            else if (string.IsNullOrEmpty(te_nums.Text))
            {
                te_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_nums.ErrorText = "管理费缴费年限必须输入!";
                return;
            }

            dec_price = Convert.ToDecimal(te_price.Text);
            dec_nums = Convert.ToDecimal(te_nums.Text);
            if(dec_price < 0)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "管理费单价必须大于0!");
                te_price.Focus();
                return;
            }
            if(dec_nums <= 0)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "缴费年限必须大于0!");
                te_nums.Focus();
                return;
            }
            s_fa001 = MiscAction.GetEntityPK("FA01");
            try
            {
                if (BusinessAction.ManageFee(s_fa001, ac01.AC001, dec_price, dec_nums, Envior.cur_userId, Envior.WORKSTATIONID) > 0)
                {
                    if (XtraMessageBox.Show("缴费成功!\r\n" + "是否现在开具发票?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        sb_ok.Enabled = false;
                        //获取税务客户信息
                        Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(ac01.AC003);
                        if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                        TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                        CriteriaOperator criteria = CriteriaOperator.Parse("FA001='" + s_fa001 + "'");
                        XPCollection<FP01> xpCollection_fp01 = new XPCollection<FP01>(PersistentCriteriaEvaluationBehavior.BeforeTransaction,session1 , criteria);
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
                    //打印缴费记录
                    Tools.msg(MessageBoxIcon.Information,"提示","现在打印缴费记录!");
                    PrintAction.PrintPayRecord(s_fa001);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ee)
            {
                Tools.msg(MessageBoxIcon.Error, "错误", "缴费错误!\r\n" + ee.ToString()) ;
            }


        }
    }
}