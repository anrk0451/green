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

    public partial class TU01 : XPLiteObject
    {
        string fTU001;
        [Key]
        [Size(10)]
        public string TU001
        {
            get { return fTU001; }
            set { SetPropertyValue<string>(nameof(TU001), ref fTU001, value); }
        }
        string fTU003;
        [Indexed(Name = @"IDX_TU01_NAME")]
        [Size(200)]
        public string TU003
        {
            get { return fTU003; }
            set { SetPropertyValue<string>(nameof(TU003), ref fTU003, value); }
        }
        string fTU005;
        [Size(50)]
        public string TU005
        {
            get { return fTU005; }
            set { SetPropertyValue<string>(nameof(TU005), ref fTU005, value); }
        }
        string fTU006;
        [Size(200)]
        public string TU006
        {
            get { return fTU006; }
            set { SetPropertyValue<string>(nameof(TU006), ref fTU006, value); }
        }
        string fTU007;
        [Size(200)]
        public string TU007
        {
            get { return fTU007; }
            set { SetPropertyValue<string>(nameof(TU007), ref fTU007, value); }
        }
    }

}