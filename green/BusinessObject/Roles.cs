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
using DevExpress.XtraGrid.Views.Grid;
using green.Action;

namespace green.BusinessObject
{
    public partial class Roles : BaseBusiness
    {
        private DataTable dt_ro01 = new DataTable("Ro01");
        private OracleDataAdapter ro01Adapter = new OracleDataAdapter("select * from ro01 order by ro001",SqlAssist.conn);
        private OracleCommandBuilder builder = null;            //命令生成器(更新时候用)

        public Roles()
        {
            InitializeComponent();
            builder = new OracleCommandBuilder(ro01Adapter);
        }

        private void Roles_Load(object sender, EventArgs e)
        {
            gridControl1.DataSource = dt_ro01;
            ro01Adapter.Fill(dt_ro01);
        }

        /// <summary>
        /// 绘制行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 编辑验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            string colName = (sender as ColumnView).FocusedColumn.FieldName.ToUpper();
            if (colName.Equals("RO003"))
            {
                if (String.IsNullOrEmpty(e.Value.ToString()))
                {
                    e.Valid = false;
                    e.ErrorText = "角色名称不能为空!";
                }
                else
                {
                    for (int i = 0; i < gridView1.RowCount - 1; i++)
                    {
                        if (i == (sender as ColumnView).FocusedRowHandle) continue;
                        if (gridView1.GetRowCellValue(i, "RO003") == null) continue;

                        //如果角色名字相同,则校验不通过!                        
                        if (String.Equals(gridView1.GetRowCellValue(i, "RO003").ToString(), e.Value.ToString()))
                        {
                            e.Valid = false;
                            e.ErrorText = "角色名称已经存在!";
                            break;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 行验证
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ValidateRow(object sender, ValidateRowEventArgs e)
        {
            string value = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RO003").ToString();
            if (String.IsNullOrEmpty(value))
            {
                e.Valid = false;
                (sender as ColumnView).SetColumnError(gridView1.Columns["RO003"], "角色名称不能为空!");
            }
        }

        private void gridView1_InitNewRow(object sender, DevExpress.XtraGrid.Views.Grid.InitNewRowEventArgs e)
        {
            //// 初始化新行时触发(当在新行中)
            GridView view = sender as GridView;
            string ro001 = MiscAction.GetEntityPK("RO01");
            int currow = view.FocusedRowHandle;
            view.SetRowCellValue(e.RowHandle, view.Columns["RO001"], ro001); 
        }

        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.AddNewRow();
            int rowno = gridView1.FocusedRowHandle;
            /////// 设置焦点 开始编辑 !!!
            gridView1.FocusedColumn = gridView1.Columns["RO003"];
            gridView1.ShowEditor();
        }

        /// <summary>
        /// 刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridView1.BeginUpdate();
            dt_ro01.Rows.Clear();
            ro01Adapter.Fill(dt_ro01);
            gridView1.EndUpdate();
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
                int rowHandle = gridView1.FocusedRowHandle;
                if(gridView1.GetRowCellValue(rowHandle,"RO001").ToString() == AppInfo.ADMINGID)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "内置角色,不能删除!");
                    return;
                }
            }
            gridView1.DeleteRow(gridView1.FocusedRowHandle);
            gridView1.UpdateCurrentRow();
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!gridView1.PostEditor()) return;
            if (!gridView1.UpdateCurrentRow()) return;

            try
            {
                ro01Adapter.Update(dt_ro01);
                Tools.msg(MessageBoxIcon.Information, "提示", "保存成功!");                
            }
            catch (Exception ee)
            {
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString()); 
            }
        }

        /// <summary>
        /// 如果是管理员组,拒绝编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(gridView1.FocusedRowHandle >= 0)
            {
                string s_ro001 = gridView1.GetRowCellValue(gridView1.FocusedRowHandle, "RO001").ToString();
                if (s_ro001 == AppInfo.ADMINGID) e.Cancel = true;
            }            
        }
    }
}
