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

    [NonPersistent]
    public partial class V_FINDETAIL : XPLiteObject
    {
        string fSA001;
        [Size(10)]
        public string SA001
        {
            get { return fSA001; }
            set { SetPropertyValue<string>(nameof(SA001), ref fSA001, value); }
        }
        char fSA002;
        public char SA002
        {
            get { return fSA002; }
            set { SetPropertyValue<char>(nameof(SA002), ref fSA002, value); }
        }
        string fSA003;
        [Size(50)]
        public string SA003
        {
            get { return fSA003; }
            set { SetPropertyValue<string>(nameof(SA003), ref fSA003, value); }
        }
        decimal fPRICE;
        public decimal PRICE
        {
            get { return fPRICE; }
            set { SetPropertyValue<decimal>(nameof(PRICE), ref fPRICE, value); }
        }
        decimal fNUMS;
        public decimal NUMS
        {
            get { return fNUMS; }
            set { SetPropertyValue<decimal>(nameof(NUMS), ref fNUMS, value); }
        }
        decimal fSA007;
        public decimal SA007
        {
            get { return fSA007; }
            set { SetPropertyValue<decimal>(nameof(SA007), ref fSA007, value); }
        }
        string fSA010;
        [Size(10)]
        public string SA010
        {
            get { return fSA010; }
            set { SetPropertyValue<string>(nameof(SA010), ref fSA010, value); }
        }
        char fSA020;
        public char SA020
        {
            get { return fSA020; }
            set { SetPropertyValue<char>(nameof(SA020), ref fSA020, value); }
        }
    }

}
