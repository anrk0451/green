namespace green.Form
{
    partial class Frm_tombTransfer
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
            this.te_position = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.te_ac001 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.te_ac003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.me_reason = new DevExpress.XtraEditors.MemoEdit();
            this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.be_newposition = new DevExpress.XtraEditors.ButtonEdit();
            this.session1 = new DevExpress.Xpo.Session(this.components);
            this.labelControl6 = new DevExpress.XtraEditors.LabelControl();
            this.te_price = new DevExpress.XtraEditors.TextEdit();
            this.labelControl7 = new DevExpress.XtraEditors.LabelControl();
            this.te_new_price = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ac003.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_reason.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_newposition.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_new_price.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // te_position
            // 
            this.te_position.Enabled = false;
            this.te_position.Location = new System.Drawing.Point(157, 130);
            this.te_position.Name = "te_position";
            this.te_position.Size = new System.Drawing.Size(195, 28);
            this.te_position.TabIndex = 7;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(37, 130);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 22);
            this.labelControl2.TabIndex = 6;
            this.labelControl2.Text = "墓穴位置:";
            // 
            // te_ac001
            // 
            this.te_ac001.Enabled = false;
            this.te_ac001.Location = new System.Drawing.Point(157, 32);
            this.te_ac001.Name = "te_ac001";
            this.te_ac001.Size = new System.Drawing.Size(195, 28);
            this.te_ac001.TabIndex = 5;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(37, 36);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 22);
            this.labelControl1.TabIndex = 4;
            this.labelControl1.Text = "购墓编号:";
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(37, 80);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(60, 22);
            this.labelControl3.TabIndex = 8;
            this.labelControl3.Text = "购墓人:";
            // 
            // te_ac003
            // 
            this.te_ac003.Enabled = false;
            this.te_ac003.Location = new System.Drawing.Point(157, 81);
            this.te_ac003.Name = "te_ac003";
            this.te_ac003.Size = new System.Drawing.Size(195, 28);
            this.te_ac003.TabIndex = 9;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = null;
            this.labelControl4.AppearanceDisabled.Image = null;
            this.labelControl4.AppearanceHovered.Image = null;
            this.labelControl4.AppearancePressed.Image = null;
            this.labelControl4.Location = new System.Drawing.Point(36, 179);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(96, 22);
            this.labelControl4.TabIndex = 10;
            this.labelControl4.Text = "新墓穴位置:";
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Image = null;
            this.labelControl5.AppearanceDisabled.Image = null;
            this.labelControl5.AppearanceHovered.Image = null;
            this.labelControl5.AppearancePressed.Image = null;
            this.labelControl5.Location = new System.Drawing.Point(36, 229);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 22);
            this.labelControl5.TabIndex = 12;
            this.labelControl5.Text = "变更原因:";
            // 
            // me_reason
            // 
            this.me_reason.Location = new System.Drawing.Point(157, 228);
            this.me_reason.Name = "me_reason";
            this.me_reason.Size = new System.Drawing.Size(430, 96);
            this.me_reason.TabIndex = 13;
            // 
            // sb_cancel
            // 
            this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_cancel.Appearance.Options.UseBackColor = true;
            this.sb_cancel.Appearance.Options.UseForeColor = true;
            this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_cancel.Location = new System.Drawing.Point(457, 346);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(124, 30);
            this.sb_cancel.TabIndex = 61;
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
            this.sb_ok.Location = new System.Drawing.Point(327, 346);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(124, 30);
            this.sb_ok.TabIndex = 60;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // be_newposition
            // 
            this.be_newposition.Location = new System.Drawing.Point(157, 179);
            this.be_newposition.Name = "be_newposition";
            this.be_newposition.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.be_newposition.Properties.ReadOnly = true;
            this.be_newposition.Size = new System.Drawing.Size(195, 28);
            this.be_newposition.TabIndex = 11;
            this.be_newposition.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.be_newposition_ButtonClick);
            this.be_newposition.DoubleClick += new System.EventHandler(this.be_newposition_DoubleClick);
            // 
            // session1
            // 
            this.session1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.session1.TrackPropertiesModifications = false;
            // 
            // labelControl6
            // 
            this.labelControl6.Appearance.Image = null;
            this.labelControl6.AppearanceDisabled.Image = null;
            this.labelControl6.AppearanceHovered.Image = null;
            this.labelControl6.AppearancePressed.Image = null;
            this.labelControl6.Location = new System.Drawing.Point(395, 130);
            this.labelControl6.Name = "labelControl6";
            this.labelControl6.Size = new System.Drawing.Size(42, 22);
            this.labelControl6.TabIndex = 62;
            this.labelControl6.Text = "定价:";
            // 
            // te_price
            // 
            this.te_price.Enabled = false;
            this.te_price.Location = new System.Drawing.Point(460, 130);
            this.te_price.Name = "te_price";
            this.te_price.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.te_price.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.AppearanceReadOnly.Options.UseTextOptions = true;
            this.te_price.Properties.AppearanceReadOnly.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.DisplayFormat.FormatString = "N2";
            this.te_price.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.te_price.Size = new System.Drawing.Size(127, 28);
            this.te_price.TabIndex = 63;
            // 
            // labelControl7
            // 
            this.labelControl7.Appearance.Image = null;
            this.labelControl7.AppearanceDisabled.Image = null;
            this.labelControl7.AppearanceHovered.Image = null;
            this.labelControl7.AppearancePressed.Image = null;
            this.labelControl7.Location = new System.Drawing.Point(395, 179);
            this.labelControl7.Name = "labelControl7";
            this.labelControl7.Size = new System.Drawing.Size(42, 22);
            this.labelControl7.TabIndex = 64;
            this.labelControl7.Text = "定价:";
            // 
            // te_new_price
            // 
            this.te_new_price.Enabled = false;
            this.te_new_price.Location = new System.Drawing.Point(460, 179);
            this.te_new_price.Name = "te_new_price";
            this.te_new_price.Properties.AppearanceDisabled.Options.UseTextOptions = true;
            this.te_new_price.Properties.AppearanceDisabled.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_new_price.Properties.DisplayFormat.FormatString = "N2";
            this.te_new_price.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.te_new_price.Size = new System.Drawing.Size(127, 28);
            this.te_new_price.TabIndex = 65;
            // 
            // Frm_tombTransfer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(615, 397);
            this.Controls.Add(this.te_new_price);
            this.Controls.Add(this.labelControl7);
            this.Controls.Add(this.te_price);
            this.Controls.Add(this.labelControl6);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.me_reason);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.te_ac003);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.te_position);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.te_ac001);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.be_newposition);
            this.Name = "Frm_tombTransfer";
            this.Text = "墓位调整";
            this.Load += new System.EventHandler(this.Frm_tombTransfer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_ac003.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.me_reason.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.be_newposition.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_new_price.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit te_position;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_ac001;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_ac003;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.MemoEdit me_reason;
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.ButtonEdit be_newposition;
        private DevExpress.Xpo.Session session1;
        private DevExpress.XtraEditors.LabelControl labelControl6;
        private DevExpress.XtraEditors.TextEdit te_price;
        private DevExpress.XtraEditors.LabelControl labelControl7;
        private DevExpress.XtraEditors.TextEdit te_new_price;
    }
}