namespace green.Form
{
    partial class Frm_chgPwd
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
            this.textEdit3 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.textEdit1 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.b_exit = new DevExpress.XtraEditors.SimpleButton();
            this.b_ok = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // textEdit3
            // 
            this.textEdit3.Location = new System.Drawing.Point(158, 121);
            this.textEdit3.Name = "textEdit3";
            this.textEdit3.Properties.PasswordChar = '#';
            this.textEdit3.Size = new System.Drawing.Size(258, 28);
            this.textEdit3.TabIndex = 28;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(57, 124);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(72, 22);
            this.labelControl3.TabIndex = 27;
            this.labelControl3.Text = "重复密码";
            // 
            // textEdit2
            // 
            this.textEdit2.Location = new System.Drawing.Point(158, 75);
            this.textEdit2.Name = "textEdit2";
            this.textEdit2.Properties.PasswordChar = '#';
            this.textEdit2.Size = new System.Drawing.Size(258, 28);
            this.textEdit2.TabIndex = 26;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(57, 78);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(54, 22);
            this.labelControl2.TabIndex = 25;
            this.labelControl2.Text = "新密码";
            // 
            // textEdit1
            // 
            this.textEdit1.Location = new System.Drawing.Point(158, 28);
            this.textEdit1.Name = "textEdit1";
            this.textEdit1.Properties.PasswordChar = '#';
            this.textEdit1.Size = new System.Drawing.Size(258, 28);
            this.textEdit1.TabIndex = 24;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(57, 31);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 22);
            this.labelControl1.TabIndex = 23;
            this.labelControl1.Text = "原密码";
            // 
            // b_exit
            // 
            this.b_exit.Appearance.BackColor = System.Drawing.Color.Aqua;
            this.b_exit.Appearance.ForeColor = System.Drawing.Color.Black;
            this.b_exit.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_exit.Appearance.Options.UseBackColor = true;
            this.b_exit.Appearance.Options.UseForeColor = true;
            this.b_exit.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_exit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.b_exit.Location = new System.Drawing.Point(351, 166);
            this.b_exit.Name = "b_exit";
            this.b_exit.Size = new System.Drawing.Size(63, 31);
            this.b_exit.TabIndex = 30;
            this.b_exit.Text = "取消";
            this.b_exit.Click += new System.EventHandler(this.b_exit_Click);
            // 
            // b_ok
            // 
            this.b_ok.Appearance.BackColor = System.Drawing.Color.LightGreen;
            this.b_ok.Appearance.ForeColor = System.Drawing.Color.Black;
            this.b_ok.Appearance.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.ForwardDiagonal;
            this.b_ok.Appearance.Options.UseBackColor = true;
            this.b_ok.Appearance.Options.UseForeColor = true;
            this.b_ok.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.UltraFlat;
            this.b_ok.Location = new System.Drawing.Point(202, 166);
            this.b_ok.Name = "b_ok";
            this.b_ok.Size = new System.Drawing.Size(143, 31);
            this.b_ok.TabIndex = 29;
            this.b_ok.Text = "确定";
            this.b_ok.Click += new System.EventHandler(this.b_ok_Click);
            // 
            // Frm_chgPwd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 218);
            this.Controls.Add(this.b_exit);
            this.Controls.Add(this.b_ok);
            this.Controls.Add(this.textEdit3);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.textEdit2);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.textEdit1);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_chgPwd";
            this.Text = "修改密码";
            ((System.ComponentModel.ISupportInitialize)(this.textEdit3.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.textEdit1.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.TextEdit textEdit3;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit textEdit2;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit textEdit1;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton b_exit;
        private DevExpress.XtraEditors.SimpleButton b_ok;
    }
}