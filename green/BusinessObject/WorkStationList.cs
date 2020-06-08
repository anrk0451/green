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
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using DevExpress.XtraGrid.Views.Base;
using green.Action;

namespace green.BusinessObject
{
    public partial class WorkStationList : BaseBusiness
    {
        private DataColumn col_ws001 = new DataColumn("WS001", typeof(string));   // 工作站编号
        private DataColumn col_ws003 = new DataColumn("WS003", typeof(string));   // 工作站名称
        private DataColumn col_ws005 = new DataColumn("WS005", typeof(string));   // 主机名称
        private DataColumn col_ws007 = new DataColumn("WS007", typeof(string));   // IP地址
        private DataColumn col_status = new DataColumn("STATUS", typeof(string)); // 状态 1-有效 0-无效

        private DataTable dt_ws01 = new DataTable("WS01");
        private OracleDataAdapter adapterWs01 = new OracleDataAdapter("select * from ws01 order by status desc,ws001", SqlAssist.conn);

        private OracleCommandBuilder builder = null;


        public WorkStationList()
        {
            InitializeComponent();
            dt_ws01.Columns.AddRange(new DataColumn[] { col_ws001, col_ws003, col_ws005, col_ws007, col_status });
            dt_ws01.PrimaryKey = new DataColumn[] { col_ws001 };                //设置主键
            builder = new OracleCommandBuilder(adapterWs01);
        }

        private void WorkStationList_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_ws01;
            adapterWs01.Fill(dt_ws01);
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            string ws001 = MiscAction.GetEntityPK("WS01");
            gridView1.SetRowCellValue(e.RowHandle, "WS001", ws001);
            gridView1.SetRowCellValue(e.RowHandle, "STATUS", "0");
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (XtraMessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    return;
                }
            }
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
        }

        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("WS003"))       //工作站名称
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "工作站名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "WS003") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "WS003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "【名称】已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("WS005"))   //主机名
            {
                if (!String.IsNullOrEmpty(e.Value.ToString()))
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "WS005") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "WS005").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "【主机名】已经存在!";
                            break;
                        }
                    }
                }
            }
            else if (colName.Equals("WS007"))   //IP地址
            {
                if (!String.IsNullOrEmpty(e.Value.ToString()))
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "WS007") == null) continue;

                        //如果名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "WS007").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "【IP地址】已经存在!";
                            break;
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 新增行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            int rowno = gridView1.FocusedRowHandle;
            /////// 设置焦点 开始编辑 !!!
            gridView1.FocusedColumn = gridView1.Columns["WS003"];
            gridView1.ShowEditor();
        }
        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            dt_ws01.Rows.Clear();
            adapterWs01.Fill(dt_ws01);
        }

        private bool SaveCheck()
        {
            if (!gridView1.PostEditor()) return false;
            if (!gridView1.UpdateCurrentRow()) return false;

            //检查输入完整性!!!
            foreach (DataRow dr in dt_ws01.Rows)
            {
                if (dr.RowState == DataRowState.Deleted) continue;
                if (string.IsNullOrEmpty(dr["WS003"].ToString()))
                {
                    gridView1.FocusedRowHandle = gridView1.GetRowHandle(dt_ws01.Rows.IndexOf(dr));
                    gridView1.FocusedColumn = gridColumn2;
                    XtraMessageBox.Show("工作站名称不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.ShowEditor();
                    return false;
                }
                else if (string.IsNullOrEmpty(dr["WS005"].ToString()) && dr["STATUS"].ToString() == "1")
                {
                    gridView1.FocusedRowHandle = gridView1.GetRowHandle(dt_ws01.Rows.IndexOf(dr));
                    gridView1.FocusedColumn = gridColumn3;
                    XtraMessageBox.Show("允许联机的工作站【计算机名称】不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.ShowEditor();
                    return false;
                }
                else if (string.IsNullOrEmpty(dr["WS007"].ToString()) && dr["STATUS"].ToString() == "1")
                {
                    gridView1.FocusedRowHandle = gridView1.GetRowHandle(dt_ws01.Rows.IndexOf(dr));
                    gridView1.FocusedColumn = gridColumn4;
                    XtraMessageBox.Show("允许联机的工作站【IP地址】不能为空!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    gridView1.ShowEditor();
                    return false;
                }
            }
            return true;
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Save();
        }

        private void Save()
        {
            if (!SaveCheck()) return;
            try
            {
                adapterWs01.Update(dt_ws01);
                XtraMessageBox.Show("保存成功!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ee)
            {
                XtraMessageBox.Show(ee.ToString(), "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
