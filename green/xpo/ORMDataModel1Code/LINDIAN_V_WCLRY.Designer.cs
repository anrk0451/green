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
    public partial class LINDIAN_V_WCLRY : XPLiteObject
    {
        int fBOOKIN_ID;
        public int BOOKIN_ID
        {
            get { return fBOOKIN_ID; }
            set { SetPropertyValue<int>(nameof(BOOKIN_ID), ref fBOOKIN_ID, value); }
        }
        string fREGISTER_ID;
        [Size(20)]
        public string REGISTER_ID
        {
            get { return fREGISTER_ID; }
            set { SetPropertyValue<string>(nameof(REGISTER_ID), ref fREGISTER_ID, value); }
        }
        int fJYLSH;
        public int JYLSH
        {
            get { return fJYLSH; }
            set { SetPropertyValue<int>(nameof(JYLSH), ref fJYLSH, value); }
        }
        string fP_NAME;
        [Size(20)]
        public string P_NAME
        {
            get { return fP_NAME; }
            set { SetPropertyValue<string>(nameof(P_NAME), ref fP_NAME, value); }
        }
        char fP_SEX;
        public char P_SEX
        {
            get { return fP_SEX; }
            set { SetPropertyValue<char>(nameof(P_SEX), ref fP_SEX, value); }
        }
        int fP_AGE;
        public int P_AGE
        {
            get { return fP_AGE; }
            set { SetPropertyValue<int>(nameof(P_AGE), ref fP_AGE, value); }
        }
        string fP_NAME2;
        [Size(20)]
        public string P_NAME2
        {
            get { return fP_NAME2; }
            set { SetPropertyValue<string>(nameof(P_NAME2), ref fP_NAME2, value); }
        }
        char fP_SEX2;
        public char P_SEX2
        {
            get { return fP_SEX2; }
            set { SetPropertyValue<char>(nameof(P_SEX2), ref fP_SEX2, value); }
        }
        int fP_AGE2;
        public int P_AGE2
        {
            get { return fP_AGE2; }
            set { SetPropertyValue<int>(nameof(P_AGE2), ref fP_AGE2, value); }
        }
        string fP_REASON;
        [Size(50)]
        public string P_REASON
        {
            get { return fP_REASON; }
            set { SetPropertyValue<string>(nameof(P_REASON), ref fP_REASON, value); }
        }
        string fP_HANDLE;
        [Size(50)]
        public string P_HANDLE
        {
            get { return fP_HANDLE; }
            set { SetPropertyValue<string>(nameof(P_HANDLE), ref fP_HANDLE, value); }
        }
        string fREGION;
        [Size(50)]
        public string REGION
        {
            get { return fREGION; }
            set { SetPropertyValue<string>(nameof(REGION), ref fREGION, value); }
        }
        string fP_ADDRESS;
        public string P_ADDRESS
        {
            get { return fP_ADDRESS; }
            set { SetPropertyValue<string>(nameof(P_ADDRESS), ref fP_ADDRESS, value); }
        }
        DateTime fHHSJ;
        public DateTime HHSJ
        {
            get { return fHHSJ; }
            set { SetPropertyValue<DateTime>(nameof(HHSJ), ref fHHSJ, value); }
        }
        DateTime fDIEDATE;
        public DateTime DIEDATE
        {
            get { return fDIEDATE; }
            set { SetPropertyValue<DateTime>(nameof(DIEDATE), ref fDIEDATE, value); }
        }
        string fDIEADDRESS;
        public string DIEADDRESS
        {
            get { return fDIEADDRESS; }
            set { SetPropertyValue<string>(nameof(DIEADDRESS), ref fDIEADDRESS, value); }
        }
        DateTime fGETTOTIME;
        public DateTime GETTOTIME
        {
            get { return fGETTOTIME; }
            set { SetPropertyValue<DateTime>(nameof(GETTOTIME), ref fGETTOTIME, value); }
        }
        DateTime fCLSJ;
        public DateTime CLSJ
        {
            get { return fCLSJ; }
            set { SetPropertyValue<DateTime>(nameof(CLSJ), ref fCLSJ, value); }
        }
        DateTime fGBSJ;
        public DateTime GBSJ
        {
            get { return fGBSJ; }
            set { SetPropertyValue<DateTime>(nameof(GBSJ), ref fGBSJ, value); }
        }
        string fZCR;
        [Size(20)]
        public string ZCR
        {
            get { return fZCR; }
            set { SetPropertyValue<string>(nameof(ZCR), ref fZCR, value); }
        }
        DateTime fKGSJ;
        public DateTime KGSJ
        {
            get { return fKGSJ; }
            set { SetPropertyValue<DateTime>(nameof(KGSJ), ref fKGSJ, value); }
        }
        string fMEMO;
        [Size(250)]
        public string MEMO
        {
            get { return fMEMO; }
            set { SetPropertyValue<string>(nameof(MEMO), ref fMEMO, value); }
        }
        string fLINKER;
        [Size(20)]
        public string LINKER
        {
            get { return fLINKER; }
            set { SetPropertyValue<string>(nameof(LINKER), ref fLINKER, value); }
        }
        string fRELATION;
        [Size(50)]
        public string RELATION
        {
            get { return fRELATION; }
            set { SetPropertyValue<string>(nameof(RELATION), ref fRELATION, value); }
        }
        string fREGION2;
        [Size(50)]
        public string REGION2
        {
            get { return fREGION2; }
            set { SetPropertyValue<string>(nameof(REGION2), ref fREGION2, value); }
        }
        string fLINKER_ADDRESS;
        public string LINKER_ADDRESS
        {
            get { return fLINKER_ADDRESS; }
            set { SetPropertyValue<string>(nameof(LINKER_ADDRESS), ref fLINKER_ADDRESS, value); }
        }
        string fPHONE;
        [Size(20)]
        public string PHONE
        {
            get { return fPHONE; }
            set { SetPropertyValue<string>(nameof(PHONE), ref fPHONE, value); }
        }
        string fPTYPE;
        [Size(3)]
        public string PTYPE
        {
            get { return fPTYPE; }
            set { SetPropertyValue<string>(nameof(PTYPE), ref fPTYPE, value); }
        }
        char fBALANCE;
        public char BALANCE
        {
            get { return fBALANCE; }
            set { SetPropertyValue<char>(nameof(BALANCE), ref fBALANCE, value); }
        }
        char fBOOKINFLAG;
        public char BOOKINFLAG
        {
            get { return fBOOKINFLAG; }
            set { SetPropertyValue<char>(nameof(BOOKINFLAG), ref fBOOKINFLAG, value); }
        }
        int fHALL_ID;
        public int HALL_ID
        {
            get { return fHALL_ID; }
            set { SetPropertyValue<int>(nameof(HALL_ID), ref fHALL_ID, value); }
        }
        int fFLOOR_ID;
        public int FLOOR_ID
        {
            get { return fFLOOR_ID; }
            set { SetPropertyValue<int>(nameof(FLOOR_ID), ref fFLOOR_ID, value); }
        }
        int fROOM_ID;
        public int ROOM_ID
        {
            get { return fROOM_ID; }
            set { SetPropertyValue<int>(nameof(ROOM_ID), ref fROOM_ID, value); }
        }
        int fBIT_ID;
        public int BIT_ID
        {
            get { return fBIT_ID; }
            set { SetPropertyValue<int>(nameof(BIT_ID), ref fBIT_ID, value); }
        }
        string fPOSITION;
        public string POSITION
        {
            get { return fPOSITION; }
            set { SetPropertyValue<string>(nameof(POSITION), ref fPOSITION, value); }
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
        int fLASTJBR;
        public int LASTJBR
        {
            get { return fLASTJBR; }
            set { SetPropertyValue<int>(nameof(LASTJBR), ref fLASTJBR, value); }
        }
        DateTime fLASTJBRQ;
        public DateTime LASTJBRQ
        {
            get { return fLASTJBRQ; }
            set { SetPropertyValue<DateTime>(nameof(LASTJBRQ), ref fLASTJBRQ, value); }
        }
        DateTime fEXPIREDATE;
        public DateTime EXPIREDATE
        {
            get { return fEXPIREDATE; }
            set { SetPropertyValue<DateTime>(nameof(EXPIREDATE), ref fEXPIREDATE, value); }
        }
        DateTime fREGISTER_DATE;
        public DateTime REGISTER_DATE
        {
            get { return fREGISTER_DATE; }
            set { SetPropertyValue<DateTime>(nameof(REGISTER_DATE), ref fREGISTER_DATE, value); }
        }
        string fDATA1;
        [Size(50)]
        public string DATA1
        {
            get { return fDATA1; }
            set { SetPropertyValue<string>(nameof(DATA1), ref fDATA1, value); }
        }
        int fDATA2;
        public int DATA2
        {
            get { return fDATA2; }
            set { SetPropertyValue<int>(nameof(DATA2), ref fDATA2, value); }
        }
        string fDATA3;
        [Size(50)]
        public string DATA3
        {
            get { return fDATA3; }
            set { SetPropertyValue<string>(nameof(DATA3), ref fDATA3, value); }
        }
        int fDATA4;
        public int DATA4
        {
            get { return fDATA4; }
            set { SetPropertyValue<int>(nameof(DATA4), ref fDATA4, value); }
        }
        int fLAYER_ID;
        public int LAYER_ID
        {
            get { return fLAYER_ID; }
            set { SetPropertyValue<int>(nameof(LAYER_ID), ref fLAYER_ID, value); }
        }
        int fREGION_ID;
        public int REGION_ID
        {
            get { return fREGION_ID; }
            set { SetPropertyValue<int>(nameof(REGION_ID), ref fREGION_ID, value); }
        }
    }

}
