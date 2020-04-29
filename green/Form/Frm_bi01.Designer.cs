namespace green.Form
{
    partial class Frm_bi01
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.te_position = new DevExpress.XtraEditors.TextEdit();
            this.te_price = new DevExpress.XtraEditors.TextEdit();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.te_bi003 = new DevExpress.XtraEditors.TextEdit();
            this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.gl_mx = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gl_mx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(33, 28);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "墓位位置:";
            // 
            // te_position
            // 
            this.te_position.Location = new System.Drawing.Point(132, 25);
            this.te_position.Name = "te_position";
            this.te_position.Properties.ReadOnly = true;
            this.te_position.Size = new System.Drawing.Size(300, 28);
            this.te_position.TabIndex = 1;
            // 
            // te_price
            // 
            this.te_price.Enabled = false;
            this.te_price.Location = new System.Drawing.Point(132, 118);
            this.te_price.Name = "te_price";
            this.te_price.Properties.Appearance.Options.UseTextOptions = true;
            this.te_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.te_price.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.DisplayFormat.FormatString = "N2";
            this.te_price.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.te_price.Properties.EditFormat.FormatString = "N2";
            this.te_price.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.te_price.Properties.Mask.EditMask = "N2";
            this.te_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_price.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_price.Size = new System.Drawing.Size(169, 28);
            this.te_price.TabIndex = 3;
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "3";
            this.radioGroup1.Location = new System.Drawing.Point(27, 68);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("3", "墓型"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "修改定价"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "修改号位"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("2", "冻结")});
            this.radioGroup1.Size = new System.Drawing.Size(97, 170);
            this.radioGroup1.TabIndex = 4;
            this.radioGroup1.SelectedIndexChanged += new System.EventHandler(this.radioGroup1_SelectedIndexChanged);
            // 
            // te_bi003
            // 
            this.te_bi003.Enabled = false;
            this.te_bi003.Location = new System.Drawing.Point(132, 157);
            this.te_bi003.Name = "te_bi003";
            this.te_bi003.Properties.Appearance.Options.UseTextOptions = true;
            this.te_bi003.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_bi003.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.te_bi003.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_bi003.Size = new System.Drawing.Size(169, 28);
            this.te_bi003.TabIndex = 5;
            // 
            // sb_cancel
            // 
            this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_cancel.Appearance.Options.UseBackColor = true;
            this.sb_cancel.Appearance.Options.UseForeColor = true;
            this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_cancel.Location = new System.Drawing.Point(342, 230);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(90, 30);
            this.sb_cancel.TabIndex = 51;
            this.sb_cancel.Text = "取消";
            this.sb_cancel.Click += new System.EventHandler(this.sb_cancel_Click);
            // 
            // sb_ok
            // 
            this.sb_ok.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.sb_ok.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_ok.Appearance.Options.UseBackColor = true;
            this.sb_ok.Appearance.Options.UseForeColor = true;
            this.sb_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_ok.Location = new System.Drawing.Point(200, 230);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(137, 30);
            this.sb_ok.TabIndex = 50;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // gl_mx
            // 
            this.gl_mx.Location = new System.Drawing.Point(132, 78);
            this.gl_mx.Name = "gl_mx";
            this.gl_mx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gl_mx.Properties.NullText = "[选择墓型]";
            this.gl_mx.Properties.PopupView = this.gridLookUpEdit1View;
            this.gl_mx.Size = new System.Drawing.Size(169, 28);
            this.gl_mx.TabIndex = 52;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ST001";
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "gridColumn2";
            this.gridColumn2.FieldName = "ST003";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            // 
            // Frm_bi01
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 276);
            this.Controls.Add(this.gl_mx);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.te_bi003);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.te_price);
            this.Controls.Add(this.te_position);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_bi01";
            this.Text = "墓位信息";
            this.Load += new System.EventHandler(this.Frm_bi01_Load);
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_bi003.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gl_mx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_position;
        private DevExpress.XtraEditors.TextEdit te_price;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.TextEdit te_bi003;
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.GridLookUpEdit gl_mx;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}