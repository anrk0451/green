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

    public partial class V_REPORT_TOMBQUIT : XPLiteObject
    {
        string fAC001;
        [Key]
        [Size(10)]
        public string AC001
        {
            get { return fAC001; }
            set { SetPropertyValue<string>(nameof(AC001), ref fAC001, value); }
        }
        string fAC003;
        [Size(50)]
        public string AC003
        {
            get { return fAC003; }
            set { SetPropertyValue<string>(nameof(AC003), ref fAC003, value); }
        }
        string fPOSITION;
        [Size(4000)]
        public string POSITION
        {
            get { return fPOSITION; }
            set { SetPropertyValue<string>(nameof(POSITION), ref fPOSITION, value); }
        }
        DateTime fAC049;
        public DateTime AC049
        {
            get { return fAC049; }
            set { SetPropertyValue<DateTime>(nameof(AC049), ref fAC049, value); }
        }
        string fQT003;
        public string QT003
        {
            get { return fQT003; }
            set { SetPropertyValue<string>(nameof(QT003), ref fQT003, value); }
        }
        string fQT100;
        [Size(4000)]
        public string QT100
        {
            get { return fQT100; }
            set { SetPropertyValue<string>(nameof(QT100), ref fQT100, value); }
        }
        DateTime fQT200;
        public DateTime QT200
        {
            get { return fQT200; }
            set { SetPropertyValue<DateTime>(nameof(QT200), ref fQT200, value); }
        }
    }

}
