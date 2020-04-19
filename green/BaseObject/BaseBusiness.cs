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

namespace green.BaseObject
{
    public partial class BaseBusiness : DevExpress.XtraEditors.XtraUserControl
    {
        public Dictionary<string, object> swapdata { get; set; }    //交换数据	 

        public BaseBusiness()
        {
            swapdata = new Dictionary<string, object>();
            InitializeComponent();
        }

        public virtual void Business_Init()
        {

        }
    }
}
