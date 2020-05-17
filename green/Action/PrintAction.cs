using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.Action
{
    class PrintAction
    {
        /// <summary>
        /// 打印墓位证书
        /// </summary>
        /// <param name="ac001"></param>
        public static void PrintCert(string ac001)
        {
            XtraMessageBox.Show("打印购墓证书!");
        }

        /// <summary>
        /// 打印管理费缴纳记录
        /// </summary>
        /// <param name="fa001"></param>
        public static void PrintPayRecord(string fa001)
        {
            XtraMessageBox.Show("打印缴费记录!");
        }
    }
}
