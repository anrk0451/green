using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Runtime.InteropServices;
using DevExpress.XtraSplashScreen;
using System.Threading;
using green.Misc;
using System.Diagnostics;
using DevExpress.XtraTab;
using Oracle.ManagedDataAccess.Client;
using green.BaseObject;
using DevExpress.XtraTab.ViewInfo;

namespace green
{     
    public partial class Frm_main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
 
        
        Process printprocess = new Process();                            //打印服务进程
        public Dictionary<string, Object> swapdata { get; set; }         //交换数据对象


        //追踪已经打开的Tab页
        private Dictionary<string, Bo01> businessTab = new Dictionary<string, Bo01>();
        private Dictionary<string, XtraTabPage> openedTabPage = new Dictionary<string, XtraTabPage>();
        private DataTable dt_bo01 = new DataTable("BO01");				 //存放业务对象表

        public Frm_main()
        {
            SplashScreenManager.ShowForm(typeof(SplashScreen1));
            Thread.Sleep(2000);
            SplashScreenManager.CloseForm();

            InitializeComponent();

            Envior.mform = this;
            swapdata = new Dictionary<string, object>();

            //启动打印服务进程
            //printprocess.StartInfo.FileName = "pbnative.exe";
            //printprocess.Start();
             
        }

        /// <summary>
        /// 主窗体关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_main_FormClosed(object sender, FormClosedEventArgs e)
        {
            //销毁业务对象表
            dt_bo01.Dispose();

            //断开数据库
            SqlAssist.DisConnect();
 
            //关闭关联的打印进程
            //if (!printprocess.HasExited) printprocess.Kill();
 
        }

        /// <summary>
        /// 主窗口装入事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Frm_main_Load(object sender, EventArgs e)
        {
            //读取业务对象
            OracleDataAdapter bo01Adapter = SqlAssist.getSingleTableAdapter("select * from bo01");
            bo01Adapter.Fill(dt_bo01);

            List<Bo01> bo01_rows = ModelHelper.TableToEntity<Bo01>(dt_bo01);
            businessTab = bo01_rows.ToDictionary(key => key.bo001, value => value);

             
        }

        /// <summary>
        /// 打开业务对象
        /// </summary>
        /// <param name="bo001"></param>
        public void openBusinessObject(string bo001)
        {
            openBusinessObject(bo001, null);
        }


        /// <summary>
		/// 打开业务对象(如果没有则创建)
		/// </summary>
		public void openBusinessObject(string bo001, object parm)
        {
            if (openedTabPage.ContainsKey(bo001))
            {
                xtraTabControl1.SelectedTabPage = openedTabPage[bo001];
                if (parm != null)
                {
                    foreach (Control control in openedTabPage[bo001].Controls)
                    {
                        if (control is BaseBusiness)
                        {
                            ((BaseBusiness)control).swapdata["parm"] = parm;
                            ((BaseBusiness)control).Business_Init();
                            return;
                        }
                    }
                }
            }
            else //如果尚未打开，则new
            {
                XtraTabPage newPage = new XtraTabPage();
                newPage.Text = businessTab[bo001].bo003;
                newPage.Tag = bo001;
                newPage.ShowCloseButton = DevExpress.Utils.DefaultBoolean.True;

                BaseBusiness bo = (BaseBusiness)Activator.CreateInstance(Type.GetType("green.BusinessObject." + bo001));

                Envior.mform = this;

                bo.Dock = DockStyle.Fill;
                bo.Parent = newPage;
                bo.swapdata.Add("parm", parm);

                newPage.Controls.Add(bo);

                xtraTabControl1.TabPages.Add(newPage);
                xtraTabControl1.SelectedTabPage = newPage;

                bo.Business_Init();

                ////////登记已打开 Tabpage ////////
                openedTabPage.Add(bo001, newPage);

            }
        }

        /// <summary>
        /// 标签页关闭事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void xtraTabControl1_CloseButtonClick(object sender, EventArgs e)
        {
            ClosePageButtonEventArgs arg = e as ClosePageButtonEventArgs;

            XtraTabPage curPage = (XtraTabPage)arg.Page;
            ///////// 清除页面追踪 ////////
            openedTabPage.Remove(curPage.Tag.ToString());

            curPage.Dispose();
        }

        /// <summary>
        /// 用户管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("Operators");
        }
        
        private void barButtonItem10_ItemClick(object sender, ItemClickEventArgs e)
        {
            
        }
        /// <summary>
        /// 角色管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("Roles");
        }
        /// <summary>
        /// 数据项目维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem11_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("DataDict");
        }
    }

    /// <summary>
    /// 业务对象
    /// </summary>
    class Bo01
    {
        public string bo001 { get; set; }   //业务编号

        public string bo003 { get; set; }   //业务名称
        public string bo004 { get; set; }   //业务对象类型 w-窗口 x-xtratabpage对象
    }

}