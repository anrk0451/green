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

    public partial class SA01 : XPLiteObject
    {
        string fSA001;
        [Key]
        [Size(10)]
        public string SA001
        {
            get { return fSA001; }
            set { SetPropertyValue<string>(nameof(SA001), ref fSA001, value); }
        }
        string fAC001;
        [Indexed(Name = @"IDX_SA01_AC001")]
        [Size(10)]
        public string AC001
        {
            get { return fAC001; }
            set { SetPropertyValue<string>(nameof(AC001), ref fAC001, value); }
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
        string fSA004;
        [Size(10)]
        public string SA004
        {
            get { return fSA004; }
            set { SetPropertyValue<string>(nameof(SA004), ref fSA004, value); }
        }
        char fSA005;
        public char SA005
        {
            get { return fSA005; }
            set { SetPropertyValue<char>(nameof(SA005), ref fSA005, value); }
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
        decimal fSA006;
        public decimal SA006
        {
            get { return fSA006; }
            set { SetPropertyValue<decimal>(nameof(SA006), ref fSA006, value); }
        }
        char fSA008;
        public char SA008
        {
            get { return fSA008; }
            set { SetPropertyValue<char>(nameof(SA008), ref fSA008, value); }
        }
        string fSA010;
        [Indexed(Name = @"IDX_SA01_SA010")]
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
        decimal fSA025;
        public decimal SA025
        {
            get { return fSA025; }
            set { SetPropertyValue<decimal>(nameof(SA025), ref fSA025, value); }
        }
        string fSA100;
        [Size(10)]
        public string SA100
        {
            get { return fSA100; }
            set { SetPropertyValue<string>(nameof(SA100), ref fSA100, value); }
        }
        DateTime fSA200;
        public DateTime SA200
        {
            get { return fSA200; }
            set { SetPropertyValue<DateTime>(nameof(SA200), ref fSA200, value); }
        }
        char fSTATUS;
        [Indexed(Name = @"IDX_SA01_STATUS")]
        public char STATUS
        {
            get { return fSTATUS; }
            set { SetPropertyValue<char>(nameof(STATUS), ref fSTATUS, value); }
        }
    }

}
