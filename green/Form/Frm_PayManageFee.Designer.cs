namespace green.Form
{
    partial class Frm_PayManageFee
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
            this.te_position = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.te_expire = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.te_price = new DevExpress.XtraEditors.TextEdit();
            this.labelControl5 = new DevExpress.XtraEditors.LabelControl();
            this.te_nums = new DevExpress.XtraEditors.TextEdit();
            this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
            this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
            this.session1 = new DevExpress.Xpo.Session(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_expire.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_nums.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(39, 34);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(78, 22);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "购墓编号:";
            // 
            // te_ac001
            // 
            this.te_ac001.Enabled = false;
            this.te_ac001.Location = new System.Drawing.Point(140, 30);
            this.te_ac001.Name = "te_ac001";
            this.te_ac001.Size = new System.Drawing.Size(236, 28);
            this.te_ac001.TabIndex = 1;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(39, 83);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(78, 22);
            this.labelControl2.TabIndex = 2;
            this.labelControl2.Text = "墓穴位置:";
            // 
            // te_position
            // 
            this.te_position.Enabled = false;
            this.te_position.Location = new System.Drawing.Point(140, 79);
            this.te_position.Name = "te_position";
            this.te_position.Size = new System.Drawing.Size(236, 28);
            this.te_position.TabIndex = 3;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(39, 132);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(132, 22);
            this.labelControl3.TabIndex = 4;
            this.labelControl3.Text = "管理费到期日期:";
            // 
            // te_expire
            // 
            this.te_expire.Enabled = false;
            this.te_expire.Location = new System.Drawing.Point(247, 128);
            this.te_expire.Name = "te_expire";
            this.te_expire.Properties.Mask.EditMask = "d";
            this.te_expire.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.te_expire.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_expire.Size = new System.Drawing.Size(129, 28);
            this.te_expire.TabIndex = 5;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = null;
            this.labelControl4.AppearanceDisabled.Image = null;
            this.labelControl4.AppearanceHovered.Image = null;
            this.labelControl4.AppearancePressed.Image = null;
            this.labelControl4.Location = new System.Drawing.Point(37, 181);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(128, 22);
            this.labelControl4.TabIndex = 6;
            this.labelControl4.Text = "管理费单价(年):";
            // 
            // te_price
            // 
            this.te_price.Location = new System.Drawing.Point(247, 177);
            this.te_price.Name = "te_price";
            this.te_price.Properties.Appearance.Options.UseTextOptions = true;
            this.te_price.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_price.Properties.Mask.EditMask = "n2";
            this.te_price.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_price.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_price.Size = new System.Drawing.Size(129, 28);
            this.te_price.TabIndex = 7;
            // 
            // labelControl5
            // 
            this.labelControl5.Appearance.Image = null;
            this.labelControl5.AppearanceDisabled.Image = null;
            this.labelControl5.AppearanceHovered.Image = null;
            this.labelControl5.AppearancePressed.Image = null;
            this.labelControl5.Location = new System.Drawing.Point(39, 230);
            this.labelControl5.Name = "labelControl5";
            this.labelControl5.Size = new System.Drawing.Size(78, 22);
            this.labelControl5.TabIndex = 8;
            this.labelControl5.Text = "缴费年限:";
            // 
            // te_nums
            // 
            this.te_nums.Location = new System.Drawing.Point(247, 226);
            this.te_nums.Name = "te_nums";
            this.te_nums.Properties.Appearance.Options.UseTextOptions = true;
            this.te_nums.Properties.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.te_nums.Properties.Mask.EditMask = "N0";
            this.te_nums.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.te_nums.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.te_nums.Size = new System.Drawing.Size(129, 28);
            this.te_nums.TabIndex = 9;
            // 
            // sb_cancel
            // 
            this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
            this.sb_cancel.Appearance.Options.UseBackColor = true;
            this.sb_cancel.Appearance.Options.UseForeColor = true;
            this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
            this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.sb_cancel.Location = new System.Drawing.Point(395, 70);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(124, 30);
            this.sb_cancel.TabIndex = 59;
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
            this.sb_ok.Location = new System.Drawing.Point(395, 29);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(124, 30);
            this.sb_ok.TabIndex = 58;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // session1
            // 
            this.session1.IsObjectModifiedOnNonPersistentPropertyChange = null;
            this.session1.TrackPropertiesModifications = false;
            // 
            // Frm_PayManageFee
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(540, 285);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.te_nums);
            this.Controls.Add(this.labelControl5);
            this.Controls.Add(this.te_price);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.te_expire);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.te_position);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.te_ac001);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_PayManageFee";
            this.Text = "缴纳管理费";
            this.Load += new System.EventHandler(this.Frm_PayManageFee_Load);
            ((System.ComponentModel.ISupportInitialize)(this.te_ac001.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_position.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_expire.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_price.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.te_nums.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.session1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit te_ac001;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit te_position;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit te_expire;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit te_price;
        private DevExpress.XtraEditors.LabelControl labelControl5;
        private DevExpress.XtraEditors.TextEdit te_nums;
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.Xpo.Session session1;
    }
}