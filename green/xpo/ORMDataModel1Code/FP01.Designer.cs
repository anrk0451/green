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

    public partial class FP01 : XPLiteObject
    {
        string fFP001;
        [Key]
        [Size(10)]
        public string FP001
        {
            get { return fFP001; }
            set { SetPropertyValue<string>(nameof(FP001), ref fFP001, value); }
        }
        string fFA001;
        [Indexed(Name = @"IDX_FP01_FA001")]
        [Size(10)]
        public string FA001
        {
            get { return fFA001; }
            set { SetPropertyValue<string>(nameof(FA001), ref fFA001, value); }
        }
        string fINFOKIND;
        [Size(3)]
        public string INFOKIND
        {
            get { return fINFOKIND; }
            set { SetPropertyValue<string>(nameof(INFOKIND), ref fINFOKIND, value); }
        }
        string fINFOINVOICER;
        [Size(10)]
        public string INFOINVOICER
        {
            get { return fINFOINVOICER; }
            set { SetPropertyValue<string>(nameof(INFOINVOICER), ref fINFOINVOICER, value); }
        }
        string fINFOCASHIER;
        [Size(50)]
        public string INFOCASHIER
        {
            get { return fINFOCASHIER; }
            set { SetPropertyValue<string>(nameof(INFOCASHIER), ref fINFOCASHIER, value); }
        }
        string fINFOCHECKER;
        [Size(50)]
        public string INFOCHECKER
        {
            get { return fINFOCHECKER; }
            set { SetPropertyValue<string>(nameof(INFOCHECKER), ref fINFOCHECKER, value); }
        }
        DateTime fINVOICEDATE;
        public DateTime INVOICEDATE
        {
            get { return fINVOICEDATE; }
            set { SetPropertyValue<DateTime>(nameof(INVOICEDATE), ref fINVOICEDATE, value); }
        }
        string fINFOCLIENTNAME;
        public string INFOCLIENTNAME
        {
            get { return fINFOCLIENTNAME; }
            set { SetPropertyValue<string>(nameof(INFOCLIENTNAME), ref fINFOCLIENTNAME, value); }
        }
        string fINFOCLIENTTAXCODE;
        [Size(50)]
        public string INFOCLIENTTAXCODE
        {
            get { return fINFOCLIENTTAXCODE; }
            set { SetPropertyValue<string>(nameof(INFOCLIENTTAXCODE), ref fINFOCLIENTTAXCODE, value); }
        }
        string fINFOCLIENTBANKACCOUNT;
        public string INFOCLIENTBANKACCOUNT
        {
            get { return fINFOCLIENTBANKACCOUNT; }
            set { SetPropertyValue<string>(nameof(INFOCLIENTBANKACCOUNT), ref fINFOCLIENTBANKACCOUNT, value); }
        }
        string fINFOCLIENTADDRESSPHONE;
        public string INFOCLIENTADDRESSPHONE
        {
            get { return fINFOCLIENTADDRESSPHONE; }
            set { SetPropertyValue<string>(nameof(INFOCLIENTADDRESSPHONE), ref fINFOCLIENTADDRESSPHONE, value); }
        }
        string fINVOICECODE;
        [Size(50)]
        public string INVOICECODE
        {
            get { return fINVOICECODE; }
            set { SetPropertyValue<string>(nameof(INVOICECODE), ref fINVOICECODE, value); }
        }
        string fINVOICENUM;
        [Indexed(Name = @"IDX_FP01_INVNU")]
        [Size(50)]
        public string INVOICENUM
        {
            get { return fINVOICENUM; }
            set { SetPropertyValue<string>(nameof(INVOICENUM), ref fINVOICENUM, value); }
        }
        string fMW;
        public string MW
        {
            get { return fMW; }
            set { SetPropertyValue<string>(nameof(MW), ref fMW, value); }
        }
        string fJYM;
        [Size(50)]
        public string JYM
        {
            get { return fJYM; }
            set { SetPropertyValue<string>(nameof(JYM), ref fJYM, value); }
        }
        decimal fHJJE;
        public decimal HJJE
        {
            get { return fHJJE; }
            set { SetPropertyValue<decimal>(nameof(HJJE), ref fHJJE, value); }
        }
        decimal fJSHJ;
        public decimal JSHJ
        {
            get { return fJSHJ; }
            set { SetPropertyValue<decimal>(nameof(JSHJ), ref fJSHJ, value); }
        }
        char fFLAG;
        [Indexed(Name = @"IDX_FP01")]
        public char FLAG
        {
            get { return fFLAG; }
            set { SetPropertyValue<char>(nameof(FLAG), ref fFLAG, value); }
        }
        string fZFR;
        [Size(20)]
        public string ZFR
        {
            get { return fZFR; }
            set { SetPropertyValue<string>(nameof(ZFR), ref fZFR, value); }
        }
        string fZFREASON;
        [Size(50)]
        public string ZFREASON
        {
            get { return fZFREASON; }
            set { SetPropertyValue<string>(nameof(ZFREASON), ref fZFREASON, value); }
        }
        DateTime fZFDATE;
        public DateTime ZFDATE
        {
            get { return fZFDATE; }
            set { SetPropertyValue<DateTime>(nameof(ZFDATE), ref fZFDATE, value); }
        }
        string fORI_INVOICECODE;
        [Size(50)]
        public string ORI_INVOICECODE
        {
            get { return fORI_INVOICECODE; }
            set { SetPropertyValue<string>(nameof(ORI_INVOICECODE), ref fORI_INVOICECODE, value); }
        }
        string fORI_INVOICENUM;
        [Size(50)]
        public string ORI_INVOICENUM
        {
            get { return fORI_INVOICENUM; }
            set { SetPropertyValue<string>(nameof(ORI_INVOICENUM), ref fORI_INVOICENUM, value); }
        }
        string fWS001;
        [Size(10)]
        public string WS001
        {
            get { return fWS001; }
            set { SetPropertyValue<string>(nameof(WS001), ref fWS001, value); }
        }
    }

}
