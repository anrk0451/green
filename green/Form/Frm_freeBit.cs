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
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using DevExpress.XtraTreeList;
using DevExpress.XtraSplashScreen;
using green.Action;
using DevExpress.Utils.DirectXPaint;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace green.Form
{
    public partial class Frm_freeBit : MyDialog
    {
        private DataTable dt_rg01 = new DataTable("Rg01");
        private DataView dv_rg01;
        private DataTable dt_bi01 = new DataTable("Bi01");
 
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;

        private OracleDataAdapter rg01Adapter = new OracleDataAdapter("select * from v_freebit where rg002 in ('0','1','2') order by rg001", SqlAssist.conn);
        private OracleDataAdapter bi01Adaapter = new OracleDataAdapter("select * from bi01 where rg001=:rg001 ",SqlAssist.conn);

        private OracleParameter op_rg001 = new OracleParameter("rg001", OracleDbType.Varchar2, 10);
        private DataRow[] rower_arry = null;

        public Frm_freeBit()
        {
            InitializeComponent();
            dv_rg01 = new DataView(dt_rg01);
 
            rg01Adapter.Fill(dt_rg01);
            dv_rg01.RowFilter = "rg002 <> '2'";

            bi01Adaapter.SelectCommand.Parameters.Add(op_rg001);
        }

        private void Frm_freeBit_Load(object sender, EventArgs e)
        {
            treeList1.DataSource = dv_rg01;
            treeList1.ExpandToLevel(1);
        }
        /// <summary>
        /// 节点焦点改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void treeList1_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            if(e.Node.Level == 0)
            {
                pictureBox1.Visible = true;
                gridControl1.Visible = false;           
            }
            else
            {
                pictureBox1.Visible = false;
                gridControl1.Visible = true;

                curRegionId = e.Node.GetValue("RG001").ToString();
                op_rg001.Value = curRegionId;
                bi01Adaapter.Fill(dt_bi01);

                rower_arry = dt_rg01.Select("RG009='" + curRegionId + "'", "RG001 ASC");

                SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");
                DrawGrid(e.Node.GetValue("RG001").ToString());
                SplashScreenManager.CloseDefaultWaitForm();
            }
        }

        /// <summary>
        /// 绘制墓位表格
        /// </summary>
        /// <param name="nodeId"></param>
        private void DrawGrid(string nodeId)
        {
            int rows = MiscAction.GetRowerCount(nodeId);
            int cols = MiscAction.GetMaxCols(nodeId);

            gridView1.BeginUpdate();

            /////////清除所有数据
            gridTable.Rows.Clear();
            gridTable.Columns.Clear();

            gridView1.RowHeight = AppInfo.GRID_HEIGHT;

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
            //DataRow[] rower_arry = dt_rg01.Select("RG009='" + nodeId + "'", "RG001 ASC");

            for (int i = 0; i <= rows - 1; i++)
            {
                row = gridTable.NewRow();
                bit_arry = dt_bi01.Select("RE001='" + rower_arry[i]["RG001"].ToString() + "'", "BI002 ASC");
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

            gridControl1.DataSource = gridTable;
            gridView1.PopulateColumns();


            //设置列宽 
            for (int i = 1; i <= cols; i++)
            {
                gridView1.Columns[i - 1].Width = AppInfo.GRID_WIDTH;
            }

            //grid标题            
            gridView1.ViewCaption = treeList1.FocusedNode.GetValue("RG003").ToString();
            gridView1.EndUpdate();

        }
        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle >= 0)
                { 
                    e.Info.DisplayText = rower_arry[e.RowHandle]["RG003"].ToString();
                }
            }
        }

        private void gridView1_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            string s_bitStatus = string.Empty;
            string s_rowerId = rower_arry[e.RowHandle]["RG001"].ToString();
            DataRow[] rows = dt_bi01.Select("RE001='" + s_rowerId + "' and BI003='" + e.CellValue.ToString() + "'");

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

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            GridHitInfo _info;
            Point _pt = gridView1.GridControl.PointToClient(Control.MousePosition);
            _info = gridView1.CalcHitInfo(_pt);
            if (_info.HitTest != GridHitTest.RowCell)
                return;


            string s_bitDesc = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, gridView1.FocusedColumn).ToString();
            string s_rowerId = rower_arry[gridView1.FocusedRowHandle]["RG001"].ToString();
            string s_status = MiscAction.GetTombStatus(s_rowerId, s_bitDesc);
            if(s_status == "2" || s_status == "3")
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "该墓位已经被占用!");
                return;
            }
            else if(s_status == "4")
            {
                return;
            }
            else if(s_status == "1")
            {
                string s_bitId = MiscAction.GetTombId(s_rowerId, s_bitDesc);
                this.swapdata["bi001"] = s_bitId;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            
        }
    }
}