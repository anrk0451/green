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
using green.Domain;
using green.Dao;
using Oracle.ManagedDataAccess.Client;
using green.Misc;
using green.Form;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using green.DataSet;

namespace green.BusinessObject
{
    public partial class Operators : BaseBusiness
    {
        private Uc01_ds uc01_ds = new Uc01_ds();
        
        public Operators()
        {
            InitializeComponent();
            gridControl1.DataSource = uc01_ds.Uc01;
        }

        //对象装入事件
        private void Operators_Load(object sender, EventArgs e)
        {
            gridView1.ActiveFilter.Clear();
            gridView1.ActiveFilterString = "STATUS <> '0'";
            uc01_ds.uc01Adapter.Fill(uc01_ds.Uc01);
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
        /// 刷新数据
        /// </summary>
        private void RefreshData()
        {
            gridView1.BeginUpdate();
            uc01_ds.Uc01.Rows.Clear();
            uc01_ds.uc01Adapter.Fill(uc01_ds.Uc01);
            gridView1.EndUpdate();
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Frm_operator frm_Edit = new Frm_operator();
            //传递数据
            frm_Edit.swapdata["action"] = "add";
            frm_Edit.ShowDialog();
            if (frm_Edit.DialogResult == DialogResult.OK)
            {
                this.RefreshData();
                frm_Edit.Dispose();
            }
        }

        /// <summary>
        /// 编辑记录
        /// </summary>
        /// <param name="row"></param>
        private void EditData(int row)
        {
            string uc001 = gridView1.GetRowCellValue(row, "UC001").ToString();
            if (uc001 == AppInfo.ROOTID)
            {
                Tools.msg(MessageBoxIcon.Warning, "提示", "内置用户,不能编辑!");
                return;
            }

            Frm_operator frm_Edit = new Frm_operator();
            //传递数据
            frm_Edit.swapdata["action"] = "edit";
            frm_Edit.swapdata["uc001"] = uc001;
            frm_Edit.ShowDialog();
            if (frm_Edit.DialogResult == DialogResult.OK)
            {
                this.RefreshData();
                frm_Edit.Dispose();
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.FocusedRowHandle >= 0)
            {
                if (XtraMessageBox.Show("确认要删除当前的记录吗", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.No) return;
                if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"UC001").ToString() == AppInfo.ROOTID)
                {
                    Tools.msg(MessageBoxIcon.Exclamation, "提示", "内置操作员,不能删除!");
                    return;
                }
            }

            gridView1.SetFocusedRowCellValue("STATUS", "0");
            try
            {
                if (!gridView1.UpdateCurrentRow()) return;
                uc01_ds.uc01Adapter.Update(uc01_ds.Uc01);
                Tools.msg(MessageBoxIcon.Information, "提示", "删除成功!"); 
            }
            catch (Exception ee)
            {
                Tools.msg(MessageBoxIcon.Error, "错误", ee.ToString()); 
            }
        }
        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.RefreshData();
        }

        /// <summary>
        /// 设置管理员组，不能修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gridView1_ShowingEditor(object sender, CancelEventArgs e)
        {
            if(gridView1.FocusedColumn.FieldName.ToUpper() == "RO001")
            {
                if(gridView1.GetRowCellValue(gridView1.FocusedRowHandle,"RO001").ToString() == AppInfo.ADMINGID)
                {
                    e.Cancel = true;
                }
            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            int row = gridView1.FocusedRowHandle;
            if (row < 0) return;

            this.EditData(row);
        }

        private void gridView1_MouseDown(object sender, MouseEventArgs e)
        {
            GridHitInfo hInfo = gridView1.CalcHitInfo(new Point(e.X, e.Y));
            if (e.Button == MouseButtons.Left && e.Clicks == 2)
            {
                //判断光标是否在行范围内  
                if (hInfo.InRow)
                {
                    EditData(gridView1.FocusedRowHandle);
                }
            }
        }
    }
}
