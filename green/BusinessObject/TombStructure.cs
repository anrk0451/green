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

namespace green.BusinessObject
{
    public partial class TombStructure : BaseBusiness
    {
        private DataTable gridTable = new DataTable("grid");
        private string curRegionId = string.Empty;
        private TG_ds tg_ds = new TG_ds();

        public TombStructure()
        {
            InitializeComponent();
        }

        private void TombStructure_Load(object sender, EventArgs e)
        {
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");

            tg_ds.Fill_Rg01();
            tg_ds.Fill_Bi01();
            treeList1.DataSource = tg_ds.dt_rg01;
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
                pictureBox1.Visible = true;
                splitContainerControl2.Visible = false;
                return;
            }
            else
            {
                pictureBox1.Visible = false;
                splitContainerControl2.Visible = true;

                curRegionId = e.Node.GetValue("RG001").ToString();
            }
            SplashScreenManager.ShowDefaultWaitForm("请等待", "处理中....");
            //DrawGrid(e.Node);
            SplashScreenManager.CloseDefaultWaitForm();
        }
    }
}
