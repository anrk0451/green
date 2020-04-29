﻿using green.Action;
using green.BaseObject;
using green.Misc;
using System;
using System.Windows.Forms;

namespace green.Form
{
    public partial class Frm_taxBaseInfo : MyDialog
    {
        public Frm_taxBaseInfo()
        {
            InitializeComponent();
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Frm_taxBaseInfo_Load(object sender, EventArgs e)
        {
            //设置税务基本信息
            te_addr_tele.Text = Envior.TAX_ADDR_TELE;           //地址、电话
            te_bank_account.Text = Envior.TAX_BANK_ACCOUNT;     //银行、账号
            te_id.Text = Envior.TAX_ID;                         //纳税识别号

            if (Envior.TAX_INVOICE_TYPE == "007")
                combo_type.Text = "普票";
            else if (Envior.TAX_INVOICE_TYPE == "004")
                combo_type.Text = "专票";
            else if (Envior.TAX_INVOICE_TYPE == "026")
                combo_type.Text = "电子票";

            te_publickey.Text = Envior.TAX_PUBLIC_KEY;          //公钥
            te_privatekey.Text = Envior.TAX_PRIVATE_KEY;        //私钥
            te_url.Text = Envior.TAX_SERVER_URL;                //服务URL
            te_cashier.Text = Envior.TAX_CASHIER;               //收款人
            te_checker.Text = Envior.TAX_CHECKER;               //复核人
            te_max.EditValue = Envior.TAX_MAX_FEE;              //单张最大面额
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_id = te_id.Text;                       //纳税识别号
            string s_addr_tele = te_addr_tele.Text;         //销方地址电话
            string s_bank_account = te_bank_account.Text;   //销方银行账号

            string s_invocie_type = string.Empty;
            if (combo_type.Text == "普票")
                s_invocie_type = "007";
            else if (combo_type.Text == "专票")
                s_invocie_type = "004";
            else if (combo_type.Text == "电子票")
                s_invocie_type = "026";

            string s_public_key = te_publickey.Text;        //公钥
            string s_private_key = te_privatekey.Text;      //私钥
            string s_url = te_url.Text;                     //服务处理URL
            string s_cashier = te_cashier.Text;             //收款人
            string s_checker = te_checker.Text;             //复核人
            decimal dec_max;

            if (!decimal.TryParse(te_max.Text, out dec_max)) dec_max = 0;
                

            if (string.IsNullOrEmpty(s_url))
            {
                te_url.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_url.ErrorText = "发票服务URL不能为空!";
                return;
            }
            else if (String.IsNullOrEmpty(s_id))
            {
                te_id.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_id.ErrorText = "纳税人识别号不能为空!";
                return;
            }
            else if (String.IsNullOrEmpty(s_invocie_type))
            {
                combo_type.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                combo_type.ErrorText = "发票类型必须选择!";
                return;
            }
            else if (string.IsNullOrEmpty(s_public_key))
            {
                te_publickey.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_publickey.ErrorText = "公钥不能为空!";
                return;
            }
            else if (string.IsNullOrEmpty(s_private_key))
            {
                te_privatekey.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_privatekey.ErrorText = "私钥不能为空!";
                return;
            }
            else if (string.IsNullOrEmpty(s_cashier))
            {
                te_cashier.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_cashier.ErrorText = "收款人不能为空!";
                return;
            }
            else if (string.IsNullOrEmpty(s_checker))
            {
                te_checker.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_checker.ErrorText = "复核人不能为空!";
                return;
            }


            if (MiscAction.SaveTaxInfo(s_url, s_id, "", s_addr_tele, s_bank_account, s_invocie_type, s_public_key, s_private_key, s_cashier, s_checker,dec_max) > 0)
            {
                Tools.msg(System.Windows.Forms.MessageBoxIcon.Information, "提示", "保存成功");               
                Envior.TAX_SERVER_URL = s_url;
                Envior.TAX_ID = s_id;
                Envior.TAX_ADDR_TELE = s_addr_tele;
                Envior.TAX_BANK_ACCOUNT = s_bank_account;
                Envior.TAX_INVOICE_TYPE = s_invocie_type;
                Envior.TAX_PUBLIC_KEY = s_public_key;
                Envior.TAX_PRIVATE_KEY = s_private_key;
                Envior.TAX_CASHIER = s_cashier;
                Envior.TAX_CHECKER = s_checker;
                Envior.TAX_MAX_FEE = dec_max;
                this.Close();
            }

        }
    }
}