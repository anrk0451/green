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
using System.Net.Sockets;
using System.Diagnostics;
using DevExpress.XtraTab;
using Oracle.ManagedDataAccess.Client;
using green.BaseObject;
using DevExpress.XtraTab.ViewInfo;
using green.Form;
using DevExpress.XtraEditors;

namespace green
{     
    public partial class Frm_main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        [DllImport("user32.dll", EntryPoint = "FindWindow")]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);
        //[DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true, CharSet = CharSet.Auto)]
        //private static extern int SendMessage(IntPtr hwnd, uint wMsg, int wParam, int lParam);
  
        Process printprocess = new Process();                            //打印服务进程
        public static SocketClient socket = new SocketClient();

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
            printprocess.StartInfo.FileName = "pbnative.exe";
            printprocess.Start();
             
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
            if (!printprocess.HasExited) printprocess.Kill();
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

            Frm_login f_login = new Frm_login();
            f_login.ShowDialog();

            if (f_login.DialogResult == DialogResult.OK)  //登录成功处理..........
            {
                bs_user.Caption = Envior.cur_userName;
                bs_version.Caption = AppInfo.AppVersion;
                f_login.Dispose();
            }
            else
                return;
             
             
            //读取发票基础信息
            this.ReadInvoiceBaseInfo();

            //连接打印服务
            this.ConnectPrtServ();
        }

        /// <summary>
		/// 连接打印服务
		/// </summary>
		private void ConnectPrtServ()
        {
            IntPtr hwnd = FindWindow(null, "prtserv");
            if (hwnd != IntPtr.Zero)
            {
                Envior.prtservHandle = hwnd;
            }
            else
            {
                XtraMessageBox.Show("没有找到打印服务进程,不能打印!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            }
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
        /// <summary>
        /// 墓区结构维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("TombStructure");
        }
        /// <summary>
        /// 税票基础信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_taxBaseInfo frm_1 = new Frm_taxBaseInfo();
            frm_1.ShowDialog();
            frm_1.Dispose();
        }

        /// <summary>
		/// 读取发票信息
		/// </summary>
		private void ReadInvoiceBaseInfo()
        {
            OracleDataReader reader = SqlAssist.ExecuteReader("select * from sp01 where sp001  < '0000000100' ");
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    if (reader["SP002"].ToString() == "tax_no")                        //纳税人识别号
                        Envior.TAX_ID = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_addrtele")             //税票-销方地址电话
                        Envior.TAX_ADDR_TELE = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_bankaccount")          //税票-销方银行账号
                        Envior.TAX_BANK_ACCOUNT = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_appid")                //税票-应用账号
                        Envior.TAX_APPID = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_invoicetype")          //税票-发票类型
                        Envior.TAX_INVOICE_TYPE = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_server_url")           //税票-服务请求URL	
                        Envior.TAX_SERVER_URL = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_publickey")            //税票-公钥
                        Envior.TAX_PUBLIC_KEY = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_privatekey")           //税票-私钥
                        Envior.TAX_PRIVATE_KEY = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_cashier")              //税票-收款人
                        Envior.TAX_CASHIER = reader["SP005"].ToString();
                    else if (reader["SP002"].ToString() == "tax_checker")              //税票-复核人
                        Envior.TAX_CHECKER = reader["SP005"].ToString();
                    else if(reader["SP002"].ToString() == "tax_max_fee")
                    {
                        decimal dec_max;
                        if (decimal.TryParse(reader["SP006"].ToString(), out dec_max))
                            Envior.TAX_MAX_FEE = dec_max;
                        else
                            Envior.TAX_MAX_FEE = 0;
                    }
                         
                }
            }
            reader.Dispose();
        }
        /// <summary>
        /// 税票项目维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("TaxItem");
        }
        /// <summary>
        /// 收费项目维护
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("SalesItemInfo");
        }
        /// <summary>
        /// 购墓登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_checkin frm_checkin = new Frm_checkin();
            if(frm_checkin.ShowDialog() == DialogResult.OK)
            {

            }
            frm_checkin.Dispose();
        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("BusinessBrow");
        }

        /// <summary>
        /// 购墓预定登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_bookin frm_1 = new Frm_bookin();
            frm_1.ShowDialog();
            frm_1.Dispose();
        }

        private void barButtonItem18_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("BookinBrow");
        }
        /// <summary>
        /// 服务祭品
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_TempSales frm_1 = new Frm_TempSales();
            frm_1.ShowDialog();
            frm_1.Dispose();
        }

        private void barButtonItem20_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("TombData");
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("Report_cancel");
        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("Report_quit");
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("FinanceDaySearch");
        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            openBusinessObject("WorkStationList");
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            Frm_chgPwd frm_modify_pwd = new Frm_chgPwd();
            frm_modify_pwd.ShowDialog();
            frm_modify_pwd.Dispose();
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