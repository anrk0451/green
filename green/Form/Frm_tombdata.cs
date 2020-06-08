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
using DevExpress.Data.Filtering;
using System.Web.UI.WebControls;
using green.Misc;

namespace green.Form
{
    public partial class Frm_tombdata : DevExpress.XtraEditors.XtraForm
    {
        public string action { get; set; }
        public string unitid { get; set; }
        public string unitname { get; set; }
        public Frm_tombdata()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 执行查询
        /// </summary>
        public void dosearch()
        {
            CriteriaOperator criteria = null;

            gridControl1.Visible = false;
            gridControl2.Visible = false;
            gridControl3.Visible = false;
            gridControl4.Visible = false;
            switch (action)
            {
                case "unsaled":  //待售墓位
                    criteria = CriteriaOperator.Parse("RG001 LIKE '" + (unitid == AppInfo.TOMB_ROOT_ID ? "%" : unitid) + "'" );
                    xpCollection_unsaled.Criteria = criteria;
                    xpCollection_unsaled.LoadingEnabled = true;
                    this.Text = "墓区数据-待售(" + unitname + ")";
                    gridControl1.Visible = true;                    
                    break;
                case "saled":    //已售墓位
                    criteria = CriteriaOperator.Parse("RG001 LIKE '" + (unitid == AppInfo.TOMB_ROOT_ID ? "%" : unitid) + "'");
                    xpCollection_saled.Criteria = criteria;
                    xpCollection_saled.LoadingEnabled = true;
                    this.Text = "墓区数据-已售(" + unitname + ")";
                    gridControl2.Visible = true;
                    break;
                case "debt":    //欠费数据
                    criteria = CriteriaOperator.Parse("RG001 LIKE '" + (unitid == AppInfo.TOMB_ROOT_ID ? "%" : unitid) + "'");
                    xpCollection_debt.Criteria = criteria;
                    xpCollection_debt.LoadingEnabled = true;
                    this.Text = "墓区数据-欠费(" + unitname + ")";
                    gridControl3.Visible = true;
                    break;
                case "bookin":  //预定数据
                    criteria = CriteriaOperator.Parse("RG001 LIKE '" + (unitid == AppInfo.TOMB_ROOT_ID ? "%" : unitid) + "'");
                    xpCollection_bookin.Criteria = criteria;
                    xpCollection_bookin.LoadingEnabled = true;
                    this.Text = "墓区数据-预订(" + unitname + ")";
                    gridControl4.Visible = true;
                    break;
            }
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

        private void gridView3_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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

        private void gridView4_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
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
    }
}