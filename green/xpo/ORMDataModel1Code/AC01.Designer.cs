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

    public partial class AC01 : XPLiteObject
    {
        string fAC001;
        [Key]
        [Size(10)]
        public string AC001
        {
            get { return fAC001; }
            set { SetPropertyValue<string>(nameof(AC001), ref fAC001, value); }
        }
        string fAC002;
        [Size(20)]
        public string AC002
        {
            get { return fAC002; }
            set { SetPropertyValue<string>(nameof(AC002), ref fAC002, value); }
        }
        string fAC003;
        [Indexed(Name = @"IDX_AC01_AC003")]
        [Size(50)]
        public string AC003
        {
            get { return fAC003; }
            set { SetPropertyValue<string>(nameof(AC003), ref fAC003, value); }
        }
        string fAC004;
        [Size(50)]
        public string AC004
        {
            get { return fAC004; }
            set { SetPropertyValue<string>(nameof(AC004), ref fAC004, value); }
        }
        string fAC005;
        public string AC005
        {
            get { return fAC005; }
            set { SetPropertyValue<string>(nameof(AC005), ref fAC005, value); }
        }
        string fAC010;
        [Size(10)]
        public string AC010
        {
            get { return fAC010; }
            set { SetPropertyValue<string>(nameof(AC010), ref fAC010, value); }
        }
        string fAC012;
        [Size(10)]
        public string AC012
        {
            get { return fAC012; }
            set { SetPropertyValue<string>(nameof(AC012), ref fAC012, value); }
        }
        string fAC015;
        [Indexed(Name = @"IDX_AC01_AC015")]
        [Size(10)]
        public string AC015
        {
            get { return fAC015; }
            set { SetPropertyValue<string>(nameof(AC015), ref fAC015, value); }
        }
        decimal fAC020;
        public decimal AC020
        {
            get { return fAC020; }
            set { SetPropertyValue<decimal>(nameof(AC020), ref fAC020, value); }
        }
        decimal fAC022;
        public decimal AC022
        {
            get { return fAC022; }
            set { SetPropertyValue<decimal>(nameof(AC022), ref fAC022, value); }
        }
        int fAC038;
        public int AC038
        {
            get { return fAC038; }
            set { SetPropertyValue<int>(nameof(AC038), ref fAC038, value); }
        }
        DateTime fAC040;
        public DateTime AC040
        {
            get { return fAC040; }
            set { SetPropertyValue<DateTime>(nameof(AC040), ref fAC040, value); }
        }
        char fAC042;
        public char AC042
        {
            get { return fAC042; }
            set { SetPropertyValue<char>(nameof(AC042), ref fAC042, value); }
        }
        string fAC048;
        [Size(10)]
        public string AC048
        {
            get { return fAC048; }
            set { SetPropertyValue<string>(nameof(AC048), ref fAC048, value); }
        }
        DateTime fAC049;
        public DateTime AC049
        {
            get { return fAC049; }
            set { SetPropertyValue<DateTime>(nameof(AC049), ref fAC049, value); }
        }
        string fAC100;
        [Size(10)]
        public string AC100
        {
            get { return fAC100; }
            set { SetPropertyValue<string>(nameof(AC100), ref fAC100, value); }
        }
        DateTime fAC200;
        public DateTime AC200
        {
            get { return fAC200; }
            set { SetPropertyValue<DateTime>(nameof(AC200), ref fAC200, value); }
        }
        string fAC250;
        [Size(200)]
        public string AC250
        {
            get { return fAC250; }
            set { SetPropertyValue<string>(nameof(AC250), ref fAC250, value); }
        }
        char fAC300;
        public char AC300
        {
            get { return fAC300; }
            set { SetPropertyValue<char>(nameof(AC300), ref fAC300, value); }
        }
        char fSTATUS;
        public char STATUS
        {
            get { return fSTATUS; }
            set { SetPropertyValue<char>(nameof(STATUS), ref fSTATUS, value); }
        }
        string fAC050;
        [Indexed(Name = @"IDX_AC01_AC050")]
        [Size(15)]
        public string AC050
        {
            get { return fAC050; }
            set { SetPropertyValue<string>(nameof(AC050), ref fAC050, value); }
        }
    }

}
