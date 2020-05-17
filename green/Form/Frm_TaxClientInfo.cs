﻿using System;
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
using green.Misc;

namespace green.Form
{
    public partial class Frm_TaxClientInfo : MyDialog
    {
        TaxClientInfo taxClientInfo = new TaxClientInfo();

        public Frm_TaxClientInfo()
        {
            InitializeComponent();
        }

        public Frm_TaxClientInfo(string cuname)
        {
            InitializeComponent();
            txtedit_clientName.Text = cuname;
        }

        public Frm_TaxClientInfo(TU01 tu01)
        {
            InitializeComponent();
            txtedit_clientName.Text = tu01.TU003;
            txtedit_InfoClientTaxCode.Text = tu01.TU005;
            txtedit_infoclientaddressphone.Text = tu01.TU006;
            txtedit_infoclientbankaccount.Text = tu01.TU007;
        }

        private void Frm_TaxClientInfo_Load(object sender, EventArgs e)
        {
            txtedit_infocashier.Text = Envior.TAX_CASHIER;  //收款人
            txtedit_infochecker.Text = Envior.TAX_CHECKER;  //复核人
            txtedit_clientName.Focus();
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (txtedit_clientName.EditValue == null)
            {
                txtedit_clientName.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                txtedit_clientName.ErrorText = "【购方名称】必须输入!";
                return;
            }
            taxClientInfo.InfoClientName = txtedit_clientName.Text;                    //客户名称
            taxClientInfo.InfoClientTaxCode = txtedit_InfoClientTaxCode.Text;          //税号
            taxClientInfo.infoclientbankaccount = txtedit_infoclientbankaccount.Text;  //客户银行账户
            taxClientInfo.infoclientaddressphone = txtedit_infoclientaddressphone.Text;//客户地址及电话
            taxClientInfo.infocashier = txtedit_infocashier.Text;                      //收款人
            taxClientInfo.infochecker = txtedit_infochecker.Text;                      //复核人

            this.swapdata["taxclientinfo"] = taxClientInfo;
            DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}