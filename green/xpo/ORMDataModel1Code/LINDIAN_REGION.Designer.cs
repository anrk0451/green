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

    [Persistent(@"LINDIAN.REGION")]
    public partial class LINDIAN_REGION : XPLiteObject
    {
        int fROOMID;
        public int ROOMID
        {
            get { return fROOMID; }
            set { SetPropertyValue<int>(nameof(ROOMID), ref fROOMID, value); }
        }
        int fLAYERCOUNT;
        public int LAYERCOUNT
        {
            get { return fLAYERCOUNT; }
            set { SetPropertyValue<int>(nameof(LAYERCOUNT), ref fLAYERCOUNT, value); }
        }
        int fBEGINBIT;
        public int BEGINBIT
        {
            get { return fBEGINBIT; }
            set { SetPropertyValue<int>(nameof(BEGINBIT), ref fBEGINBIT, value); }
        }
        int fENDBIT;
        public int ENDBIT
        {
            get { return fENDBIT; }
            set { SetPropertyValue<int>(nameof(ENDBIT), ref fENDBIT, value); }
        }
        int fFIRSTLAYERBITS;
        public int FIRSTLAYERBITS
        {
            get { return fFIRSTLAYERBITS; }
            set { SetPropertyValue<int>(nameof(FIRSTLAYERBITS), ref fFIRSTLAYERBITS, value); }
        }
        string fREGIONNAME;
        [Size(50)]
        public string REGIONNAME
        {
            get { return fREGIONNAME; }
            set { SetPropertyValue<string>(nameof(REGIONNAME), ref fREGIONNAME, value); }
        }
        public struct CompoundKey1Struct
        {
            [Persistent("REGIONID")]
            public int REGIONID { get; set; }
        }
        [Key, Persistent]
        public CompoundKey1Struct CompoundKey1;
    }

}
