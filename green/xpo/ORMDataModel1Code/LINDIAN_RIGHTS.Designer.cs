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

    [Persistent(@"LINDIAN.RIGHTS")]
    public partial class LINDIAN_RIGHTS : XPLiteObject
    {
        string fRIGHTS;
        [Size(3)]
        public string RIGHTS
        {
            get { return fRIGHTS; }
            set { SetPropertyValue<string>(nameof(RIGHTS), ref fRIGHTS, value); }
        }
        public struct CompoundKey1Struct
        {
            [Persistent("FUN_ID")]
            public int FUN_ID { get; set; }
            [Persistent("OPERATOR_ID")]
            public int OPERATOR_ID { get; set; }
        }
        [Key, Persistent]
        public CompoundKey1Struct CompoundKey1;
    }

}