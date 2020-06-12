namespace green.Form
{
    partial class Frm_refund
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.xpCollection1 = new DevExpress.Xpo.XPCollection(this.components);
            this.unitOfWork1 = new DevExpress.Xpo.UnitOfWork(this.components);
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSA001 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSA002 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSA003 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSA004 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSA005 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPRICE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNUMS = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSA007 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.te_total = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.te_reason = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_total.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_reason.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControl1
            // 
            this.gridControl1.DataSource = this.xpCollection1;
            this.gridControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.gridControl1.Location = new System.Drawing.Point(0, 0);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(729, 413);
            this.gridControl1.TabIndex = 0;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // xpCollection1
            // 
            this.xpCollection1.LoadingEnabled = false;
            this.xpCollection1.ObjectType = typeof(green.xpo.orcl.V_SA01);
            this.xpCollection1.Session = this.unitOfWork1;
            // 
            // unitOfWork1
            // 
            this.unitOfWork1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.unitOfWork1.TrackPropertiesModifications = false;
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSA001,
            this.colSA002,
            this.colSA003,
            this.colSA004,
            this.colSA005,
            this.colPRICE,
            this.colNUMS,
            this.colSA007});
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsBehavior.Editable = false;
            this.gridView1.OptionsSelection.InvertSelection = true;
            this.gridView1.OptionsSelection.MultiSelect = true;
            this.gridView1.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CheckBoxRowSelect;
            this.gridView1.OptionsView.ColumnAutoWidth = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            // 
            // colSA001
            // 
            this.colSA001.FieldName = "SA001";
            this.colSA001.MinWidth = 25;
            this.colSA001.Name = "colSA001";
            this.colSA001.Width = 94;
            // 
            // colSA002
            // 
            this.colSA002.FieldName = "SA002";
            this.colSA002.MinWidth = 25;
            this.colSA002.Name = "colSA002";
            this.colSA002.OptionsColumn.AllowShowHide = false;
            this.colSA002.Width = 94;
            // 
            // colSA003
            // 
            this.colSA003.Caption = "项目名称";
            this.colSA003.FieldName = "SA003";
            this.colSA003.MinWidth = 25;
            this.colSA003.Name = "colSA003";
            this.colSA003.Visible = true;
            this.colSA003.VisibleIndex = 1;
            this.colSA003.Width = 180;
            // 
            // colSA004
            // 
            this.colSA004.FieldName = "SA004";
            this.colSA004.MinWidth = 25;
            this.colSA004.Name = "colSA004";
            this.colSA004.OptionsColumn.AllowShowHide = false;
            this.colSA004.Width = 94;
            // 
            // colSA005
            // 
            this.colSA005.FieldName = "SA005";
            this.colSA005.MinWidth = 25;
            this.colSA005.Name = "colSA005";
            this.colSA005.OptionsColumn.AllowShowHide = false;
            this.colSA005.Width = 94;
            // 
            // colPRICE
            // 
            this.colPRICE.Caption = "单价";
            this.colPRICE.DisplayFormat.FormatString = "N2";
            this.colPRICE.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colPRICE.FieldName = "PRICE";
            this.colPRICE.MinWidth = 25;
            this.colPRICE.Name = "colPRICE";
            this.colPRICE.Visible = true;
            this.colPRICE.VisibleIndex = 2;
            this.colPRICE.Width = 124;
            // 
            // colNUMS
            // 
            this.colNUMS.Caption = "数量";
            this.colNUMS.DisplayFormat.FormatString = "N1";
            this.colNUMS.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNUMS.FieldName = "NUMS";
            this.colNUMS.MinWidth = 25;
            this.colNUMS.Name = "colNUMS";
            this.colNUMS.Visible = true;
            this.colNUMS.VisibleIndex = 3;
            this.colNUMS.Width = 99;
            // 
            // colSA007
            // 
            this.colSA007.Caption = "金额";
            this.colSA007.DisplayFormat.FormatString = "N2";
            this.colSA007.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSA007.FieldName = "SA007";
            this.colSA007.MinWidth = 25;
            this.colSA007.Name = "colSA007";
            this.colSA007.Visible = true;
            this.colSA007.VisibleIndex = 4;
            this.colSA007.Width = 146;
            // 
            // sb_cancel
            // 
            this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_cancel.Appearance.Options.UseBackColor = true;
            this.sb_cancel.Appearance.Options.UseForeColor = true;
            this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_cancel.Location = new System.Drawing.Point(616, 480);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(101, 30);
            this.sb_cancel.TabIndex = 65;
            this.sb_cancel.Text = "关闭";
            this.sb_cancel.Click += new System.EventHandler(this.sb_cancel_Click);
            // 
            // sb_ok
            // 
            this.sb_ok.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.sb_ok.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_ok.Appearance.Options.UseBackColor = true;
            this.sb_ok.Appearance.Options.UseForeColor = true;
            this.sb_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_ok.Location = new System.Drawing.Point(486, 480);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(124, 30);
            this.sb_ok.TabIndex = 64;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(12, 476);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(72, 22);
            this.labelControl1.TabIndex = 66;
            this.labelControl1.Text = "退费合计";
            // 
            // te_total
            // 
            this.te_total.Enabled = false;
            this.te_total.Location = new System.Drawing.Point(97, 473);
            this.te_total.Name = "te_total";
            this.te_total.Properties.Appearance.Options.UseTextOptions = true;
            this.te_total.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_total.Properties.Mask.EditMask = "N2";
            this.te_total.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_total.Size = new System.Drawing.Size(101, 28);
            this.te_total.TabIndex = 67;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(12, 434);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(72, 22);
            this.labelControl2.TabIndex = 68;
            this.labelControl2.Text = "退费原因";
            // 
            // te_reason
            // 
            this.te_reason.Enabled = false;
            this.te_reason.Location = new System.Drawing.Point(97, 432);
            this.te_reason.Name = "te_reason";
            this.te_reason.Properties.Appearance.Options.UseTextOptions = true;
            this.te_reason.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_reason.Properties.Mask.EditMask = "N2";
            this.te_reason.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_reason.Size = new System.Drawing.Size(620, 28);
            this.te_reason.TabIndex = 69;
            // 
            // Frm_refund
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(729, 522);
            this.Controls.Add(this.te_reason);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.te_total);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.gridControl1);
            this.Name = "Frm_refund";
            this.Text = "退费";
            this.Load += new System.EventHandler(this.Frm_refund_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.unitOfWork1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_total.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_reason.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControl1;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.Xpo.UnitOfWork unitOfWork1;
        private DevExpress.Xpo.XPCollection xpCollection1;
        private DevExpress.XtraGrid.Columns.GridColumn colSA001;
        private DevExpress.XtraGrid.Columns.GridColumn colSA002;
        private DevExpress.XtraGrid.Columns.GridColumn colSA003;
        private DevExpress.XtraGrid.Columns.GridColumn colSA004;
        private DevExpress.XtraGrid.Columns.GridColumn colSA005;
        private DevExpress.XtraGrid.Columns.GridColumn colPRICE;
        private DevExpress.XtraGrid.Columns.GridColumn colNUMS;
        private DevExpress.XtraGrid.Columns.GridColumn colSA007;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_total;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_reason;
    }
}