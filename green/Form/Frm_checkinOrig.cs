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
using green.Action;
using green.Misc;
using DevExpress.XtraGrid.Views.Base;
using green.DataSet;
using DevExpress.XtraEditors.Controls;
using green.xpo.orcl;

namespace green.Form
{
    public partial class Frm_checkinOrig : MyDialog
    {
        private string s_ac001 = string.Empty;          //购墓流水号
        private string s_ac199 = string.Empty;          //安葬批次号
        private string s_bk001 = string.Empty;          //预定流水号

        private string s_action = string.Empty;
        private string s_bi001 = string.Empty;          //墓位编号
        private BI01 bi01 = null;
        private DataDict_ds dd_ds = new DataDict_ds();  //字典数据源
         
        private string s_fa001 = string.Empty;
        private AC01 ac01 = null;

        public Frm_checkinOrig()
        {
            InitializeComponent();
            le_mx.Properties.DataSource = dd_ds.dv_mx;
            rep_item_gx.DataSource = dd_ds.dv_gx;
            rep_item_zs.DataSource = dd_ds.dv_zs;
        }

        private void Frm_checkinOrig_Load(object sender, EventArgs e)
        {
            s_ac001 = MiscAction.GetEntityPK("AC01");
            s_ac199 = s_ac001;
            te_free_nums.EditValue = MiscAction.GetSysParaValue1("FREEYEARS");

            de_ac049.EditValue = Tools.GetServerDate();
        }
        /// <summary>
        /// 选择墓位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void be_position_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (s_action == "bookin") return;
            Frm_freeBit frm_free = new Frm_freeBit();
            if (frm_free.ShowDialog() == DialogResult.OK)
            {
                s_bi001 = frm_free.swapdata["bi001"].ToString();
                bi01 = unitOfWork1.GetObjectByKey(typeof(BI01), s_bi001) as BI01;
                if (bi01 != null)
                {
                    be_position.Text = MiscAction.GetTombPosition(bi01.BI001);
                    te_fixprice.EditValue = bi01.PRICE;
                    te_price.EditValue = bi01.PRICE;
                    le_mx.EditValue = bi01.BI005;

                    if (bi01.PRICE > 0)
                        te_fixprice.ReadOnly = true;
                    else
                        te_fixprice.ReadOnly = false;

                    if (bi01.BI005 != null)
                        le_mx.ReadOnly = true;
                    else
                        le_mx.ReadOnly = false;
                }
            }
            frm_free.Dispose();
        }
        /// <summary>
        /// 选择墓位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void be_position_DoubleClick(object sender, EventArgs e)
        {
            this.be_position_ButtonClick(sender, new ButtonPressedEventArgs(be_position.Properties.Buttons[0]));
        }
        /// <summary>
        /// 逝者 新行初始化
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

        private void gridView2_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("AC113"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "逝者名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView2.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView2.GetRowCellValue(i, "AC113") == null) continue;

                        //如果角色名字相同,则校验不通过!                        
                        if (String.Equals(gridView2.GetRowCellValue(i, "AC113").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "逝者名称已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("AC116"))  //出生日期
            {
                if (e.Value != null && !(e.Value is DBNull))
                {
                    if (DateTime.Compare(Convert.ToDateTime(e.Value.ToString()), DateTime.Today) > 0)
                    {
                        e.Valid = false;
                        e.ErrorText = "出生日期错误!";
                    }
                }
            }
            else if (colName.Equals("AC118"))  //逝世时间
            {
                if (e.Value != null && !(e.Value is DBNull))
                {
                    if (DateTime.Compare(Convert.ToDateTime(e.Value.ToString()), DateTime.Today) > 0)
                    {
                        e.Valid = false;
                        e.ErrorText = "逝世日期错误!";
                    }
                }
            }
        }

        private void te_price_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            decimal dec_1 = decimal.Zero;
            if (decimal.TryParse(te_price.Text, out dec_1))
            {
                if (dec_1 < 0)
                {
                    te_price.ErrorText = "请输入正确的价格!";
                    te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    return;
                }
            }
        }

        private void te_free_nums_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int i;
            if (int.TryParse(te_free_nums.Text, out i))
            {
                if (i < 0)
                {
                    te_free_nums.ErrorText = "请输入正确的年限!";
                    te_free_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    return;
                }
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
                gridView2.FocusedColumn = gridView2.Columns["AC113"];
                gridView2.ShowEditor();
            }
            else if (s_button == "删除")
            {
                gridView2.DeleteRow(gridView2.FocusedRowHandle);
                //gridView2.UpdateCurrentRow();
            }
        }
        /// <summary>
        /// 身份证号校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void te_ac002_Validating(object sender, CancelEventArgs e)
        {
            string s_idcard = te_ac002.Text.Trim();
            if (string.IsNullOrWhiteSpace(s_idcard)) return;

            if (s_idcard.Length != 15 && s_idcard.Length != 18)
            {
                te_ac002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac002.ErrorText = "身份证号位数错误!";
                e.Cancel = true;
            }
            else if (s_idcard.Length == 15)
            {
                if (!Tools.CheckIDCard15(s_idcard))
                {
                    te_ac002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_ac002.ErrorText = "身份证号错误!";
                    e.Cancel = true;
                }
            }
            else if (s_idcard.Length == 18)
            {
                if (!Tools.CheckIDCard18(s_idcard))
                {
                    te_ac002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_ac002.ErrorText = "身份证号错误!";
                    e.Cancel = true;
                }
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
                //1.购墓登记
                ac01 = new AC01(unitOfWork1);
                ac01.AC001 = s_ac001;          //购墓登记编号
                ac01.AC002 = te_ac002.Text;    //购墓人身份证号 
                ac01.AC003 = te_ac003.Text;    //购墓人
                ac01.AC004 = te_ac004.Text;    //联系电话
                ac01.AC005 = te_ac005.Text;    //联系地址
                ac01.AC012 = bi01.RE001;       //排编号
                ac01.AC010 = bi01.RG001;       //墓区编号
                ac01.AC015 = bi01.BI001;       //墓位编号
                ac01.AC020 = Convert.ToDecimal(bi01.PRICE);         //墓位定价
                ac01.AC022 = Convert.ToDecimal(te_price.Text);      //售价
                ac01.AC038 = Convert.ToInt32(te_free_nums.Text);    //免费管理年限
                ac01.AC049 = Convert.ToDateTime(de_ac049.Text);     //购墓日期
        
                //管理费到期日期               
                if (ac01.AC038 > 0)
                    ac01.AC040 = ac01.AC049.AddYears(ac01.AC038);
                else
                    ac01.AC040 = ac01.AC049;

                ac01.AC042 = '1';                       //缴费状态
                ac01.AC048 = s_fa001;                   //缴费流水号
                ac01.AC100 = Envior.cur_userId;         //经办人
                ac01.AC200 = Tools.GetServerDate();     //经办日期
                ac01.AC250 = te_ac250.Text;             //备注
                ac01.AC300 = '0';                       //登记类型 1-正常登记 0-原始登记
                ac01.STATUS = '1';                      //当前状态     
                ac01.Save();

                
                //3.号位信息  
                bi01.STATUS = '2';                                  //已使用
                bi01.PRICE = Convert.ToDecimal(te_fixprice.Text);   //定价
                bi01.BI005 = le_mx.EditValue.ToString();            //墓型
                bi01.AC001 = s_ac001;                               //购墓登记编号
                bi01.Save();
 
                unitOfWork1.CommitTransaction();
                BusinessAction.SetExtraInfo(s_ac001);

                Tools.msg(MessageBoxIcon.Information, "提示", "登记成功!");

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
            if (bi01 == null)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "请先选择一个墓位!");
                return false;
            }
            else if (string.IsNullOrEmpty(te_ac003.Text))
            {
                te_ac003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac003.ErrorText = "请输入购墓人姓名!";
                return false;
            }
            else if (string.IsNullOrEmpty(te_ac004.Text))
            {
                te_ac004.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac004.ErrorText = "请输入联系电话!";
                return false;
            }
            else if (string.IsNullOrEmpty(te_ac002.Text))
            {
                te_ac002.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac002.ErrorText = "请输入购墓人身份证号!";
                return false;
            }

            DateTime dt_ac049;
            if(!DateTime.TryParse(de_ac049.Text,out dt_ac049))
			{
                de_ac049.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                de_ac049.ErrorText = "请输入正确的购墓日期!";
                return false;
			}


            decimal dec_1 = decimal.Zero;

            if (decimal.TryParse(te_fixprice.Text, out dec_1))
            {
                if (dec_1 <= 0)
                {
                    te_fixprice.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_fixprice.ErrorText = "请输入正确的墓位定价!";
                    return false;
                }
            }
            else
            {
                te_fixprice.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_fixprice.ErrorText = "请输入正确的墓位定价!";
                return false;
            }

            if (decimal.TryParse(te_price.Text, out dec_1))
            {
                if (dec_1 <= 0)
                {
                    te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_price.ErrorText = "请输入正确的墓位价格!";
                    return false;
                }
            }
            else
            {
                te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_price.ErrorText = "请输入正确的墓位价格!";
                return false;
            }
            if (le_mx.EditValue == DBNull.Value || le_mx.EditValue == null)
            {
                le_mx.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                le_mx.ErrorText = "请选择一个墓型!";
                return false;
            }
            if (string.IsNullOrEmpty(te_free_nums.Text))
            {
                te_free_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_free_nums.ErrorText = "请输入免费管理年限!";
                te_free_nums.Focus();
            }
 
            if (!gridView2.PostEditor()) return false;
            if (!gridView2.UpdateCurrentRow()) return false; 
            return true;
        }

    }
}