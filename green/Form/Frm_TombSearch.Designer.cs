namespace green.Form
{
    partial class Frm_TombSearch
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
			this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
			this.te_ac001 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
			this.te_ac003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
			this.te_ac050 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
			this.le_region = new DevExpress.XtraEditors.LookUpEdit();
			this.xpCollection1 = new DevExpress.Xpo.XPCollection(this.components);
			this.session1 = new DevExpress.Xpo.Session(this.components);
			this.te_bi003 = new DevExpress.XtraEditors.TextEdit();
			this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
			this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
			this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
			this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
			this.comboBoxEdit1 = new DevExpress.XtraEditors.ComboBoxEdit();
			this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
			this.te_ac113 = new DevExpress.XtraEditors.TextEdit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac050.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.le_region.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.session1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac113.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// labelControl1
			// 
			this.labelControl1.Appearance.Image = null;
			this.labelControl1.AppearanceDisabled.Image = null;
			this.labelControl1.AppearanceHovered.Image = null;
			this.labelControl1.AppearancePressed.Image = null;
			this.labelControl1.Location = new System.Drawing.Point(39, 32);
			this.labelControl1.Name = "labelControl1";
			this.labelControl1.Size = new System.Drawing.Size(72, 22);
			this.labelControl1.TabIndex = 0;
			this.labelControl1.Text = "购墓编号";
			// 
			// te_ac001
			// 
			this.te_ac001.Location = new System.Drawing.Point(145, 30);
			this.te_ac001.Name = "te_ac001";
			this.te_ac001.Size = new System.Drawing.Size(226, 28);
			this.te_ac001.TabIndex = 1;
			// 
			// labelControl2
			// 
			this.labelControl2.Appearance.Image = null;
			this.labelControl2.AppearanceDisabled.Image = null;
			this.labelControl2.AppearanceHovered.Image = null;
			this.labelControl2.AppearancePressed.Image = null;
			this.labelControl2.Location = new System.Drawing.Point(39, 139);
			this.labelControl2.Name = "labelControl2";
			this.labelControl2.Size = new System.Drawing.Size(54, 22);
			this.labelControl2.TabIndex = 2;
			this.labelControl2.Text = "购墓人";
			// 
			// te_ac003
			// 
			this.te_ac003.Location = new System.Drawing.Point(145, 136);
			this.te_ac003.Name = "te_ac003";
			this.te_ac003.Size = new System.Drawing.Size(226, 28);
			this.te_ac003.TabIndex = 3;
			// 
			// labelControl3
			// 
			this.labelControl3.Appearance.Image = null;
			this.labelControl3.AppearanceDisabled.Image = null;
			this.labelControl3.AppearanceHovered.Image = null;
			this.labelControl3.AppearancePressed.Image = null;
			this.labelControl3.Location = new System.Drawing.Point(39, 84);
			this.labelControl3.Name = "labelControl3";
			this.labelControl3.Size = new System.Drawing.Size(72, 22);
			this.labelControl3.TabIndex = 4;
			this.labelControl3.Text = "证书编号";
			// 
			// te_ac050
			// 
			this.te_ac050.Location = new System.Drawing.Point(145, 81);
			this.te_ac050.Name = "te_ac050";
			this.te_ac050.Size = new System.Drawing.Size(226, 28);
			this.te_ac050.TabIndex = 5;
			// 
			// labelControl4
			// 
			this.labelControl4.Appearance.Image = null;
			this.labelControl4.AppearanceDisabled.Image = null;
			this.labelControl4.AppearanceHovered.Image = null;
			this.labelControl4.AppearancePressed.Image = null;
			this.labelControl4.Location = new System.Drawing.Point(39, 192);
			this.labelControl4.Name = "labelControl4";
			this.labelControl4.Size = new System.Drawing.Size(72, 22);
			this.labelControl4.TabIndex = 6;
			this.labelControl4.Text = "墓穴位置";
			// 
			// le_region
			// 
			this.le_region.Location = new System.Drawing.Point(145, 191);
			this.le_region.Name = "le_region";
			this.le_region.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.le_region.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RG003", ""),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("RG001", "名称6", 20, DevExpress.Utils.FormatType.None, "", false, DevExpress.Utils.HorzAlignment.Default, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
			this.le_region.Properties.DataSource = this.xpCollection1;
			this.le_region.Properties.DisplayMember = "RG003";
			this.le_region.Properties.NullText = "";
			this.le_region.Properties.ShowHeader = false;
			this.le_region.Properties.ValueMember = "RG001";
			this.le_region.Size = new System.Drawing.Size(140, 28);
			this.le_region.TabIndex = 7;
			// 
			// xpCollection1
			// 
			this.xpCollection1.CriteriaString = "[RG002] = \'1\'c";
			this.xpCollection1.ObjectType = typeof(green.xpo.orcl.RG01);
			this.xpCollection1.Session = this.session1;
			this.xpCollection1.Sorting.AddRange(new DevExpress.Xpo.SortProperty[] {
            new DevExpress.Xpo.SortProperty("[RG001]", DevExpress.Xpo.DB.SortingDirection.Ascending)});
			// 
			// session1
			// 
			this.session1.IsObjectModifiedOnNonPersistentPropertyChange = null;
			this.session1.TrackPropertiesModifications = false;
			// 
			// te_bi003
			// 
			this.te_bi003.Location = new System.Drawing.Point(291, 191);
			this.te_bi003.Name = "te_bi003";
			this.te_bi003.Size = new System.Drawing.Size(80, 28);
			this.te_bi003.TabIndex = 8;
			// 
			// labelControl5
			// 
			this.labelControl5.Appearance.Image = null;
			this.labelControl5.AppearanceDisabled.Image = null;
			this.labelControl5.AppearanceHovered.Image = null;
			this.labelControl5.AppearancePressed.Image = null;
			this.labelControl5.Location = new System.Drawing.Point(377, 194);
			this.labelControl5.Name = "labelControl5";
			this.labelControl5.Size = new System.Drawing.Size(18, 22);
			this.labelControl5.TabIndex = 9;
			this.labelControl5.Text = "号";
			// 
			// sb_cancel
			// 
			this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
			this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.sb_cancel.Appearance.Options.UseBackColor = true;
			this.sb_cancel.Appearance.Options.UseForeColor = true;
			this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_cancel.Location = new System.Drawing.Point(388, 70);
			this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_cancel.Name = "sb_cancel";
			this.sb_cancel.Size = new System.Drawing.Size(137, 30);
			this.sb_cancel.TabIndex = 51;
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
			this.sb_ok.Location = new System.Drawing.Point(388, 30);
			this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_ok.Name = "sb_ok";
			this.sb_ok.Size = new System.Drawing.Size(137, 30);
			this.sb_ok.TabIndex = 50;
			this.sb_ok.Text = "确定";
			this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
			// 
			// labelControl6
			// 
			this.labelControl6.Appearance.Image = null;
			this.labelControl6.AppearanceDisabled.Image = null;
			this.labelControl6.AppearanceHovered.Image = null;
			this.labelControl6.AppearancePressed.Image = null;
			this.labelControl6.Location = new System.Drawing.Point(39, 296);
			this.labelControl6.Name = "labelControl6";
			this.labelControl6.Size = new System.Drawing.Size(72, 22);
			this.labelControl6.TabIndex = 52;
			this.labelControl6.Text = "购墓日期";
			// 
			// comboBoxEdit1
			// 
			this.comboBoxEdit1.Location = new System.Drawing.Point(145, 295);
			this.comboBoxEdit1.Name = "comboBoxEdit1";
			this.comboBoxEdit1.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
			this.comboBoxEdit1.Properties.Items.AddRange(new object[] {
            "当日",
            "一周以内",
            "一月以内"});
			this.comboBoxEdit1.Size = new System.Drawing.Size(226, 28);
			this.comboBoxEdit1.TabIndex = 53;
			// 
			// labelControl7
			// 
			this.labelControl7.Appearance.Image = null;
			this.labelControl7.AppearanceDisabled.Image = null;
			this.labelControl7.AppearanceHovered.Image = null;
			this.labelControl7.AppearancePressed.Image = null;
			this.labelControl7.Location = new System.Drawing.Point(39, 243);
			this.labelControl7.Name = "labelControl7";
			this.labelControl7.Size = new System.Drawing.Size(72, 22);
			this.labelControl7.TabIndex = 54;
			this.labelControl7.Text = "安葬逝者";
			// 
			// te_ac113
			// 
			this.te_ac113.Location = new System.Drawing.Point(145, 243);
			this.te_ac113.Name = "te_ac113";
			this.te_ac113.Size = new System.Drawing.Size(226, 28);
			this.te_ac113.TabIndex = 55;
			// 
			// Frm_TombSearch
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(540, 365);
			this.Controls.Add(this.te_ac113);
			this.Controls.Add(this.labelControl7);
			this.Controls.Add(this.comboBoxEdit1);
			this.Controls.Add(this.labelControl6);
			this.Controls.Add(this.sb_cancel);
			this.Controls.Add(this.sb_ok);
			this.Controls.Add(this.labelControl5);
			this.Controls.Add(this.te_bi003);
			this.Controls.Add(this.le_region);
			this.Controls.Add(this.labelControl4);
			this.Controls.Add(this.te_ac050);
			this.Controls.Add(this.labelControl3);
			this.Controls.Add(this.te_ac003);
			this.Controls.Add(this.labelControl2);
			this.Controls.Add(this.te_ac001);
			this.Controls.Add(this.labelControl1);
			this.Name = "Frm_TombSearch";
			this.Text = "查询条件";
			this.Load += new System.EventHandler(this.Frm_TombSearch_Load);
			((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac050.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.le_region.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.xpCollection1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.session1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.comboBoxEdit1.Properties)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.te_ac113.Properties)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_ac001;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_ac003;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_ac050;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LookUpEdit le_region;
        private DevExpress.XtraEditors.TextEdit te_bi003;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.Xpo.Session session1;
        private DevExpress.Xpo.XPCollection xpCollection1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.ComboBoxEdit comboBoxEdit1;
		private DevExpress.XtraEditors.LabelControl labelControl7;
		private DevExpress.XtraEditors.TextEdit te_ac113;
	}
}