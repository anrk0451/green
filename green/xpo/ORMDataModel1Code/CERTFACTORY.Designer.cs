﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
namespace green.xpo.orcl
{

    public partial class CERTFACTORY : XPLiteObject
    {
        DateTime fCURDATE;
        [Key]
        public DateTime CURDATE
        {
            get { return fCURDATE; }
            set { SetPropertyValue<DateTime>(nameof(CURDATE), ref fCURDATE, value); }
        }
        int fCURNUM;
        public int CURNUM
        {
            get { return fCURNUM; }
            set { SetPropertyValue<int>(nameof(CURNUM), ref fCURNUM, value); }
        }
    }

}
