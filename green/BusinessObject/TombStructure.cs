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
using DevExpress.XtraSplashScreen;
using green.DataSet;
using green.Form;
using green.Misc;
using Oracle.ManagedDataAccess.Client;
using DevExpress.XtraTreeList.Nodes;
using green.Action;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid;

namespace green.BusinessObject
{
    public partial class TombStructure : BaseBusiness
    {
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;
        private TG_ds tg_ds = new TG_ds();

        private DataView dv_rower = null;
        private DataView dv_region = null;

        
        public TombStructure()
        {
            InitializeComponent();
            dv_rower = new DataView(tg_ds.dt_rg01);
            dv_region = new DataView(tg_ds.dt_rg01);
        }

        private void TombStructure_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

            tg_ds.Fill_Rg01();
            tg_ds.Fill_Bi01();

            dv_region.RowFilter = "RG002 < '2'";
            treeList1.DataSource = dv_region;
            treeList1.ExpandToLevel(1);

            gridControl1.DataSource = dv_rower;

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
                barButtonItem1.Enabled = true;
                barButtonItem2.Enabled = false;

                pictureBox1.Visible = true;
                splitContainerControl2.Visible = false;
                return;
            }
            else
            {
                barButtonItem1.Enabled = false;
                barButtonItem2.Enabled = true;

                pictureBox1.Visible = false;
                splitContainerControl2.Visible = true;
                gridControl1.Visible = true;
                gridControl2.Visible = true;

                curRegionId = e.Node.GetValue("RG001").ToString();
                dv_rower.RowFilter = "rg009='" + curRegionId + "'";

                SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");
                DrawGrid(e.Node.GetValue("RG001").ToString());
                SplashScreenManager.CloseDefaultWaitForm();
            }
            
        }

        /// <summary>
        /// 新建墓区
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_region frm_1 = new Frm_region();
            ///传递数据
            frm_1.swapdata["dataset"] = tg_ds;   
            if(frm_1.ShowDialog() == DialogResult.OK)
            {
                Tools.msg(MessageBoxIcon.Information,"提示","操作成功!");
            }
            frm_1.Dispose();
        }

        /// <summary>
        /// 新建墓区排
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_rower frm_1 = new Frm_rower();
            ///传递数据
            frm_1.swapdata["dataset"] = tg_ds;
            frm_1.swapdata["rg009"] = curRegionId;
            if (frm_1.ShowDialog() == DialogResult.OK)
            {
                Tools.msg(MessageBoxIcon.Information, "提示", "操作成功!");
                DrawGrid(curRegionId);
            }
            frm_1.Dispose();
        }

        /// <summary>
        /// 编辑节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode curNode = treeList1.FocusedNode;
            if(curNode == null || curNode.Level == 0)
            {
                Tools.msg(MessageBoxIcon.Exclamation, "提示", "请选择要修改的节点!");
                return;
            }
            editNode(curNode);
        }

        private void editNode(TreeListNode node)
        {
            if (node.Level == 1)
            {
                Frm_region frm_1 = new Frm_region();
                frm_1.swapdata["rg001"] = node.GetValue("RG001");
                frm_1.swapdata["dataset"] = tg_ds;
                frm_1.ShowDialog();
                frm_1.Dispose();
            } 
        }

        private void treeList1_DoubleClick(object sender, EventArgs e)
        {
            TreeListNode curNode = treeList1.FocusedNode;
            if (  curNode.Level == 1)
            {
                editNode(curNode);
            }            
        }

        private void gridView1_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Column.FieldName.ToUpper() == "RG004")  //墓型
                e.DisplayText = MapperID.Mapper_mx(e.Value.ToString());
            else if(e.Column.FieldName.ToUpper() == "RG007")
            {
                if (e.Value.ToString() == "0")
                    e.DisplayText = "左起";
                else if (e.Value.ToString() == "1")
                    e.DisplayText = "右起";
            }
        }

        /// <summary>
        /// 双击编辑 排
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            int rowHandle = gridView1.FocusedRowHandle;
            GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));             
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow && hInfo.Column != null && rowHandle >=0)
                {
                    string s_rg001 = gridView1.GetRowCellValue(rowHandle, "RG001").ToString();
                    string s_rg009 = gridView1.GetRowCellValue(rowHandle, "RG009").ToString();
                    Frm_rower frm_1 = new Frm_rower();
                    frm_1.swapdata["dataset"] = tg_ds;
                    frm_1.swapdata["rg001"] = s_rg001;
                    frm_1.swapdata["rg009"] = s_rg009;
                    if(frm_1.ShowDialog() == DialogResult.OK)
                    {
                        //Tools.msg(MessageBoxIcon.Information, "提示", "");
                        this.DrawGrid(curRegionId);
                    }
                    frm_1.Dispose();
                }
            }             
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
                bit_arry = tg_ds.dt_bi01.Select("RE001='" + rower_arry[i]["RG001"].ToString() + "'","BI002 ASC");
                i_begin = Convert.ToInt32(rower_arry[i]["RG005"].ToString());
                i_end = Convert.ToInt32(rower_arry[i]["RG006"].ToString());

                
                for (int j = 1; j <= cols; j++)
                {
                    if(j<= (i_end - i_begin + 1))
                    {
                        row.SetField(j - 1, bit_arry[j-1]["BI003"]);
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
        /// 计算墓区最大列数
        /// </summary>
        /// <param name="regionId"></param>
        /// <returns></returns>
        private int calcMaxCols(string regionId)
        {
            int maxCols = 0;
            int i_begin, i_end;
            DataRow[] rowers = tg_ds.dt_rg01.Select("rg009='" + regionId + "'");
            foreach(DataRow dr in rowers)
            {
                int.TryParse(dr["RG005"].ToString(),out i_begin);
                int.TryParse(dr["RG006"].ToString(), out i_end);
                if ((i_end - i_begin + 1) > maxCols)
                    maxCols = i_end - i_begin + 1;
            }
            return maxCols;
        }

        /// <summary>
        /// 绘制 排名 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = gridView1.GetRowCellValue(e.RowHandle,"RG003").ToString();                     
                }                 
            }
        }

        /// <summary>
        /// 绘制图形单元格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView2_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string s_bitStatus = string.Empty;
            string s_rowerId = gridView1.GetRowCellValue(e.RowHandle, "RG001").ToString();
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
            else if(s_bitStatus == "2")     //已使用
            {
                e.Appearance.BackColor = Color.Red;
                e.Appearance.ForeColor = Color.White;
            }
            else if (s_bitStatus == "3")    //预定
            {
                e.Appearance.BackColor = Color.Blue;
                e.Appearance.ForeColor = Color.White;
            }
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            treeList1.SetFocusedNode(treeList1.GetNodeByVisibleIndex(0));
              
            try
            {
                tg_ds.Update_Rg01();
                tg_ds.Update_Bi01();                
                XtraMessageBox.Show("保存成功!", "提示");
            }
            catch (Exception ee)
            {
                Tools.msg(MessageBoxIcon.Error, "错误", "保存墓位结构错误!\n" + ee.ToString());          
            }
            this.RefreshData();
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            if (tg_ds.dt_rg01.GetChanges() != null || tg_ds.dt_bi01.GetChanges() != null )
            {
                DialogResult dr = XtraMessageBox.Show("刷新会丢失未保存的更新,是否继续?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                if (dr == DialogResult.Cancel) return;
            }

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

        private void gridView2_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo _info;
            Point _pt = gridView2.GridControl.PointToClient(Control.MousePosition);
            _info = gridView2.CalcHitInfo(_pt);
            if (_info.HitTest != GridHitTest.RowCell)
                return;


            string s_bitDesc = gridView2.GetRowCellValue(gridView2.FocusedRowHandle, gridView2.FocusedColumn).ToString();
            string s_rowerId = gridView1.GetRowCellValue(gridView2.FocusedRowHandle, "RG001").ToString();

            DataRow[] bitrow = tg_ds.dt_bi01.Select("RE001='" + s_rowerId + "' and BI003='"  + s_bitDesc + "'");
            if (bitrow.Length > 0)
            {
                Frm_bi01 frm_bi01 = new Frm_bi01();
                frm_bi01.swapdata["bit_record"] = bitrow[0];
                
                string s_rower = gridView1.GetRowCellValue(gridView2.FocusedRowHandle, "RG003").ToString();
                string s_position = treeList1.FocusedNode.GetValue("RG003").ToString() + s_rower + s_bitDesc + "#";

                frm_bi01.swapdata["position"] = s_position;
                frm_bi01.swapdata["dataset"] = tg_ds;
                if (frm_bi01.ShowDialog() == DialogResult.OK)
                {
                    this.DrawGrid(curRegionId);
                }
            }
            
             
        }

        /// <summary>
        /// 删除节点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TreeListNode node = treeList1.FocusedNode;
            if(node.Level == 0)
            {
                Tools.msg(MessageBoxIcon.Exclamation, "提示", "请选择要删除的墓区");
                return;
            }
            else if(node.Level == 1)
            {
                if (XtraMessageBox.Show("确认要删除墓区【" + node.GetValue("RG003").ToString() + "】", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
                DataRow[] bit_rows = tg_ds.dt_bi01.Select("RG001='" + curRegionId + "' and STATUS IN ('2','3')");
                if (bit_rows.Length > 0)
                {
                    Tools.msg(MessageBoxIcon.Warning, "提示", "墓区已经被占用!");
                    return;
                }
                this.Cursor = Cursors.WaitCursor;
                //删除号位
                bit_rows = tg_ds.dt_bi01.Select("RG001='" + curRegionId + "'");
                foreach(DataRow dr in bit_rows)
                {
                    tg_ds.dt_bi01.Rows.Remove(dr);
                }
                //删除墓区和排
                DataRow[] rg_rows = tg_ds.dt_rg01.Select("RG001='" + curRegionId + "' or RG009='" + curRegionId + "'");
                foreach(DataRow dr in rg_rows)
                {
                    tg_ds.dt_rg01.Rows.Remove(dr);
                }
                this.RefreshData();
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
