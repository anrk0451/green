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
	public partial class V_INVOICE_REPORT : XPLiteObject
	{
		string fINVOICECODE;
		[Size(50)]
		public string INVOICECODE
		{
			get { return fINVOICECODE; }
			set { SetPropertyValue<string>(nameof(INVOICECODE), ref fINVOICECODE, value); }
		}
		string fINVOICENUM;
		[Size(50)]
		public string INVOICENUM
		{
			get { return fINVOICENUM; }
			set { SetPropertyValue<string>(nameof(INVOICENUM), ref fINVOICENUM, value); }
		}
		string fINFOCLIENTNAME;
		public string INFOCLIENTNAME
		{
			get { return fINFOCLIENTNAME; }
			set { SetPropertyValue<string>(nameof(INFOCLIENTNAME), ref fINFOCLIENTNAME, value); }
		}
		decimal fJSHJ;
		public decimal JSHJ
		{
			get { return fJSHJ; }
			set { SetPropertyValue<decimal>(nameof(JSHJ), ref fJSHJ, value); }
		}
		DateTime fINVOICEDATE;
		public DateTime INVOICEDATE
		{
			get { return fINVOICEDATE; }
			set { SetPropertyValue<DateTime>(nameof(INVOICEDATE), ref fINVOICEDATE, value); }
		}
		string fWS003;
		[Size(50)]
		public string WS003
		{
			get { return fWS003; }
			set { SetPropertyValue<string>(nameof(WS003), ref fWS003, value); }
		}
		string fFA001;
		[Size(10)]
		public string FA001
		{
			get { return fFA001; }
			set { SetPropertyValue<string>(nameof(FA001), ref fFA001, value); }
		}
		string fWS001;
		[Size(10)]
		public string WS001
		{
			get { return fWS001; }
			set { SetPropertyValue<string>(nameof(WS001), ref fWS001, value); }
		}
		char fFLAG;
		public char FLAG
		{
			get { return fFLAG; }
			set { SetPropertyValue<char>(nameof(FLAG), ref fFLAG, value); }
		}
		string fINFOINVOICER;
		[Size(10)]
		public string INFOINVOICER
		{
			get { return fINFOINVOICER; }
			set { SetPropertyValue<string>(nameof(INFOINVOICER), ref fINFOINVOICER, value); }
		}
		string fKPR;
		[Size(4000)]
		public string KPR
		{
			get { return fKPR; }
			set { SetPropertyValue<string>(nameof(KPR), ref fKPR, value); }
		}
		DateTime fZFDATE;
		public DateTime ZFDATE
		{
			get { return fZFDATE; }
			set { SetPropertyValue<DateTime>(nameof(ZFDATE), ref fZFDATE, value); }
		}
		string fFP001;
		[Size(10)]
		public string FP001
		{
			get { return fFP001; }
			set { SetPropertyValue<string>(nameof(FP001), ref fFP001, value); }
		}
	}

}
