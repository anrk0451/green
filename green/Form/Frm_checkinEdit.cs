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
using green.Misc;
using green.DataSet;
using DevExpress.Data.Filtering;
using green.Action;

namespace green.Form
{
    public partial class Frm_checkinEdit : MyDialog
    {
        private DataDict_ds dd_ds = new DataDict_ds();  //字典数据源
        private string s_ac001 = string.Empty;
        private string s_ac199 = string.Empty;

        private AC01 ac01 = null;
        private BI01 bi01 = null;

        public Frm_checkinEdit()
        {
            InitializeComponent();
            le_mx.Properties.DataSource = dd_ds.dv_mx;
            rep_item_gx.DataSource = dd_ds.dv_gx;
            rep_item_zs.DataSource = dd_ds.dv_zs;
        }

        private void Frm_checkinEdit_Load(object sender, EventArgs e)
        {
            s_ac001 = this.swapdata["ac001"].ToString();
            ac01 = unitOfWork1.GetObjectByKey<AC01>(s_ac001, true);
            if(ac01 == null)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "未找到数据!");
                sb_ok.Enabled = false;                
                return;
            }
            bi01 = unitOfWork1.GetObjectByKey<BI01>(ac01.AC015, true);
            if(bi01 == null)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "未找到数据!");
                sb_ok.Enabled = false;
                return;
            }

            te_ac003.EditValue = ac01.AC003;
            be_position.EditValue = MiscAction.GetTombPosition(ac01.AC015);
            le_mx.EditValue = bi01.BI005;           //墓型
            te_fixprice.EditValue = ac01.AC020;     //定价
            te_ac004.EditValue = ac01.AC004;        //联系电话
            te_ac002.EditValue = ac01.AC002;        //身份证号
            te_price.EditValue = ac01.AC022;        //售价
            te_ac250.EditValue = ac01.AC250;        //备注

            //检索 ac03
            xpCollection_ac03.Criteria = CriteriaOperator.Parse("AC001='" + s_ac001 + "'");
            xpCollection_ac03.LoadingEnabled = true;
            //检索缴费记录
            xpCollection_pr01.Criteria = CriteriaOperator.Parse("AC001='" + s_ac001 + "'");
            xpCollection_pr01.LoadingEnabled = true;

            //安葬批次号
            s_ac199 = MiscAction.GetEntityPK("AC01");

            if (BusinessAction.IsDebt(s_ac001)== 1)
                lc_debt.Text = "欠费";
            else
                lc_debt.Text = "不欠费";
            
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if(e.Column.FieldName.ToUpper() == "PR004")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "免管理费";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "正常缴纳";
            }
        }

        private void groupControl4_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            string s_button = e.Button.Properties.ToolTip.ToString();
            if (s_button == "增加")
            {
                gridView2.AddNewRow();
                int rowno = gridView2.FocusedRowHandle;
                /////// 设置焦点 开始编辑 !!!
                gridView2.FocusedColumn = gridView1.Columns["AC113"];
                gridView2.ShowEditor();
            }
            else if (s_button == "删除")
            {
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
            }
        }

        private void gridView2_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "AC116")  //出生日期
            {
                if (e.Value != null && !(e.Value is DBNull))
                {
                    ChineseDateTime cdt = new ChineseDateTime(Convert.ToDateTime(e.Value));
                    gridView2.SetRowCellValue(e.RowHandle, "AC117", cdt.ToChineseString());
                }
            }
        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
        /// 新行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            string s_ac111 = MiscAction.GetEntityPK("AC03");
            gridView2.SetRowCellValue(e.RowHandle, "AC001", s_ac001);
            gridView2.SetRowCellValue(e.RowHandle, "AC112", "0");
            gridView2.SetRowCellValue(e.RowHandle, "AC111", s_ac111);
            gridView2.SetRowCellValue(e.RowHandle, "STATUS", '1');
            gridView2.SetRowCellValue(e.RowHandle, "AC118", DateTime.Today);
            gridView2.SetRowCellValue(e.RowHandle, "AC119", DateTime.Today);
            gridView2.SetRowCellValue(e.RowHandle, "AC199", s_ac199);
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            if (!gridView2.UpdateCurrentRow()) return;
            if (string.IsNullOrEmpty(te_ac004.Text))
            {
                te_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac004.ErrorText = "联系电话不能为空!";
                return;
            }

            try
            {
                ac01.AC004 = te_ac004.Text;
                ac01.AC005 = te_ac005.Text;
                ac01.AC002 = te_ac002.Text;
                ac01.AC250 = te_ac250.Text;
                ac01.Save();
                unitOfWork1.CommitTransaction();
                Tools.msg(MessageBoxIcon.Information, "提示", "保存成功!");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch(Exception ee)
            {
                unitOfWork1.RollbackTransaction();
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString());
            }
        }

        private void sb_print_cert_Click(object sender, EventArgs e)
        {
            
        }

        private void sb_print_pr_Click(object sender, EventArgs e)
        {

        }
    }
}