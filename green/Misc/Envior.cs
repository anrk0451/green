using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.Misc
{
    class Envior
    {
        public static Frm_main mform { get; set; }          //当前主窗口
        public static string cur_userId { get; set; }       //当前登录用户Id
        public static string cur_userName { get; set; }     //当前登录用户名
        
  
        public static string NEXT_BILL_CODE { get; set; }     //下张发票代码
        public static string NEXT_BILL_NUM { get; set; }      //下张发票票号
        public static string TAX_ID { get; set; }             //纳税识别号
        public static string TAX_ADDR_TELE { get; set; }      //税务-销方地址电话
        public static string TAX_BANK_ACCOUNT { get; set; }   //税务-销方银行&账号
        public static string TAX_APPID { get; set; }          //税务 appId
        public static string TAX_INVOICE_TYPE { get; set; }   //发票类型
        public static string TAX_PUBLIC_KEY { get; set; }     //公钥
        public static string TAX_PRIVATE_KEY { get; set; }    //私钥
        public static string TAX_SERVER_URL { get; set; }     //税务发票服务URL  
        public static string TAX_CASHIER { get; set; }        //税务发票-收款人
        public static string TAX_CHECKER { get; set; }        //税务发票-复核人
        public static decimal TAX_MAX_FEE { get; set; }       //税务发票-单张最大面额  

        public static string WORKSTATIONID { get; set; }      //工作站ID

 
        public static char loginMode { get; set; }            //登陆模式
 
        public static bool canInvoice { get; set; }		      //当前的用户允许开发票
 
         
    }
}
