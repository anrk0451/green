namespace green.Form
{
    partial class Frm_rower
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
            this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.te_price = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.gl_mx = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.te_rg003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.te_rg005 = new DevExpress.XtraEditors.TextEdit();
            this.te_rg006 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
            this.sb_del = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gl_mx.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg003.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg005.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg006.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // sb_cancel
            // 
            this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_cancel.Appearance.Options.UseBackColor = true;
            this.sb_cancel.Appearance.Options.UseForeColor = true;
            this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_cancel.Location = new System.Drawing.Point(280, 294);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(90, 30);
            this.sb_cancel.TabIndex = 67;
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
            this.sb_ok.Location = new System.Drawing.Point(72, 294);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(106, 30);
            this.sb_ok.TabIndex = 66;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // te_price
            // 
            this.te_price.Location = new System.Drawing.Point(140, 131);
            this.te_price.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.te_price.Name = "te_price";
            this.te_price.Properties.Appearance.Options.UseTextOptions = true;
            this.te_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.Mask.EditMask = "N2";
            this.te_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_price.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_price.Size = new System.Drawing.Size(230, 28);
            this.te_price.TabIndex = 55;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(51, 135);
            this.labelControl3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(42, 22);
            this.labelControl3.TabIndex = 54;
            this.labelControl3.Text = "定价:";
            // 
            // gl_mx
            // 
            this.gl_mx.EditValue = "";
            this.gl_mx.Location = new System.Drawing.Point(140, 77);
            this.gl_mx.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.gl_mx.Name = "gl_mx";
            this.gl_mx.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.gl_mx.Properties.NullText = "[请选择墓型]";
            this.gl_mx.Properties.PopupView = this.gridLookUpEdit1View;
            this.gl_mx.Size = new System.Drawing.Size(230, 28);
            this.gl_mx.TabIndex = 53;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowDetailButtons = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupExpandCollapseButtons = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "gridColumn1";
            this.gridColumn1.FieldName = "ST003";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(51, 81);
            this.labelControl2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(42, 22);
            this.labelControl2.TabIndex = 52;
            this.labelControl2.Text = "墓型:";
            // 
            // te_rg003
            // 
            this.te_rg003.Location = new System.Drawing.Point(140, 28);
            this.te_rg003.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.te_rg003.Name = "te_rg003";
            this.te_rg003.Size = new System.Drawing.Size(230, 28);
            this.te_rg003.TabIndex = 51;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(51, 32);
            this.labelControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(60, 22);
            this.labelControl1.TabIndex = 50;
            this.labelControl1.Text = "排名称:";
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = null;
            this.labelControl4.AppearanceDisabled.Image = null;
            this.labelControl4.AppearanceHovered.Image = null;
            this.labelControl4.AppearancePressed.Image = null;
            this.labelControl4.Location = new System.Drawing.Point(51, 191);
            this.labelControl4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(42, 22);
            this.labelControl4.TabIndex = 58;
            this.labelControl4.Text = "号位:";
            // 
            // te_rg005
            // 
            this.te_rg005.Location = new System.Drawing.Point(140, 188);
            this.te_rg005.Name = "te_rg005";
            this.te_rg005.Properties.Mask.EditMask = "N0";
            this.te_rg005.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_rg005.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_rg005.Size = new System.Drawing.Size(100, 28);
            this.te_rg005.TabIndex = 56;
            // 
            // te_rg006
            // 
            this.te_rg006.Location = new System.Drawing.Point(270, 188);
            this.te_rg006.Name = "te_rg006";
            this.te_rg006.Properties.Mask.EditMask = "N0";
            this.te_rg006.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_rg006.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_rg006.Size = new System.Drawing.Size(100, 28);
            this.te_rg006.TabIndex = 57;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Image = null;
            this.labelControl5.AppearanceDisabled.Image = null;
            this.labelControl5.AppearanceHovered.Image = null;
            this.labelControl5.AppearancePressed.Image = null;
            this.labelControl5.Location = new System.Drawing.Point(246, 191);
            this.labelControl5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(18, 22);
            this.labelControl5.TabIndex = 61;
            this.labelControl5.Text = "至";
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Image = null;
            this.labelControl6.AppearanceDisabled.Image = null;
            this.labelControl6.AppearanceHovered.Image = null;
            this.labelControl6.AppearancePressed.Image = null;
            this.labelControl6.Location = new System.Drawing.Point(51, 244);
            this.labelControl6.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(78, 22);
            this.labelControl6.TabIndex = 62;
            this.labelControl6.Text = "排列方向:";
            // 
            // radioGroup1
            // 
            this.radioGroup1.EditValue = "0";
            this.radioGroup1.Location = new System.Drawing.Point(140, 243);
            this.radioGroup1.Name = "radioGroup1";
            this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
            this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem("0", "左"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem("1", "右")});
            this.radioGroup1.Size = new System.Drawing.Size(230, 30);
            this.radioGroup1.TabIndex = 58;
            // 
            // sb_del
            // 
            this.sb_del.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_del.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_del.Appearance.Options.UseBackColor = true;
            this.sb_del.Appearance.Options.UseForeColor = true;
            this.sb_del.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_del.Location = new System.Drawing.Point(184, 294);
            this.sb_del.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_del.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.sb_del.Name = "sb_del";
            this.sb_del.Size = new System.Drawing.Size(90, 30);
            this.sb_del.TabIndex = 68;
            this.sb_del.Text = "删除";
            this.sb_del.Click += new System.EventHandler(this.sb_del_Click);
            // 
            // Frm_rower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 349);
            this.Controls.Add(this.sb_del);
            this.Controls.Add(this.radioGroup1);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.te_rg006);
            this.Controls.Add(this.te_rg005);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.te_price);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.gl_mx);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.te_rg003);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_rower";
            this.Text = "墓区-排";
            this.Load += new System.EventHandler(this.Frm_rower_Load);
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gl_mx.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg003.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg005.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_rg006.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.TextEdit te_price;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.GridLookUpEdit gl_mx;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_rg003;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit te_rg005;
        private DevExpress.XtraEditors.TextEdit te_rg006;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.RadioGroup radioGroup1;
        private DevExpress.XtraEditors.SimpleButton sb_del;
    }
}