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
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using green.Action;

namespace green.Form
{
    public partial class Frm_region : MyDialog
    {
        private string rg001 = string.Empty;
        private TG_ds tg_ds = null;
        
        public Frm_region()
        {
            InitializeComponent();
        }

        private void Frm_region_Load(object sender, EventArgs e)
        {
            ///接收 墓区编号
            if (this.swapdata.ContainsKey("rg001")) rg001 = this.swapdata["rg001"].ToString();
            ///接收 数据集
            if (this.swapdata.ContainsKey("dataset")) tg_ds = this.swapdata["dataset"] as TG_ds;

            ///设置墓型 下拉选择
            if(tg_ds != null)
            {
                gl_mx.Properties.DataSource = tg_ds.dt_mx;
                gl_mx.Properties.ValueMember = "ST001";
                gl_mx.Properties.DisplayMember = "ST003";
            }

            ///如果是编辑记录 
            if (!string.IsNullOrEmpty(rg001))
            {
                using (OracleDataReader reader = SqlAssist.ExecuteReader("select * from rg01 where rg001 = '" + rg001 + "'"))
                {
                    if (reader.Read())
                    {
                        te_rg003.EditValue = reader["RG003"];
                        gl_mx.EditValue = reader["RG004"];
                        te_price.EditValue = reader["PRICE"];
                    }
                }
            }
 
        }

        private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool checkBeforeSave()
        {
            if (string.IsNullOrEmpty(te_rg003.Text))
            {
                te_rg003.ErrorText = "墓区名称必须输入!";
                te_rg003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                return false;
            }
            return true;
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (!checkBeforeSave()) return;
            if (string.IsNullOrEmpty(rg001))
            {
                //1.新增
                DataRow newrow = tg_ds.dt_rg01.NewRow();
                newrow["RG001"] = MiscAction.GetEntityPK("RG01");

            }
            else
            {
                //2.修改
            }
        }
    }
}