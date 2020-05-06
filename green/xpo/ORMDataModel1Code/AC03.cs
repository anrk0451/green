using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace green.xpo.orcl
{

    public partial class AC03
    {
        public AC03(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
    }

}
