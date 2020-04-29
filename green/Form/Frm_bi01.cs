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
using green.DataSet;

namespace green.Form
{
    public partial class Frm_bi01 : MyDialog
    {
        private DataRow dr_bit = null;
        private TG_ds tg_ds = null;

        public Frm_bi01()
        {
            InitializeComponent();
        }
 
        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (radioGroup1.EditValue.ToString())
            {
                case "0":  //修改定价
                    te_price.Enabled = true;
                    te_bi003.Enabled = false;
                    gl_mx.Enabled = false; 
                    break;
                case "1":  //修改号位
                    te_bi003.Enabled = true;
                    te_price.Enabled = false;
                    gl_mx.Enabled = false;
                    break;
                case "2":  //冻结
                    te_price.Enabled = false;
                    te_bi003.Enabled = false;
                    gl_mx.Enabled = false;
                    break;
                case "3":  //墓型
                    te_price.Enabled = false;
                    te_bi003.Enabled = false;
                    gl_mx.Enabled = true;
                    break;
            }
        }

        private void Frm_bi01_Load(object sender, EventArgs e)
        {
            //接收数据
            tg_ds = this.swapdata["dataset"] as TG_ds;
            dr_bit = this.swapdata["bit_record"] as DataRow;
            te_position.Text = this.swapdata["position"].ToString();
 
            if (tg_ds != null)
            {
                gl_mx.Properties.DataSource = tg_ds.dt_mx;
                gl_mx.Properties.ValueMember = "ST001";
                gl_mx.Properties.DisplayMember = "ST003";
            }

            te_price.EditValue = dr_bit["PRICE"];
            te_bi003.EditValue = dr_bit["BI003"];
            gl_mx.EditValue = dr_bit["BI005"];

            switch (dr_bit["STATUS"].ToString())
            {
                case "2":   //占用
                    radioGroup1.Properties.Items[3].Enabled = false;
                    break;
                case "3":   //预定
                    radioGroup1.Properties.Items[3].Enabled = false;
                    break;
                case "1":   //空闲
                    radioGroup1.Properties.Items[3].Description = "冻结";
                    break;
                case "4":   //冻结
                    radioGroup1.Properties.Items[3].Description = "解冻";
                    break;
            }


        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            switch (radioGroup1.EditValue.ToString())
            {
                case "0":  //修改价格
                    decimal dec_price;
                    if(!decimal.TryParse(te_price.Text,out dec_price) || dec_price < 0 )
                    {
                        te_price.ErrorText = "请输入正确的定价!";
                        te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                        return;
                    }
                    else
                    {
                        dr_bit["PRICE"] = dec_price;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;
                case "1":  //修改号位   
                    if (string.IsNullOrEmpty(te_bi003.Text))
                    {
                        te_bi003.ErrorText = "请输入号位!";
                        te_bi003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                        return;
                    }
                    else
                    {
                        DataRow[] rows = tg_ds.dt_bi01.Select("RE001='" + dr_bit["RE001"].ToString() + "' and BI001 <> '" + dr_bit["BI001"].ToString() + "' and BI003='" + te_bi003.Text + "'" );
                        if (rows.Length > 0)
                        {
                            te_bi003.ErrorText = "本排号位已经存在!";
                            te_bi003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                            return;
                        }
                        else
                        {
                            dr_bit["BI003"] = te_bi003.Text;
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                    }
                    break;
                case "2":  //冻结|解冻
                    if(dr_bit["STATUS"].ToString() == "1" /*未使用*/)
                    {
                        dr_bit["STATUS"] = "4";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }else if(dr_bit["STATUS"].ToString() == "4" /*冻结*/)
                    {
                        dr_bit["STATUS"] = "1";
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    break;
                case "3":  //墓型
                    dr_bit["BI005"] = gl_mx.EditValue;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                    break;
            }
        }
    }
}