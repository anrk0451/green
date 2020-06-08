using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace green.Misc
{
    class AppInfo
    {
        private static string _AppTitle = "公墓管理信息系统";       //应用标题
        private static string _AppVersion = "20.0114001";           //应用版本号
        private static string _UnitName = "牡丹江市龙凤公墓";       //使用单位    
        private static string _ROOTID = "0000000000";               //root用户Id
        private static string _ADMINGID = "0000000000";             //管理员组ID
        private static int _GRID_HEIGHT = 30;
        private static int _GRID_WIDTH = 40;
        private static string _TOMB_ROOT_ID = "0000000000";         //墓区结构顶级节点ID
 
        private static int _TAXITEMCOUNT = 8;                       //打印发票清单阈值
 

        public static string UnitName
        {
            get { return AppInfo._UnitName; }
        }

        public static string AppTitle
        {
            get { return _AppTitle; }
        }

        public static string AppVersion
        {
            get { return _AppVersion; }
        }

        public static string ROOTID
        {
            get { return _ROOTID; }
        }

        public static int TAXITEMCOUNT
        {
            get { return _TAXITEMCOUNT; }
        }

         
        public static int GRID_HEIGHT
        {
            get { return _GRID_HEIGHT; }
        }

        public static int GRID_WIDTH
        {
            get { return _GRID_WIDTH; }
        }

        public static string ADMINGID { get { return _ADMINGID; } }
        
        public static string TOMB_ROOT_ID
        {
            get { return _TOMB_ROOT_ID; }
        }
    }
}
