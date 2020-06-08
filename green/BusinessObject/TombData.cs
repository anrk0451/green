using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using green.BaseObject;
using green.DataSet;
using DevExpress.XtraSplashScreen;
using DevExpress.XtraBars.Docking.Helpers;
using green.Misc;
using DevExpress.XtraGrid;
using green.Action;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using green.xpo.orcl;
using green.Form;

namespace green.BusinessObject
{
    public partial class TombData : BaseBusiness
    {
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;
        private TG_ds tg_ds = new TG_ds();

        private DataView dv_region = null;
        private DataView dv_rower = null;
        private BI01 bi01 = null;

        private Frm_tombdata frm_tombdata = null;
         
        public TombData()
        {
            InitializeComponent();
            dv_region = new DataView(tg_ds.dt_rg01);
            dv_rower = new DataView(tg_ds.dt_rg01);
 
            pictureBox1.Location = gridControl2.Location;
            pictureBox1.Size = gridControl2.Size;
        }

        private void TombData_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

            tg_ds.Fill_Rg01();
            tg_ds.Fill_Bi01();

            dv_region.RowFilter = "RG002 < '2'";
            treeList1.DataSource = dv_region;
            treeList1.ExpandToLevel(1);
 
            SplashScreenManager.CloseDefaultWaitForm();

             
        }
        /// <summary>
        /// 节点焦点改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, DevExpress.XtraTreeList.FocusedNodeChangedEventArgs e)
        {
            if (e.Node == null) return;
            if (e.Node.Level == 0)
            {
                gridControl2.Visible = false;
                pictureBox1.Visible = true; 
            }
            else
            {
                pictureBox1.Visible = false;
                gridControl2.Visible = true;
                 
                curRegionId = e.Node.GetValue("RG001").ToString();
                dv_rower.RowFilter = "rg009='" + curRegionId + "'";                

                SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");
                DrawGrid(e.Node.GetValue("RG001").ToString());
                SplashScreenManager.CloseDefaultWaitForm();
            }
            this.Tomb_stat();
        }
        /// <summary>
        /// 墓区数据统计
        /// </summary>
        private void Tomb_stat()
        {
            string s_rg001 = treeList1.FocusedNode.GetValue("RG001").ToString();
            lc_caption.Text = s_rg001 == AppInfo.TOMB_ROOT_ID ? "龙凤公墓" :  treeList1.FocusedNode.GetValue("RG003").ToString();
            lc_total.Text = "共有墓位" + BusinessAction.TombTotal_stat(s_rg001).ToString() ;
            lc_saled.Text = "已售墓位" + BusinessAction.TombSaled_stat(s_rg001).ToString();
            lc_unsaled.Text = "待售墓位" + BusinessAction.TombUnsaled_stat(s_rg001).ToString();
            lc_debt.Text = "其中欠费墓位" + BusinessAction.TombDebt_stat(s_rg001).ToString();
            lc_bookin.Text = "预定墓位" + BusinessAction.TombBookin_stat(s_rg001).ToString();
        }

        /// <summary>
        /// 计算墓区最大列数
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        private int calcMaxCols(string regionId)
        {
            int maxCols = 0;
            int i_begin, i_end;
            DataRow[] rowers = tg_ds.dt_rg01.Select("rg009='" + regionId + "'");
            foreach (DataRow dr in rowers)
            {
                int.TryParse(dr["RG005"].ToString(), out i_begin);
                int.TryParse(dr["RG006"].ToString(), out i_end);
                if ((i_end - i_begin + 1) > maxCols)
                    maxCols = i_end - i_begin + 1;
            }
            return maxCols;
        }

        /// <summary>
        /// 绘制墓位表格
        /// </summary>
        /// <param name="nodeId"></param>
        private void DrawGrid(string nodeId)
        {
            int rows = tg_ds.dt_rg01.Select("rg009='" + nodeId + "'").Length;
            int cols = calcMaxCols(nodeId);

            gridView2.BeginUpdate();

            /////////清除所有数据
            gridTable.Rows.Clear();
            gridTable.Columns.Clear();

            gridView2.RowHeight = AppInfo.GRID_HEIGHT;

            ////生成列
            DataColumn col = null;
            DataRow row = null;
            int i_begin, i_end;

            for (int i = 1; i <= cols; i++)
            {
                col = new DataColumn("col" + i.ToString(), typeof(string));
                col.ReadOnly = true;
                gridTable.Columns.Add(col);
            }

            DataRow[] bit_arry = null;
            DataRow[] rower_arry = tg_ds.dt_rg01.Select("RG009='" + nodeId + "'", "RG001 ASC");

            for (int i = 0; i <= rows - 1; i++)
            {
                row = gridTable.NewRow();
                bit_arry = tg_ds.dt_bi01.Select("RE001='" + rower_arry[i]["RG001"].ToString() + "'", "BI002 ASC");
                i_begin = Convert.ToInt32(rower_arry[i]["RG005"].ToString());
                i_end = Convert.ToInt32(rower_arry[i]["RG006"].ToString());


                for (int j = 1; j <= cols; j++)
                {
                    if (j <= (i_end - i_begin + 1))
                    {
                        row.SetField(j - 1, bit_arry[j - 1]["BI003"]);
                    }
                    else
                        row.SetField(j - 1, "");
                }
                gridTable.Rows.Add(row);
            }

            gridControl2.DataSource = gridTable;
            gridView2.PopulateColumns();

            //设置列宽 
            for (int i = 1; i <= cols; i++)
            {
                gridView2.Columns[i - 1].Width = AppInfo.GRID_WIDTH;
            }

            //grid标题            
            gridView2.ViewCaption = treeList1.FocusedNode.GetValue("RG003").ToString();
            gridView2.EndUpdate();
        }
        /// <summary>
        /// 绘制排名
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0 && e.RowHandle < dv_rower.Count)
                {
                    e.Info.DisplayText = dv_rower[e.RowHandle].Row["RG003"].ToString(); 
                }
            }
        }

        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string s_bitStatus = string.Empty;

            string s_rowerId = string.Empty;

            if (e.RowHandle < 0 || e.RowHandle >= dv_rower.Count) return;

            s_rowerId = dv_rower[e.RowHandle].Row["RG001"].ToString();
            //gridView1.GetRowCellValue(e.RowHandle, "RG001").ToString();

            DataRow[] rows = tg_ds.dt_bi01.Select("RE001='" + s_rowerId + "' and BI003='" + e.CellValue.ToString() + "'");
            if (rows.Length > 0)
                s_bitStatus = rows[0]["STATUS"].ToString();
            else
                s_bitStatus = "4";

            if (s_bitStatus == "1")         //空闲
            {
                e.Appearance.BackColor = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }
            else if (s_bitStatus == "4")    //冻结
            {
                e.Appearance.BackColor = Color.White;
                e.Appearance.ForeColor = Color.White;
            }
            else if (s_bitStatus == "2")     //已使用
            {
                e.Appearance.BackColor = Color.Yellow;
                e.Appearance.ForeColor = Color.Black;
            }
            else if (s_bitStatus == "3")    //预定
            {
                e.Appearance.BackColor = Color.Blue;
                e.Appearance.ForeColor = Color.White;
            }
        }

        private void gridView2_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hInfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Right && e.Clicks == 1 && hInfo.InRow && hInfo.Column != null)
            {
                contextMenuStrip1.Show(MousePosition);
                //处理菜单项状态
                string s_bi003 = gridView2.GetRowCellValue(hInfo.RowHandle, hInfo.Column).ToString();
                int rowerOrder = hInfo.RowHandle + 1;
                string s_bi001 = MiscAction.GetBi001ByBitdescRowOrder(curRegionId, s_bi003, rowerOrder);
                bi01 = session1.GetObjectByKey<BI01>(s_bi001);
                if(bi01.STATUS == '2')
                {
                    toolStripMenuItem_edit.Enabled = true;
                    ToolStripMenuItem_move.Enabled = true;
                    toolStripMenuItem_mfee.Enabled = true;
                }
                else
                {
                    toolStripMenuItem_edit.Enabled = false;
                    ToolStripMenuItem_move.Enabled = false;
                    toolStripMenuItem_mfee.Enabled = false;
                }
            }
        }

        /// <summary>
        /// 鼠标指针离开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_MouseLeave(object sender, EventArgs e)
        {
            panel_info.Visible = false;
        }
        /// <summary>
        /// 鼠标指针进入网格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_MouseEnter(object sender, EventArgs e)
        {
            GridHitInfo hInfo = gridView2.CalcHitInfo(MousePosition);
            if (hInfo.InRow && hInfo.Column != null)
                panel_info.Visible = true;
            else
                panel_info.Visible = false;
        }

        private void gridView2_MouseMove(object sender, MouseEventArgs e)
        {
            GridHitInfo hInfo = gridView2.CalcHitInfo(new Point(e.X, e.Y));
            if (hInfo.InRow && hInfo.Column != null)
            {
                panel_info.Visible = true;
                string s_bi003 = gridView2.GetRowCellValue(hInfo.RowHandle, hInfo.Column).ToString();
                int rowerOrder = hInfo.RowHandle + 1;
                string s_bi001 = MiscAction.GetBi001ByBitdescRowOrder(curRegionId, s_bi003, rowerOrder);
                bi01 = session1.GetObjectByKey<BI01>(s_bi001);
                if (bi01 != null)
                {
                    lc_position.Text = MiscAction.GetTombPosition(bi01.BI001);
                    lc_price.Text = string.Format("{0:C2}", bi01.PRICE);
                    lc_bi005.Text = MiscAction.Mapper_mx(bi01.BI005);

                    if (bi01.STATUS != '2')
                    {
                        lc_ac003_cap.Visible = false;
                        lc_ac003.Visible = false;
                        lc_ac049_cap.Visible = false;
                        lc_ac049.Visible = false;
                    }
                    else
                    {
                        lc_ac003_cap.Visible = true;
                        lc_ac003.Visible = true;
                        lc_ac049_cap.Visible = true;
                        lc_ac049.Visible = true;

                        AC01 ac01 = session1.GetObjectByKey<AC01>(bi01.AC001);
                        if(ac01 != null)
                        {
                            lc_ac003.Text = ac01.AC003;
                            lc_ac049.Text = ac01.AC049.ToString();
                        }
                        
                    }
                }
            } 
            else
                panel_info.Visible = false;             
        }
        /// <summary>
        /// 编辑 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_edit_Click(object sender, EventArgs e)
        {
            if (bi01 == null) return;
            string s_ac001 = bi01.AC001;
            Frm_checkinEdit frm_1 = new Frm_checkinEdit();
            frm_1.swapdata["ac001"] = s_ac001;
             
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                //this.RefreshData();
            }
            frm_1.Dispose();
        }

        private void RefreshData()
        {
            SplashScreenManager.ShowDefaultWaitForm("请等待", "刷新数据....");

            treeList1.BeginUpdate();
            treeList1.ClearNodes();

            tg_ds.dt_rg01.Rows.Clear();
            tg_ds.dt_bi01.Rows.Clear();

            tg_ds.Fill_Rg01();
            tg_ds.Fill_Bi01();

            treeList1.ExpandToLevel(1);
            treeList1.EndUpdate();

            SplashScreenManager.CloseDefaultWaitForm();
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }
        /// <summary>
        /// 缴管理费
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void toolStripMenuItem_mfee_Click(object sender, EventArgs e)
        {
            if (bi01 == null) return;
            V_AC01_REPORT ac01 = null;
            ac01 = session1.GetObjectByKey<V_AC01_REPORT>(bi01.AC001);        
            Frm_PayManageFee frm_1 = new Frm_PayManageFee();
            frm_1.swapdata["ac01"] = ac01;
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                
            }
            frm_1.Dispose();
           
        }
        /// <summary>
        /// 墓位调整
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolStripMenuItem_move_Click(object sender, EventArgs e)
        {
            if (bi01 == null) return;
            V_AC01_REPORT ac01 = null;
            ac01 = session1.GetObjectByKey<V_AC01_REPORT>(bi01.AC001);
            Frm_tombTransfer frm_1 = new Frm_tombTransfer();
            frm_1.swapdata["ac01"] = ac01;
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                this.RefreshData();
            }
            frm_1.Dispose();
        }
        /// <summary>
        /// 显示待售墓位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            if (frm_tombdata == null || frm_tombdata.IsDisposed) frm_tombdata = new Frm_tombdata();
            frm_tombdata.action = "unsaled";
            if (treeList1.FocusedNode.Level == 0)
            {
                frm_tombdata.unitid = AppInfo.TOMB_ROOT_ID;
                frm_tombdata.unitname = "龙凤坡公墓";
            }
            else
            {
                frm_tombdata.unitid = curRegionId;
                frm_tombdata.unitname = treeList1.FocusedNode.GetValue("RG003").ToString();
            }
            frm_tombdata.dosearch();            
            frm_tombdata.TopMost = true;
            frm_tombdata.Show();
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            if (frm_tombdata == null || frm_tombdata.IsDisposed) frm_tombdata = new Frm_tombdata();
            frm_tombdata.action = "saled";
            if (treeList1.FocusedNode.Level == 0)
            {
                frm_tombdata.unitid = AppInfo.TOMB_ROOT_ID;
                frm_tombdata.unitname = "龙凤坡公墓";
            }
            else
            {
                frm_tombdata.unitid = curRegionId;
                frm_tombdata.unitname = treeList1.FocusedNode.GetValue("RG003").ToString();
            }
            frm_tombdata.dosearch();
            frm_tombdata.TopMost = true;
            frm_tombdata.Show();
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            if (frm_tombdata == null || frm_tombdata.IsDisposed) frm_tombdata = new Frm_tombdata();
            frm_tombdata.action = "debt";
            if (treeList1.FocusedNode.Level == 0)
            {
                frm_tombdata.unitid = AppInfo.TOMB_ROOT_ID;
                frm_tombdata.unitname = "龙凤坡公墓";
            }
            else
            {
                frm_tombdata.unitid = curRegionId;
                frm_tombdata.unitname = treeList1.FocusedNode.GetValue("RG003").ToString();
            }
            frm_tombdata.dosearch();
            frm_tombdata.TopMost = true;
            frm_tombdata.Show();
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (treeList1.FocusedNode == null) return;
            if (frm_tombdata == null || frm_tombdata.IsDisposed) frm_tombdata = new Frm_tombdata();
            frm_tombdata.action = "bookin";
            if (treeList1.FocusedNode.Level == 0)
            {
                frm_tombdata.unitid = AppInfo.TOMB_ROOT_ID;
                frm_tombdata.unitname = "龙凤坡公墓";
            }
            else
            {
                frm_tombdata.unitid = curRegionId;
                frm_tombdata.unitname = treeList1.FocusedNode.GetValue("RG003").ToString();
            }
            frm_tombdata.dosearch();
            frm_tombdata.TopMost = true;
            frm_tombdata.Show();  
        }
        /// <summary>
        /// 组件销毁时执行清理
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (frm_tombdata != null)
            {
                using (frm_tombdata)
                {
                    frm_tombdata.Close();
                }
            }
            base.OnHandleDestroyed(e);
        }
    }
}
