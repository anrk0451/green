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
using green.Action;
using green.Misc;

namespace green.Form
{
    public partial class Frm_rower : MyDialog
    {
        private string rg001 = string.Empty;
        private string rg009 = string.Empty;
        private TG_ds tg_ds = null;


        public Frm_rower()
        {
            InitializeComponent();
        }

        private void Frm_rower_Load(object sender, EventArgs e)
        {
            ///接收 墓区排编号
            if (this.swapdata.ContainsKey("rg001")) rg001 = this.swapdata["rg001"].ToString();
            if (this.swapdata.ContainsKey("rg009")) rg009 = this.swapdata["rg009"].ToString();
            ///接收 数据集
            if (this.swapdata.ContainsKey("dataset")) tg_ds = this.swapdata["dataset"] as TG_ds;

            ///设置墓型 下拉选择
            if (tg_ds != null)
            {
                gl_mx.Properties.DataSource = tg_ds.dt_mx;
                gl_mx.Properties.ValueMember = "ST001";
                gl_mx.Properties.DisplayMember = "ST003";
            }

            ///如果是编辑记录 
            if (!string.IsNullOrEmpty(rg001))
            {
                te_rg005.Enabled = false;
                te_rg006.Enabled = false;
                radioGroup1.Enabled = false;

                DataRow[] rows = tg_ds.dt_rg01.Select("rg001='" + rg001 + "'");
                if (rows.Length > 0)
                {
                    te_rg003.EditValue = rows[0]["RG003"];
                    gl_mx.EditValue = rows[0]["RG004"];
                    te_price.EditValue = rows[0]["PRICE"];
                    te_rg005.EditValue = rows[0]["RG005"];
                    te_rg006.EditValue = rows[0]["RG006"];
                    radioGroup1.EditValue = rows[0]["RG007"];
                }
            }
            else  //新增 排
            {
                DataRow[] rows = tg_ds.dt_rg01.Select("rg001='" + rg009 + "'");
                if (rows.Length > 0)
                {
                    gl_mx.EditValue = rows[0]["RG004"];
                    te_price.EditValue = rows[0]["PRICE"];
                }
                sb_del.Enabled = false;
            }

        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 保存前检查
        /// </summary>
        /// <returns></returns>
        private bool checkBeforeSave()
        {
            if (string.IsNullOrEmpty(te_rg003.Text))
            {
                te_rg003.ErrorText = "排名称必须输入!";
                te_rg003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                return false;
            }
            else
            {
                //检查唯一性
                string s_sql = string.Empty;
                if (string.IsNullOrEmpty(rg001))
                    s_sql = " rg002 = '2' and rg003 ='" + te_rg003.Text + "' and rg009 ='" + rg009 + "'";
                else
                    s_sql = " rg002 = '2' and rg003='" + te_rg003.Text + "' and rg001 <> '" + rg001 + "' and rg009='" + rg009 + "'";

                if (tg_ds.dt_rg01.Select(s_sql).Length > 0)
                {
                    te_rg003.ErrorText = "排已经存在!";
                    te_rg003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    return false;
                }
            }
            int i_temp,i_temp2;
            if(!int.TryParse(te_rg005.Text,out i_temp))
            {
                te_rg005.ErrorText = "请输入起始墓位号!";
                te_rg005.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                return false;
            }
            if (!int.TryParse(te_rg006.Text, out i_temp2))
            {
                te_rg006.ErrorText = "请输入终止墓位号!";
                te_rg006.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                return false;
            }

            if(i_temp > i_temp2)
            {
                Tools.msg(MessageBoxIcon.Warning,"提示","起始号位不能大于终止号位!");
                return false;
            }
             
            return true;
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            string s_re001 = rg001;
            if (!checkBeforeSave()) return;
            if (string.IsNullOrEmpty(rg001))
            {
                //1.新增
                s_re001 = MiscAction.GetEntityPK("RG01");
                DataRow newrow = tg_ds.dt_rg01.NewRow();
                newrow["RG001"] = s_re001;
                newrow["RG002"] = "2";                              //0-顶级节点 1-墓区 2-排
                newrow["RG003"] = te_rg003.Text;
                newrow["RG004"] = gl_mx.EditValue;
                newrow["PRICE"] = te_price.EditValue;
                newrow["RG005"] = te_rg005.EditValue;
                newrow["RG006"] = te_rg006.EditValue;
                newrow["RG009"] = rg009;
                newrow["RG007"] = radioGroup1.EditValue;
                tg_ds.dt_rg01.Rows.Add(newrow);
                //生成墓位号
                int i_begin = Convert.ToInt32(te_rg005.Text);
                int i_end = Convert.ToInt32(te_rg006.Text);
                int i_order = 0;
                for(int i = i_begin; i<=i_end;i++)
                {
                    i_order ++;
                    DataRow bitrow = tg_ds.dt_bi01.NewRow();
                    bitrow["BI001"] = MiscAction.GetEntityPK("BI01");

                    if (radioGroup1.EditValue.ToString() == "0")   //左起
                        bitrow["BI002"] = i_order;
                    else                                           //右起 
                        bitrow["BI002"] = i_order * (-1);

                    bitrow["BI003"] = i.ToString();
                    bitrow["BI005"] = gl_mx.EditValue;
                    bitrow["PRICE"] = te_price.EditValue;
                    bitrow["RG001"] = rg009;                     //墓区编号
                    bitrow["RE001"] = s_re001;                   //排编号
                    bitrow["STATUS"] = "1";
                    tg_ds.dt_bi01.Rows.Add(bitrow);
                }
            }
            else
            {
                //2.修改
                DataRow[] rows = tg_ds.dt_rg01.Select("rg001='" + rg001 + "'");
                if (rows.Length > 0)
                {
                    rows[0]["RG003"] = te_rg003.Text;
                    rows[0]["RG004"] = gl_mx.EditValue;
                    rows[0]["PRICE"] = te_price.EditValue;
                    rows[0]["RG005"] = te_rg005.EditValue;
                    rows[0]["RG006"] = te_rg006.EditValue;
                }
                else
                {
                    Tools.msg(MessageBoxIcon.Warning, "提示", "未找到记录!");
                    return;
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
        /// <summary>
        /// 删除排
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sb_del_Click(object sender, EventArgs e)
        {
            if (XtraMessageBox.Show("确认要删除当前排？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            //检查是否占用!!!
            DataRow[] dr_rows = tg_ds.dt_bi01.Select("RE001='" + rg001 + "' and status in ('2','3')");
            if (dr_rows.Length > 0)
            {
                Tools.msg(MessageBoxIcon.Warning,"提示","当前排已经占用,不能删除!");
                return;
            }
            dr_rows = tg_ds.dt_bi01.Select("RE001='" + rg001 + "'");
            foreach(DataRow dr in dr_rows)
            {
                tg_ds.dt_bi01.Rows.Remove(dr);
            }
            
            DataRow[] rows = tg_ds.dt_rg01.Select("rg001='" + rg001 + "'");
            if (rows.Length > 0)
            {
                tg_ds.dt_rg01.Rows.Remove(rows[0]);
            }
            Tools.msg(MessageBoxIcon.Information, "提示", "删除成功!");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}