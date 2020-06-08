using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace green.xpo.orcl
{

    public partial class V_REPORT_TOMBDATA_BOOKIN
    {
        public V_REPORT_TOMBDATA_BOOKIN(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
