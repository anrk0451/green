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
using Oracle.ManagedDataAccess.Client;
using green.DataSet;
using DevExpress.XtraEditors.Filtering.Templates;
using DevExpress.XtraEditors.Controls;
using DevExpress.XtraGrid.Views.Base;
using green.xpo.orcl;
using DevExpress.Internal.WinApi.Windows.UI.Notifications;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;

namespace green.Form
{
    public partial class Frm_checkin : MyDialog
    {
        private string s_ac001 = string.Empty;          //购墓流水号
        private string s_ac199 = string.Empty;          //安葬批次号
        private string s_bk001 = string.Empty;          //预定流水号

        private string s_action = string.Empty;     
        private string s_bi001 = string.Empty;          //墓位编号
        private BI01 bi01 = null;
        private BK01 bk01 = null;

        private DataDict_ds dd_ds = new DataDict_ds();  //字典数据源
 
        private decimal dec_total = decimal.Zero;
        private decimal dec_sales = decimal.Zero;

        private string s_fa001 = string.Empty;
        private AC01 ac01 = null;
         
        public Frm_checkin()
        {
            InitializeComponent();

            le_mx.Properties.DataSource = dd_ds.dv_mx;
            rep_item_gx.DataSource = dd_ds.dv_gx;
            rep_item_zs.DataSource = dd_ds.dv_zs;
        }
        private void Frm_checkin_Load(object sender, EventArgs e)
        {
            if (this.swapdata.ContainsKey("action"))
                s_action = this.swapdata["action"].ToString();
            else
                s_action = "add";

            if(s_action == "add")
            {
                s_ac001 = MiscAction.GetEntityPK("AC01");
                s_ac199 = s_ac001;
                te_free_nums.EditValue = MiscAction.GetSysParaValue1("FREEYEARS");
                de_ac049.EditValue = Tools.GetServerDate();
            }
            else if(s_action == "bookin")   //预定登记
            {
                s_bk001 = this.swapdata["bk001"].ToString();
                bk01 = unitOfWork1.GetObjectByKey<BK01>(s_bk001);
                bi01 = unitOfWork1.GetObjectByKey<BI01>(bk01.BI001);
                if (bi01 != null && bk01 != null)
                {
                    be_position.Text = MiscAction.GetTombPosition(bi01.BI001);
                    te_fixprice.EditValue = bi01.PRICE;
                    te_price.EditValue = bi01.PRICE;
                    le_mx.EditValue = bi01.BI005;
                    te_ac003.Text = bk01.BK003;

                    if (bi01.PRICE > 0)
                        te_fixprice.ReadOnly = true;
                    else
                        te_fixprice.ReadOnly = false;

                    if (bi01.BI005 != null)
                        le_mx.ReadOnly = true;
                    else
                        le_mx.ReadOnly = false;

                    s_ac001 = MiscAction.GetEntityPK("AC01");
                    s_ac199 = s_ac001;
                    te_free_nums.EditValue = MiscAction.GetSysParaValue1("FREEYEARS");
                    de_ac049.EditValue = Tools.GetServerDate();
                }
            }
            else //如果是更新资料,检索信息
            {
                s_ac001 = this.swapdata["ac001"].ToString();
            }

            ///增加购墓自动选择项
            if (s_action == "add" || s_action == "bookin")
            {
                AutoAdd();
            }

        }

        /// <summary>
        /// 购墓时必选项目
        /// </summary>
        private void AutoAdd()
        {
            using (OracleDataReader reader = SqlAssist.ExecuteReader("select * from v_all_validitem where autochoose = '1' "))
            {
                SA01 sa01 = null;
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        sa01 = new SA01(unitOfWork1);
                        sa01.SA004 = reader["ITEM_ID"].ToString();
                        sa01.SA001 = MiscAction.GetEntityPK("SA01");
                        sa01.SA002 = '1';                               //销售项目类型 1 - 商品或服务
                        sa01.SA003 = reader["ITEM_NAME"].ToString();    //项目名称
                        sa01.STATUS = '1';
                        sa01.AC001 = s_ac001;                           //购墓流水号
                        sa01.SA005 = '0';                               //销售类型 0-购墓
                        sa01.SA008 = '0';                               //结算标志 0-未结算
                        sa01.SA100 = Envior.cur_userId;                 //经办人
                        sa01.SA200 = Tools.GetServerDate();             //经办日期
                        sa01.SA025 = Convert.ToDecimal(reader["TAXRATE"]);

                        if (sa01.SA003 == "管理费")
                            sa01.NUMS = MiscAction.GetSysParaValue1("MANAGEFEE_NUMS");
                        else
                            sa01.NUMS = 1;

                        sa01.PRICE = Convert.ToDecimal(reader["PRICE"]);//单价
                        sa01.SA007 = sa01.PRICE * sa01.NUMS;
                        sa01.SA006 = sa01.PRICE;

                        xpCollection_sa01.Add(sa01);
                    }
                }
            }
        }


        /// <summary>
        /// 调用 选择墓位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void be_position_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (s_action == "bookin") return;
            Frm_freeBit frm_free = new Frm_freeBit();
            if(frm_free.ShowDialog() == DialogResult.OK)
            {
                s_bi001 = frm_free.swapdata["bi001"].ToString();
                bi01 = unitOfWork1.GetObjectByKey(typeof(BI01), s_bi001) as BI01;
                if (bi01 != null)
                {
                    be_position.Text = MiscAction.GetTombPosition(bi01.BI001);
                    te_fixprice.EditValue = bi01.PRICE;
                    te_price.EditValue = bi01.PRICE;
                    le_mx.EditValue = bi01.BI005;

                    if (bi01.PRICE > 0 )
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

        private void be_position_DoubleClick(object sender, EventArgs e)
        {
            this.be_position_ButtonClick(sender, new ButtonPressedEventArgs(be_position.Properties.Buttons[0]));
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
        /// <summary>
        /// 数据改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        /// 显示行号
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
        /// 编辑校验
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
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

        
        /// <summary>
        /// 服务祭品-新行初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            string s_sa001 = MiscAction.GetEntityPK("SA01");
            gridView1.SetRowCellValue(rowHandle, "SA001", s_sa001);           //销售流水号
            gridView1.SetRowCellValue(rowHandle, "SA002", '1');               //销售项目类型 1-商品或服务
            gridView1.SetRowCellValue(rowHandle, "STATUS", "1");
            gridView1.SetRowCellValue(rowHandle, "AC001", s_ac001);           //购墓流水号
            gridView1.SetRowCellValue(rowHandle, "SA005", "0");               //销售类型 0-购墓
            gridView1.SetRowCellValue(rowHandle, "SA008", "0");               //结算标志 0-未结算
            gridView1.SetRowCellValue(rowHandle, "SA100", Envior.cur_userId); //经办人
            gridView1.SetRowCellValue(rowHandle, "SA200", Tools.GetServerDate()); //经办日期
            gridView1.SetRowCellValue(rowHandle, "NUMS", 1);                  //数量
        }
        /// <summary>
        /// 单元格值改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "SA004" && (e.Value != DBNull.Value))   //选择服务或商品
            {
                DataRow dr_item = SqlAssist.GetSingelRow("select * from v_all_validItem where item_id='" + e.Value + "'");
                if (dr_item != null)
                {
                    gridView1.SetRowCellValue(e.RowHandle, "SA003", dr_item["ITEM_NAME"].ToString());  //项目名称
                    gridView1.SetRowCellValue(e.RowHandle, "PRICE", dr_item["PRICE"]);               //单价
                    gridView1.SetRowCellValue(e.RowHandle, "SA006", dr_item["PRICE"]);               //原始单价
                    gridView1.SetRowCellValue(e.RowHandle, "SA025", dr_item["TAXRATE"]);             //税率          
                    //计算小计金额
                    decimal dec_price = decimal.Zero;
                    int i_nums = 0;
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                    {
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                    }
                }

            }
            else if (e.Column.FieldName.ToUpper() == "PRICE")                            //修改单价
            {
                decimal dec_price = decimal.Zero;
                int i_nums;
                if (e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }
                else
                {
                    gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }

            }
            else if (e.Column.FieldName.ToUpper() == "NUMS")
            {
                decimal dec_price = decimal.Zero;
                int i_nums;
                if (e.Value != DBNull.Value)
                {
                    if (decimal.TryParse(gridView1.GetRowCellValue(e.RowHandle, "PRICE").ToString(), out dec_price))
                    {
                        i_nums = Convert.ToInt32(gridView1.GetRowCellValue(e.RowHandle, "NUMS"));
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", i_nums * dec_price);
                    }
                    else
                        gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);
                }
                else
                    gridView1.SetRowCellValue(e.RowHandle, "SA007", 0);

            }
            else if (e.Column.FieldName.ToUpper() == "SA007")   //销售金额
            {
                dec_sales = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    if (i == e.RowHandle)
                    {
                        dec_sales += Convert.ToDecimal(e.Value);
                    }
                    else
                    {
                        if (gridView1.GetRowCellValue(i, "SA007") != null && gridView1.GetRowCellValue(i, "SA007") != System.DBNull.Value)
                            dec_sales += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
                    }

                }
                ///// 如果是新行
                if (e.RowHandle < 0)
                {
                    dec_sales += Convert.ToDecimal(e.Value);
                }
                this.Calc_sum();
            }
        }

        private void groupControl3_CustomButtonClick(object sender, DevExpress.XtraBars.Docking2010.BaseButtonEventArgs e)
        {
            string s_button = e.Button.Properties.ToolTip.ToString();
            if (s_button == "增加")
            {
                gridView1.AddNewRow();
                int rowno = gridView1.FocusedRowHandle;
                /////// 设置焦点 开始编辑 !!!
                gridView1.FocusedColumn = gridView1.Columns["SA004"];
                gridView1.ShowEditor();
            }
            else if(s_button == "删除")
            {
                gridView1.DeleteRow(gridView1.FocusedRowHandle);
                //gridView1.UpdateCurrentRow();
            }
        }
        /// <summary>
        /// 计算总金额
        /// </summary>
        private void Calc_sum()
        {
            decimal dec_tomb = decimal.Zero;
            if(bi01 != null)          
                if (!decimal.TryParse(te_price.Text, out dec_tomb)) dec_tomb = 0;
          
            lc_total.Text = string.Format("{0:F2}", dec_sales + dec_tomb);             
        }
        /// <summary>
        /// 墓位售价改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void te_price_EditValueChanged(object sender, EventArgs e)
        {
            this.Calc_sum();
        }

        private void te_price_EditValueChanging(object sender, ChangingEventArgs e)
        {
            decimal dec_1 = decimal.Zero;
            if(decimal.TryParse(te_price.Text,out dec_1))
            {
                if (dec_1 < 0)
                {
                    te_price.ErrorText = "请输入正确的价格!";
                    te_price.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    return;
                }
            }
        }

        private void te_free_nums_EditValueChanging(object sender, ChangingEventArgs e)
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
        /// 销售项目删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_RowDeleted(object sender, DevExpress.Data.RowDeletedEventArgs e)
        {
            dec_sales = 0;
            for (int i = 0; i < gridView1.RowCount; i++)
            {
                if (gridView1.GetRowCellValue(i, "SA007") != null && gridView1.GetRowCellValue(i, "SA007") != System.DBNull.Value)
                {
                    dec_sales += Convert.ToDecimal(gridView1.GetRowCellValue(i, "SA007"));
                }
            }
            this.Calc_sum();
        }

        private void gridView1_ValidatingEditor(object sender, BaseContainerValidateEditorEventArgs e)
        {
            if(gridView1.FocusedColumn.FieldName.ToUpper() == "SA004")
            {
                for (int i = 0; i < gridView1.RowCount - 1; i++)
                {
                    if (i == (sender as ColumnView).FocusedRowHandle) continue;
                    if (gridView1.GetRowCellValue(i, "SA004") == null) continue;

                    //如果项目相同,则校验不通过!                        
                    if (String.Equals(gridView1.GetRowCellValue(i, "SA004").ToString(), e.Value.ToString()))
                    {
                        e.Valid = false;
                        e.ErrorText = "项目已经存在!";
                        break;
                    }
                }
            }
            else if (gridView1.FocusedColumn.FieldName.ToUpper() == "NUMS")
            {
                int i_num;
                if (int.TryParse(e.Value.ToString(), out i_num))
                {
                    if (i_num <= 0)
                    {
                        e.ErrorText = "请输入数量!";
                        e.Valid = false;
                        return;
                    }
                }
            }
            else if (gridView1.FocusedColumn.FieldName.ToUpper() == "PRICE")
            {
                decimal dec_1 = decimal.Zero;
                if (decimal.TryParse(e.Value.ToString(), out dec_1))
                {
                    if (dec_1 <= 0)
                    {
                        e.ErrorText = "请输入正确的价格!";
                        e.Valid = false;
                        return;
                    }
                }
            }
        }

        private void sb_ok_Click(object sender, EventArgs e)
        {
            bool b_payrecord = false;
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
				{
                    ac01.AC040 = ac01.AC049.AddYears(ac01.AC038);
                    b_payrecord = true;
                }                    
				else
				{
                    int rowHandle = gridView1.LocateByValue("SA003", "管理费");
                    if (rowHandle >= 0)
					{
                        ac01.AC040 = ac01.AC049.AddYears(Convert.ToInt32(gridView1.GetRowCellValue(rowHandle,"NUMS")));
                        b_payrecord = true;
					}
                    else
                        ac01.AC040 = ac01.AC049;
                }
                   
                ac01.AC042 = '1';                       //缴费状态
                ac01.AC048 = s_fa001;                   //缴费流水号
                ac01.AC100 = Envior.cur_userId;         //经办人
                ac01.AC200 = Tools.GetServerDate();     //经办日期
                ac01.AC250 = te_ac250.Text;             //备注
                ac01.AC300 = '1';                       //登记类型 1-正常登记 0-原始登记
                ac01.STATUS = '1';                      //当前状态     
                ac01.Save();
 
                //2.销售项目
                foreach(SA01 sa01 in xpCollection_sa01)
                {
                    sa01.SA008 = '1';                   //结算状态 
                    sa01.SA010 = s_fa001;               //结算流水号
                    sa01.SA020 = 'T';                   //发票类型
                    sa01.SA100 = Envior.cur_userId;     //经办人
                    sa01.SA200 = Tools.GetServerDate(); //经办日期
                    sa01.Save();
                    if(sa01.SA003 == "管理费"  /*如果是管理费*/ && sa01.SA002 == '1')
					{
                        //插入缴费表 
                        PR01 pr01 = new PR01(unitOfWork1);
                        pr01.PR001 = s_fa001;
                        pr01.AC001 = s_ac001;
                        pr01.PR002 = ac01.AC049;   //缴费开始日期
                        pr01.PR003 = ac01.AC049.AddYears(Convert.ToInt32(sa01.NUMS));
                        pr01.PRICE = sa01.PRICE;
                        pr01.NUMS = sa01.NUMS;
                        pr01.PR007 = sa01.SA007;
                        pr01.PR008 = '1';
                        pr01.PR100 = Envior.cur_userId;
                        pr01.PR200 = Tools.GetServerDate();
                        pr01.STATUS = '1';
                        pr01.Save();
                    }
                }


                //3.号位信息  
                bi01.STATUS = '2';                                  //已使用
                bi01.PRICE = Convert.ToDecimal(te_fixprice.Text);   //定价
                bi01.BI005 = le_mx.EditValue.ToString();            //墓型
                bi01.AC001 = s_ac001;                               //购墓登记编号
                bi01.Save();
 
                //4.财务收费信息
                dec_total = dec_sales + Convert.ToDecimal(te_price.Text);
                FA01 fa01 = new FA01(unitOfWork1);
                fa01.FA001 = s_fa001;                               //缴费流水号
                fa01.AC001 = s_ac001;                               //购墓流水号
                fa01.FA002 = '0';                                   //收费类型 0-购墓
                fa01.FA003 = te_ac003.Text;                         //缴费人
                fa01.FA004 = dec_total;                             //收费金额
                fa01.FA190 = '0';                                   //开票标志-未开票
                fa01.FA100 = Envior.cur_userId;                     //收费人

                fa01.FA180 = MiscAction.GetTombPosition(bi01.BI001);//备注(墓穴位置)
                fa01.FA200 = Tools.GetServerDate();                 //缴费时间
                fa01.STATUS = "1";                                  //状态
                fa01.WS001 = Envior.WORKSTATIONID;                  //工作站标识
                fa01.Save();

                //如果是预定的记录 
                if(bk01 != null && s_action == "bookin")
                {
                    bk01.STATUS = '2';   //已登记
                    bk01.Save();
                }

                unitOfWork1.CommitTransaction();
                ///设置附加信息
                BusinessAction.SetExtraInfo(s_ac001);

                int i_papers = BusinessAction.GetInvoicePapers(s_fa001);
                if(XtraMessageBox.Show("登记办理成功!\r\n" + "本次结算共需要" + i_papers.ToString() + "张发票,现在开具吗?","提示",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    sb_ok.Enabled = false;
                    //获取税务客户信息
                    string s_ac003 = te_ac003.Text;
                    Frm_TaxClientInfo frm_taxClient = new Frm_TaxClientInfo(s_ac003);
                    if (frm_taxClient.ShowDialog() != DialogResult.OK) return;
                    TaxClientInfo clientInfo = frm_taxClient.swapdata["taxclientinfo"] as TaxClientInfo;

                    CriteriaOperator criteria = CriteriaOperator.Parse("FA001='" + s_fa001 + "'");
                    XPCollection<FP01> xpCollection_fp01 = new XPCollection<FP01>(PersistentCriteriaEvaluationBehavior.BeforeTransaction, unitOfWork1,criteria);
                    foreach(FP01 fp01 in xpCollection_fp01)
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
                ////打印证书
                if(XtraMessageBox.Show("现在打印【购墓证书】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PrintAction.PrintCert(s_ac001);
                }
                if (b_payrecord && XtraMessageBox.Show("现在打印【缴费记录】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    PrintAction.PrintPayRecord(s_fa001);
                }
                ////打印购墓协议
                //if (XtraMessageBox.Show("现在打印【购墓协议】?", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    PrintAction.PrintProtocol(s_ac001);
                //}


                this.Close();

            }
            catch(Exception ee)
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
            if(bi01 == null)
            {
                Tools.msg(MessageBoxIcon.Warning,"提示","请先选择一个墓位!");
                return false;
            }else if (string.IsNullOrEmpty(te_ac003.Text))
            {
                te_ac003.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                te_ac003.ErrorText = "请输入购墓人姓名!";
                return false;
            }else if (string.IsNullOrEmpty(te_ac004.Text))
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

            if (decimal.TryParse(te_price.Text,out dec_1))
            {
                if(dec_1 <= 0)
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
            if(le_mx.EditValue == DBNull.Value || le_mx.EditValue == null)
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

            DateTime dt_ac049;
            if (!DateTime.TryParse(de_ac049.Text, out dt_ac049))
            {
                de_ac049.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                de_ac049.ErrorText = "请输入正确的购墓日期!";
                return false;
            }

            if (!gridView1.PostEditor()) return false;
            if (!gridView1.UpdateCurrentRow()) return false;
            if (!gridView2.PostEditor()) return false;
            if (!gridView2.UpdateCurrentRow()) return false;

            //检查服务祭品是否输入完整
            foreach(SA01 s in xpCollection_sa01)
            {
                if (string.IsNullOrEmpty(s.SA004))
                {         
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入项目名称!");
                    return false;
                }else if(s.PRICE <=0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入价格!");
                    return false;
                }
                else if(s.NUMS <= 0)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "请输入数量!");
                    return false;
                }

                ///管理费和免费年限不可同时
                if(s.SA003 == "管理费" && Convert.ToInt32(te_free_nums.EditValue) > 0)
				{
                    te_free_nums.ErrorImageOptions.Alignment = ErrorIconAlignment.MiddleRight;
                    te_free_nums.ErrorText = "服务祭品中含有管理费,不能再设置免费年限!";
                    return false;
				}
            }
             
            return true;
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

        private void button1_Click(object sender, EventArgs e)
        {

		}

		private void sb_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
 
		private void te_fixprice_Validated(object sender, EventArgs e)
		{
            if (string.IsNullOrEmpty(te_price.Text) || Convert.ToDecimal(te_price.Text) == 0)
            {
                te_price.Text = te_fixprice.Text;
            }
        }
	}
}