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

    [Persistent(@"LINDIAN.SALES")]
    public partial class LINDIAN_SALES : XPLiteObject
    {
        int fJYLSH;
        public int JYLSH
        {
            get { return fJYLSH; }
            set { SetPropertyValue<int>(nameof(JYLSH), ref fJYLSH, value); }
        }
        int fWARE_ID;
        public int WARE_ID
        {
            get { return fWARE_ID; }
            set { SetPropertyValue<int>(nameof(WARE_ID), ref fWARE_ID, value); }
        }
        string fSALESTYPE;
        [Indexed(Name = @"IDX_SALES_SALESTYPE")]
        [Size(3)]
        public string SALESTYPE
        {
            get { return fSALESTYPE; }
            set { SetPropertyValue<string>(nameof(SALESTYPE), ref fSALESTYPE, value); }
        }
        string fSALESFLAG;
        [Size(3)]
        public string SALESFLAG
        {
            get { return fSALESFLAG; }
            set { SetPropertyValue<string>(nameof(SALESFLAG), ref fSALESFLAG, value); }
        }
        int fBOOKIN_ID;
        [Indexed(Name = @"IDX_SALES_BOOKIN_ID")]
        public int BOOKIN_ID
        {
            get { return fBOOKIN_ID; }
            set { SetPropertyValue<int>(nameof(BOOKIN_ID), ref fBOOKIN_ID, value); }
        }
        string fBUYER;
        public string BUYER
        {
            get { return fBUYER; }
            set { SetPropertyValue<string>(nameof(BUYER), ref fBUYER, value); }
        }
        decimal fNUMS;
        public decimal NUMS
        {
            get { return fNUMS; }
            set { SetPropertyValue<decimal>(nameof(NUMS), ref fNUMS, value); }
        }
        decimal fPRICE;
        public decimal PRICE
        {
            get { return fPRICE; }
            set { SetPropertyValue<decimal>(nameof(PRICE), ref fPRICE, value); }
        }
        DateTime fUSEBEGIN;
        public DateTime USEBEGIN
        {
            get { return fUSEBEGIN; }
            set { SetPropertyValue<DateTime>(nameof(USEBEGIN), ref fUSEBEGIN, value); }
        }
        DateTime fUSEEND;
        public DateTime USEEND
        {
            get { return fUSEEND; }
            set { SetPropertyValue<DateTime>(nameof(USEEND), ref fUSEEND, value); }
        }
        int fJBR;
        public int JBR
        {
            get { return fJBR; }
            set { SetPropertyValue<int>(nameof(JBR), ref fJBR, value); }
        }
        DateTime fJBRQ;
        public DateTime JBRQ
        {
            get { return fJBRQ; }
            set { SetPropertyValue<DateTime>(nameof(JBRQ), ref fJBRQ, value); }
        }
        int fJSLSH;
        [Indexed(Name = @"IDX_SALES_JSLSH")]
        public int JSLSH
        {
            get { return fJSLSH; }
            set { SetPropertyValue<int>(nameof(JSLSH), ref fJSLSH, value); }
        }
        string fWAREALIAS;
        [Size(50)]
        public string WAREALIAS
        {
            get { return fWAREALIAS; }
            set { SetPropertyValue<string>(nameof(WAREALIAS), ref fWAREALIAS, value); }
        }
        DateTime fJSSJ;
        public DateTime JSSJ
        {
            get { return fJSSJ; }
            set { SetPropertyValue<DateTime>(nameof(JSSJ), ref fJSSJ, value); }
        }
        int fJSR;
        public int JSR
        {
            get { return fJSR; }
            set { SetPropertyValue<int>(nameof(JSR), ref fJSR, value); }
        }
        decimal fORIPRICE;
        public decimal ORIPRICE
        {
            get { return fORIPRICE; }
            set { SetPropertyValue<decimal>(nameof(ORIPRICE), ref fORIPRICE, value); }
        }
        string fWARE_TYPE;
        [Size(20)]
        public string WARE_TYPE
        {
            get { return fWARE_TYPE; }
            set { SetPropertyValue<string>(nameof(WARE_TYPE), ref fWARE_TYPE, value); }
        }
        int fDATA1;
        public int DATA1
        {
            get { return fDATA1; }
            set { SetPropertyValue<int>(nameof(DATA1), ref fDATA1, value); }
        }
        string fDATA2;
        [Size(50)]
        public string DATA2
        {
            get { return fDATA2; }
            set { SetPropertyValue<string>(nameof(DATA2), ref fDATA2, value); }
        }
        public struct CompoundKey1Struct
        {
            [Persistent("SALESERIAL")]
            public int SALESERIAL { get; set; }
        }
        [Key, Persistent]
        public CompoundKey1Struct CompoundKey1;
    }

}