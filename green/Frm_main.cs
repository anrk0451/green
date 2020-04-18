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
            dt_bo01.Dispose();

            //断开数据库
            SqlAssist.DisConnect();
 
            //关闭关联的打印进程
            if (!printprocess.HasExited) printprocess.Kill();
 
        }
    }
}