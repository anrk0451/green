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

    public partial class SP01 : XPLiteObject
    {
        string fSP001;
        [Key]
        [Size(10)]
        public string SP001
        {
            get { return fSP001; }
            set { SetPropertyValue<string>(nameof(SP001), ref fSP001, value); }
        }
        string fSP002;
        [Size(50)]
        public string SP002
        {
            get { return fSP002; }
            set { SetPropertyValue<string>(nameof(SP002), ref fSP002, value); }
        }
        string fSP003;
        [Size(50)]
        public string SP003
        {
            get { return fSP003; }
            set { SetPropertyValue<string>(nameof(SP003), ref fSP003, value); }
        }
        string fSP005;
        [Size(200)]
        public string SP005
        {
            get { return fSP005; }
            set { SetPropertyValue<string>(nameof(SP005), ref fSP005, value); }
        }
        decimal fSP006;
        public decimal SP006
        {
            get { return fSP006; }
            set { SetPropertyValue<decimal>(nameof(SP006), ref fSP006, value); }
        }
    }

}
