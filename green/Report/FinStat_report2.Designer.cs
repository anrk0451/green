namespace green.Report
{
	partial class FinStat_report2
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			DevExpress.DataAccess.Json.CustomJsonSource customJsonSource1 = new DevExpress.DataAccess.Json.CustomJsonSource();
			DevExpress.DataAccess.Json.JsonSchemaNode jsonSchemaNode1 = new DevExpress.DataAccess.Json.JsonSchemaNode();
			DevExpress.DataAccess.Json.JsonSchemaNode jsonSchemaNode2 = new DevExpress.DataAccess.Json.JsonSchemaNode();
			DevExpress.DataAccess.Json.JsonSchemaNode jsonSchemaNode3 = new DevExpress.DataAccess.Json.JsonSchemaNode();
			DevExpress.DataAccess.Json.JsonSchemaNode jsonSchemaNode4 = new DevExpress.DataAccess.Json.JsonSchemaNode();
			DevExpress.XtraReports.UI.XRSummary xrSummary1 = new DevExpress.XtraReports.UI.XRSummary();
			this.jsonDataSource1 = new DevExpress.DataAccess.Json.JsonDataSource(this.components);
			this.Title = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.DetailData3_Odd = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GrandTotalCaption1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GrandTotalData1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.GrandTotalBackground1 = new DevExpress.XtraReports.UI.XRControlStyle();
			this.PageInfo = new DevExpress.XtraReports.UI.XRControlStyle();
			this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
			this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
			this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
			this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
			this.Detail = new DevExpress.XtraReports.UI.DetailBand();
			this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
			this.pageInfo1 = new DevExpress.XtraReports.UI.XRPageInfo();
			this.label1 = new DevExpress.XtraReports.UI.XRLabel();
			this.table1 = new DevExpress.XtraReports.UI.XRTable();
			this.tableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
			this.tableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
			this.tableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
			this.tableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
			this.table2 = new DevExpress.XtraReports.UI.XRTable();
			this.tableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
			this.tableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
			this.tableCell5 = new DevExpress.XtraReports.UI.XRTableCell();
			this.tableCell6 = new DevExpress.XtraReports.UI.XRTableCell();
			this.panel1 = new DevExpress.XtraReports.UI.XRPanel();
			this.label2 = new DevExpress.XtraReports.UI.XRLabel();
			this.label3 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
			this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
			this.daterange = new DevExpress.XtraReports.Parameters.Parameter();
			this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
			((System.ComponentModel.ISupportInitialize)(this.table1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.table2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
			// 
			// jsonDataSource1
			// 
			customJsonSource1.Json = "[{\"SA003\":\"0000000192\",\"NUMS\":19.0,\"SA007\":1200.0}]";
			this.jsonDataSource1.JsonSource = customJsonSource1;
			this.jsonDataSource1.Name = "jsonDataSource1";
			jsonSchemaNode2.Value = new DevExpress.DataAccess.Json.JsonNode("SA003", true, DevExpress.DataAccess.Json.JsonNodeType.Property, typeof(string));
			jsonSchemaNode3.Value = new DevExpress.DataAccess.Json.JsonNode("NUMS", true, DevExpress.DataAccess.Json.JsonNodeType.Property, typeof(System.Nullable<double>));
			jsonSchemaNode4.Value = new DevExpress.DataAccess.Json.JsonNode("SA007", true, DevExpress.DataAccess.Json.JsonNodeType.Property, typeof(System.Nullable<double>));
			jsonSchemaNode1.Nodes.AddRange(new DevExpress.DataAccess.Node<DevExpress.DataAccess.Json.JsonNode>[] {
            jsonSchemaNode2,
            jsonSchemaNode3,
            jsonSchemaNode4});
			jsonSchemaNode1.Value = new DevExpress.DataAccess.Json.JsonNode("root", true, DevExpress.DataAccess.Json.JsonNodeType.Array);
			this.jsonDataSource1.Schema = jsonSchemaNode1;
			// 
			// Title
			// 
			this.Title.BackColor = System.Drawing.Color.Transparent;
			this.Title.BorderColor = System.Drawing.Color.Black;
			this.Title.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.Title.BorderWidth = 1F;
			this.Title.Font = new System.Drawing.Font("Arial", 14.25F);
			this.Title.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.Title.Name = "Title";
			// 
			// DetailCaption1
			// 
			this.DetailCaption1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.DetailCaption1.BorderColor = System.Drawing.Color.White;
			this.DetailCaption1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
			this.DetailCaption1.BorderWidth = 2F;
			this.DetailCaption1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.DetailCaption1.ForeColor = System.Drawing.Color.White;
			this.DetailCaption1.Name = "DetailCaption1";
			this.DetailCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
			this.DetailCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// DetailData1
			// 
			this.DetailData1.BorderColor = System.Drawing.Color.Transparent;
			this.DetailData1.Borders = DevExpress.XtraPrinting.BorderSide.Left;
			this.DetailData1.BorderWidth = 2F;
			this.DetailData1.Font = new System.Drawing.Font("Arial", 8.25F);
			this.DetailData1.ForeColor = System.Drawing.Color.Black;
			this.DetailData1.Name = "DetailData1";
			this.DetailData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
			this.DetailData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// DetailData3_Odd
			// 
			this.DetailData3_Odd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(231)))), ((int)(((byte)(231)))));
			this.DetailData3_Odd.BorderColor = System.Drawing.Color.Transparent;
			this.DetailData3_Odd.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.DetailData3_Odd.BorderWidth = 1F;
			this.DetailData3_Odd.Font = new System.Drawing.Font("Arial", 8.25F);
			this.DetailData3_Odd.ForeColor = System.Drawing.Color.Black;
			this.DetailData3_Odd.Name = "DetailData3_Odd";
			this.DetailData3_Odd.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 6, 0, 0, 100F);
			this.DetailData3_Odd.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// GrandTotalCaption1
			// 
			this.GrandTotalCaption1.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.GrandTotalCaption1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.GrandTotalCaption1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(147)))), ((int)(((byte)(147)))));
			this.GrandTotalCaption1.Name = "GrandTotalCaption1";
			this.GrandTotalCaption1.Padding = new DevExpress.XtraPrinting.PaddingInfo(6, 2, 0, 0, 100F);
			this.GrandTotalCaption1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// GrandTotalData1
			// 
			this.GrandTotalData1.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.GrandTotalData1.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.GrandTotalData1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.GrandTotalData1.Name = "GrandTotalData1";
			this.GrandTotalData1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 6, 0, 0, 100F);
			this.GrandTotalData1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
			// 
			// GrandTotalBackground1
			// 
			this.GrandTotalBackground1.BackColor = System.Drawing.Color.White;
			this.GrandTotalBackground1.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.GrandTotalBackground1.Borders = DevExpress.XtraPrinting.BorderSide.Bottom;
			this.GrandTotalBackground1.BorderWidth = 2F;
			this.GrandTotalBackground1.Name = "GrandTotalBackground1";
			// 
			// PageInfo
			// 
			this.PageInfo.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
			this.PageInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(75)))), ((int)(((byte)(75)))));
			this.PageInfo.Name = "PageInfo";
			this.PageInfo.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
			// 
			// TopMargin
			// 
			this.TopMargin.Name = "TopMargin";
			// 
			// BottomMargin
			// 
			this.BottomMargin.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel1,
            this.pageInfo1});
			this.BottomMargin.Name = "BottomMargin";
			// 
			// ReportHeader
			// 
			this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel2,
            this.label1});
			this.ReportHeader.HeightF = 60F;
			this.ReportHeader.Name = "ReportHeader";
			// 
			// GroupHeader1
			// 
			this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table1});
			this.GroupHeader1.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WithFirstDetail;
			this.GroupHeader1.HeightF = 28F;
			this.GroupHeader1.Name = "GroupHeader1";
			// 
			// Detail
			// 
			this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.table2});
			this.Detail.HeightF = 25F;
			this.Detail.Name = "Detail";
			// 
			// ReportFooter
			// 
			this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.panel1});
			this.ReportFooter.HeightF = 49.38444F;
			this.ReportFooter.Name = "ReportFooter";
			// 
			// pageInfo1
			// 
			this.pageInfo1.LocationFloat = new DevExpress.Utils.PointFloat(169.1667F, 11.78223F);
			this.pageInfo1.Name = "pageInfo1";
			this.pageInfo1.PageInfo = DevExpress.XtraPrinting.PageInfo.DateTime;
			this.pageInfo1.SizeF = new System.Drawing.SizeF(106.3333F, 23F);
			this.pageInfo1.StyleName = "PageInfo";
			// 
			// label1
			// 
			this.label1.LocationFloat = new DevExpress.Utils.PointFloat(5F, 5F);
			this.label1.Name = "label1";
			this.label1.SizeF = new System.Drawing.SizeF(617F, 24.84182F);
			this.label1.StyleName = "Title";
			this.label1.StylePriority.UseTextAlignment = false;
			this.label1.Text = "综合财务统计-服务祭品";
			this.label1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
			// 
			// table1
			// 
			this.table1.LocationFloat = new DevExpress.Utils.PointFloat(42.49998F, 0F);
			this.table1.Name = "table1";
			this.table1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow1});
			this.table1.SizeF = new System.Drawing.SizeF(517.0001F, 28F);
			// 
			// tableRow1
			// 
			this.tableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell1,
            this.tableCell2,
            this.tableCell3});
			this.tableRow1.Name = "tableRow1";
			this.tableRow1.Weight = 1D;
			// 
			// tableCell1
			// 
			this.tableCell1.BackColor = System.Drawing.Color.White;
			this.tableCell1.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.tableCell1.ForeColor = System.Drawing.Color.Black;
			this.tableCell1.Name = "tableCell1";
			this.tableCell1.StyleName = "DetailCaption1";
			this.tableCell1.StylePriority.UseBackColor = false;
			this.tableCell1.StylePriority.UseBorders = false;
			this.tableCell1.StylePriority.UseForeColor = false;
			this.tableCell1.Text = "项目名称";
			this.tableCell1.Weight = 0.268512587546586D;
			// 
			// tableCell2
			// 
			this.tableCell2.BackColor = System.Drawing.Color.White;
			this.tableCell2.ForeColor = System.Drawing.Color.Black;
			this.tableCell2.Name = "tableCell2";
			this.tableCell2.StyleName = "DetailCaption1";
			this.tableCell2.StylePriority.UseBackColor = false;
			this.tableCell2.StylePriority.UseForeColor = false;
			this.tableCell2.StylePriority.UseTextAlignment = false;
			this.tableCell2.Text = "数量";
			this.tableCell2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			this.tableCell2.Weight = 0.2742454613044511D;
			// 
			// tableCell3
			// 
			this.tableCell3.BackColor = System.Drawing.Color.White;
			this.tableCell3.ForeColor = System.Drawing.Color.Black;
			this.tableCell3.Name = "tableCell3";
			this.tableCell3.StyleName = "DetailCaption1";
			this.tableCell3.StylePriority.UseBackColor = false;
			this.tableCell3.StylePriority.UseForeColor = false;
			this.tableCell3.StylePriority.UseTextAlignment = false;
			this.tableCell3.Text = "金额";
			this.tableCell3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			this.tableCell3.Weight = 0.2818034534615641D;
			// 
			// table2
			// 
			this.table2.LocationFloat = new DevExpress.Utils.PointFloat(42.49998F, 0F);
			this.table2.Name = "table2";
			this.table2.OddStyleName = "DetailData3_Odd";
			this.table2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.tableRow2});
			this.table2.SizeF = new System.Drawing.SizeF(517F, 25F);
			// 
			// tableRow2
			// 
			this.tableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.tableCell4,
            this.tableCell5,
            this.tableCell6});
			this.tableRow2.Name = "tableRow2";
			this.tableRow2.Weight = 11.5D;
			// 
			// tableCell4
			// 
			this.tableCell4.Borders = DevExpress.XtraPrinting.BorderSide.None;
			this.tableCell4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SA003]")});
			this.tableCell4.Name = "tableCell4";
			this.tableCell4.StyleName = "DetailData1";
			this.tableCell4.StylePriority.UseBorders = false;
			this.tableCell4.Weight = 0.26851258589700455D;
			// 
			// tableCell5
			// 
			this.tableCell5.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NUMS]")});
			this.tableCell5.Name = "tableCell5";
			this.tableCell5.StyleName = "DetailData1";
			this.tableCell5.StylePriority.UseTextAlignment = false;
			this.tableCell5.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			this.tableCell5.Weight = 0.27424546001066141D;
			// 
			// tableCell6
			// 
			this.tableCell6.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SA007]")});
			this.tableCell6.Name = "tableCell6";
			this.tableCell6.StyleName = "DetailData1";
			this.tableCell6.StylePriority.UseTextAlignment = false;
			this.tableCell6.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			this.tableCell6.TextFormatString = "{0:n2}";
			this.tableCell6.Weight = 0.28180347085077356D;
			// 
			// panel1
			// 
			this.panel1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.label2,
            this.label3});
			this.panel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
			this.panel1.Name = "panel1";
			this.panel1.SizeF = new System.Drawing.SizeF(627F, 49.38444F);
			this.panel1.StyleName = "GrandTotalBackground1";
			// 
			// label2
			// 
			this.label2.LocationFloat = new DevExpress.Utils.PointFloat(361.1426F, 11.50004F);
			this.label2.Name = "label2";
			this.label2.SizeF = new System.Drawing.SizeF(88.46231F, 14.88444F);
			this.label2.StyleName = "GrandTotalCaption1";
			this.label2.Text = "服务祭品合计";
			// 
			// label3
			// 
			this.label3.CanGrow = false;
			this.label3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "sumSum([SA007])")});
			this.label3.LocationFloat = new DevExpress.Utils.PointFloat(449.6049F, 11.50004F);
			this.label3.Name = "label3";
			this.label3.SizeF = new System.Drawing.SizeF(109.895F, 14.88444F);
			this.label3.StyleName = "GrandTotalData1";
			this.label3.StylePriority.UseTextAlignment = false;
			xrSummary1.Running = DevExpress.XtraReports.UI.SummaryRunning.Report;
			this.label3.Summary = xrSummary1;
			this.label3.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
			this.label3.TextFormatString = "{0:n2}";
			this.label3.WordWrap = false;
			// 
			// xrLabel2
			// 
			this.xrLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(42.5F, 35.11556F);
			this.xrLabel2.Name = "xrLabel2";
			this.xrLabel2.SizeF = new System.Drawing.SizeF(93.46242F, 14.88444F);
			this.xrLabel2.StyleName = "GrandTotalCaption1";
			this.xrLabel2.StylePriority.UseForeColor = false;
			this.xrLabel2.Text = "统计时间范围:";
			// 
			// xrLabel1
			// 
			this.xrLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(30.83333F, 11.78223F);
			this.xrLabel1.Name = "xrLabel1";
			this.xrLabel1.SizeF = new System.Drawing.SizeF(59.29575F, 14.88444F);
			this.xrLabel1.StyleName = "GrandTotalCaption1";
			this.xrLabel1.StylePriority.UseForeColor = false;
			this.xrLabel1.Text = "制表日期";
			// 
			// daterange
			// 
			this.daterange.Description = "Parameter1";
			this.daterange.Name = "daterange";
			// 
			// xrLabel3
			// 
			this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "?daterange")});
			this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(135.9624F, 35.11556F);
			this.xrLabel3.Multiline = true;
			this.xrLabel3.Name = "xrLabel3";
			this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
			this.xrLabel3.SizeF = new System.Drawing.SizeF(335.8333F, 14.88444F);
			this.xrLabel3.Text = "xrLabel3";
			// 
			// FinStat_report2
			// 
			this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.ReportHeader,
            this.GroupHeader1,
            this.Detail,
            this.ReportFooter});
			this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.jsonDataSource1});
			this.DataSource = this.jsonDataSource1;
			this.Font = new System.Drawing.Font("Arial", 9.75F);
			this.PageHeight = 1169;
			this.PageWidth = 827;
			this.PaperKind = System.Drawing.Printing.PaperKind.A4;
			this.Parameters.AddRange(new DevExpress.XtraReports.Parameters.Parameter[] {
            this.daterange});
			this.StyleSheet.AddRange(new DevExpress.XtraReports.UI.XRControlStyle[] {
            this.Title,
            this.DetailCaption1,
            this.DetailData1,
            this.DetailData3_Odd,
            this.GrandTotalCaption1,
            this.GrandTotalData1,
            this.GrandTotalBackground1,
            this.PageInfo});
			this.Version = "18.2";
			((System.ComponentModel.ISupportInitialize)(this.table1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.table2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this)).EndInit();

		}

		#endregion

		private DevExpress.DataAccess.Json.JsonDataSource jsonDataSource1;
		private DevExpress.XtraReports.UI.XRControlStyle Title;
		private DevExpress.XtraReports.UI.XRControlStyle DetailCaption1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData1;
		private DevExpress.XtraReports.UI.XRControlStyle DetailData3_Odd;
		private DevExpress.XtraReports.UI.XRControlStyle GrandTotalCaption1;
		private DevExpress.XtraReports.UI.XRControlStyle GrandTotalData1;
		private DevExpress.XtraReports.UI.XRControlStyle GrandTotalBackground1;
		private DevExpress.XtraReports.UI.XRControlStyle PageInfo;
		private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
		private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
		private DevExpress.XtraReports.UI.XRPageInfo pageInfo1;
		private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
		private DevExpress.XtraReports.UI.XRLabel label1;
		private DevExpress.XtraReports.UI.GroupHeaderBand GroupHeader1;
		private DevExpress.XtraReports.UI.XRTable table1;
		private DevExpress.XtraReports.UI.XRTableRow tableRow1;
		private DevExpress.XtraReports.UI.XRTableCell tableCell1;
		private DevExpress.XtraReports.UI.XRTableCell tableCell2;
		private DevExpress.XtraReports.UI.XRTableCell tableCell3;
		private DevExpress.XtraReports.UI.DetailBand Detail;
		private DevExpress.XtraReports.UI.XRTable table2;
		private DevExpress.XtraReports.UI.XRTableRow tableRow2;
		private DevExpress.XtraReports.UI.XRTableCell tableCell4;
		private DevExpress.XtraReports.UI.XRTableCell tableCell5;
		private DevExpress.XtraReports.UI.XRTableCell tableCell6;
		private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
		private DevExpress.XtraReports.UI.XRPanel panel1;
		private DevExpress.XtraReports.UI.XRLabel label2;
		private DevExpress.XtraReports.UI.XRLabel label3;
		private DevExpress.XtraReports.UI.XRLabel xrLabel2;
		private DevExpress.XtraReports.UI.XRLabel xrLabel1;
		private DevExpress.XtraReports.UI.XRLabel xrLabel3;
		private DevExpress.XtraReports.Parameters.Parameter daterange;
	}
}
