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

    public partial class FP02 : XPLiteObject
    {
        string fFP021;
        [Key]
        [Size(10)]
        public string FP021
        {
            get { return fFP021; }
            set { SetPropertyValue<string>(nameof(FP021), ref fFP021, value); }
        }
        string fSA001;
        [Indexed(Name = @"IDX_FP02_SA001")]
        [Size(10)]
        public string SA001
        {
            get { return fSA001; }
            set { SetPropertyValue<string>(nameof(SA001), ref fSA001, value); }
        }
        string fFP001;
        [Indexed(Name = @"IDX_FP02_FP001")]
        [Size(10)]
        public string FP001
        {
            get { return fFP001; }
            set { SetPropertyValue<string>(nameof(FP001), ref fFP001, value); }
        }
        decimal fFP022;
        public decimal FP022
        {
            get { return fFP022; }
            set { SetPropertyValue<decimal>(nameof(FP022), ref fFP022, value); }
        }
    }

}
