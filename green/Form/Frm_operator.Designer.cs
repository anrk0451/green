namespace green.Form
{
    partial class Frm_operator
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
            this.clbx_roles = new DevExpress.XtraEditors.CheckedListBoxControl();
            this.txtedit_pwd2 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl4 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_pwd = new DevExpress.XtraEditors.TextEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_uc003 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txtedit_uc002 = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).BeginInit();
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
            this.sb_cancel.Location = new System.Drawing.Point(354, 334);
            this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_cancel.Name = "sb_cancel";
            this.sb_cancel.Size = new System.Drawing.Size(90, 30);
            this.sb_cancel.TabIndex = 47;
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
            this.sb_ok.Location = new System.Drawing.Point(211, 334);
            this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
            this.sb_ok.Name = "sb_ok";
            this.sb_ok.Size = new System.Drawing.Size(137, 30);
            this.sb_ok.TabIndex = 46;
            this.sb_ok.Text = "确定";
            this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
            // 
            // clbx_roles
            // 
            this.clbx_roles.CheckOnClick = true;
            this.clbx_roles.Cursor = System.Windows.Forms.Cursors.Default;
            this.clbx_roles.DisplayMember = "RO003";
            this.clbx_roles.Location = new System.Drawing.Point(89, 221);
            this.clbx_roles.Name = "clbx_roles";
            this.clbx_roles.Size = new System.Drawing.Size(356, 95);
            this.clbx_roles.TabIndex = 45;
            this.clbx_roles.ValueMember = "RO001";
            // 
            // txtedit_pwd2
            // 
            this.txtedit_pwd2.EditValue = "";
            this.txtedit_pwd2.Location = new System.Drawing.Point(192, 169);
            this.txtedit_pwd2.Name = "txtedit_pwd2";
            this.txtedit_pwd2.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtedit_pwd2.Properties.NullText = "请再次输入密码";
            this.txtedit_pwd2.Properties.NullValuePrompt = "请再次输入密码";
            this.txtedit_pwd2.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtedit_pwd2.Properties.PasswordChar = '*';
            this.txtedit_pwd2.Size = new System.Drawing.Size(253, 28);
            this.txtedit_pwd2.TabIndex = 44;
            // 
            // labelControl4
            // 
            this.labelControl4.Appearance.Image = null;
            this.labelControl4.AppearanceDisabled.Image = null;
            this.labelControl4.AppearanceHovered.Image = null;
            this.labelControl4.AppearancePressed.Image = null;
            this.labelControl4.Location = new System.Drawing.Point(89, 173);
            this.labelControl4.Name = "labelControl4";
            this.labelControl4.Size = new System.Drawing.Size(82, 22);
            this.labelControl4.TabIndex = 43;
            this.labelControl4.Text = "确认密码*";
            // 
            // txtedit_pwd
            // 
            this.txtedit_pwd.EditValue = "";
            this.txtedit_pwd.Location = new System.Drawing.Point(192, 122);
            this.txtedit_pwd.Name = "txtedit_pwd";
            this.txtedit_pwd.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtedit_pwd.Properties.NullText = "请输入密码";
            this.txtedit_pwd.Properties.NullValuePrompt = "请输入密码";
            this.txtedit_pwd.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtedit_pwd.Properties.PasswordChar = '*';
            this.txtedit_pwd.Size = new System.Drawing.Size(253, 28);
            this.txtedit_pwd.TabIndex = 42;
            // 
            // labelControl3
            // 
            this.labelControl3.Appearance.Image = null;
            this.labelControl3.AppearanceDisabled.Image = null;
            this.labelControl3.AppearanceHovered.Image = null;
            this.labelControl3.AppearancePressed.Image = null;
            this.labelControl3.Location = new System.Drawing.Point(89, 125);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(46, 22);
            this.labelControl3.TabIndex = 41;
            this.labelControl3.Text = "密码*";
            // 
            // txtedit_uc003
            // 
            this.txtedit_uc003.EditValue = "";
            this.txtedit_uc003.Location = new System.Drawing.Point(192, 71);
            this.txtedit_uc003.Name = "txtedit_uc003";
            this.txtedit_uc003.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.txtedit_uc003.Properties.NullText = "请输入用户姓名";
            this.txtedit_uc003.Properties.NullValuePrompt = "请输入用户姓名";
            this.txtedit_uc003.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtedit_uc003.Size = new System.Drawing.Size(253, 28);
            this.txtedit_uc003.TabIndex = 40;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Image = null;
            this.labelControl2.AppearanceDisabled.Image = null;
            this.labelControl2.AppearanceHovered.Image = null;
            this.labelControl2.AppearancePressed.Image = null;
            this.labelControl2.Location = new System.Drawing.Point(89, 74);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(82, 22);
            this.labelControl2.TabIndex = 39;
            this.labelControl2.Text = "用户姓名*";
            // 
            // txtedit_uc002
            // 
            this.txtedit_uc002.EditValue = "";
            this.txtedit_uc002.Location = new System.Drawing.Point(192, 24);
            this.txtedit_uc002.Name = "txtedit_uc002";
            this.txtedit_uc002.Properties.NullText = "请输入用户登录代码";
            this.txtedit_uc002.Properties.NullValuePrompt = "请输入用户登录代码";
            this.txtedit_uc002.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtedit_uc002.Properties.ShowNullValuePromptWhenFocused = true;
            this.txtedit_uc002.Properties.ValidateOnEnterKey = true;
            this.txtedit_uc002.Size = new System.Drawing.Size(253, 28);
            this.txtedit_uc002.TabIndex = 38;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Image = null;
            this.labelControl1.AppearanceDisabled.Image = null;
            this.labelControl1.AppearanceHovered.Image = null;
            this.labelControl1.AppearancePressed.Image = null;
            this.labelControl1.Location = new System.Drawing.Point(89, 27);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(82, 22);
            this.labelControl1.TabIndex = 37;
            this.labelControl1.Text = "用户代码*";
            // 
            // Frm_operator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 382);
            this.Controls.Add(this.sb_cancel);
            this.Controls.Add(this.sb_ok);
            this.Controls.Add(this.clbx_roles);
            this.Controls.Add(this.txtedit_pwd2);
            this.Controls.Add(this.labelControl4);
            this.Controls.Add(this.txtedit_pwd);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.txtedit_uc003);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txtedit_uc002);
            this.Controls.Add(this.labelControl1);
            this.Name = "Frm_operator";
            this.Text = "操作员";
            this.Load += new System.EventHandler(this.Frm_operator_Load);
            ((System.ComponentModel.ISupportInitialize)(this.clbx_roles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd2.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_pwd.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_uc003.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtedit_uc002.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.SimpleButton sb_cancel;
        private DevExpress.XtraEditors.SimpleButton sb_ok;
        private DevExpress.XtraEditors.CheckedListBoxControl clbx_roles;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd2;
        private DevExpress.XtraEditors.LabelControl labelControl4;
        private DevExpress.XtraEditors.TextEdit txtedit_pwd;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.TextEdit txtedit_uc003;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.TextEdit txtedit_uc002;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}