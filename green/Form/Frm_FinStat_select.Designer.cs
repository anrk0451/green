namespace green.Form
{
	partial class Frm_FinStat_select
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
			this.radioGroup1 = new DevExpress.XtraEditors.RadioGroup();
			this.sb_cancel = new DevExpress.XtraEditors.SimpleButton();
			this.sb_ok = new DevExpress.XtraEditors.SimpleButton();
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).BeginInit();
			this.SuspendLayout();
			// 
			// radioGroup1
			// 
			this.radioGroup1.EditValue = "0";
			this.radioGroup1.Location = new System.Drawing.Point(20, 15);
			this.radioGroup1.Name = "radioGroup1";
			this.radioGroup1.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
			this.radioGroup1.Properties.Appearance.Options.UseBackColor = true;
			this.radioGroup1.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
			this.radioGroup1.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(0, "售墓"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(1, "服务祭品"),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(2, "管理费")});
			this.radioGroup1.Size = new System.Drawing.Size(199, 124);
			this.radioGroup1.TabIndex = 0;
			// 
			// sb_cancel
			// 
			this.sb_cancel.Appearance.BackColor = System.Drawing.Color.Aqua;
			this.sb_cancel.Appearance.ForeColor = System.Drawing.Color.Black;
			this.sb_cancel.Appearance.Options.UseBackColor = true;
			this.sb_cancel.Appearance.Options.UseForeColor = true;
			this.sb_cancel.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.HotFlat;
			this.sb_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.sb_cancel.Location = new System.Drawing.Point(225, 63);
			this.sb_cancel.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_cancel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.sb_cancel.Name = "sb_cancel";
			this.sb_cancel.Size = new System.Drawing.Size(137, 30);
			this.sb_cancel.TabIndex = 53;
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
			this.sb_ok.Location = new System.Drawing.Point(225, 23);
			this.sb_ok.LookAndFeel.UseDefaultLookAndFeel = false;
			this.sb_ok.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.sb_ok.Name = "sb_ok";
			this.sb_ok.Size = new System.Drawing.Size(137, 30);
			this.sb_ok.TabIndex = 52;
			this.sb_ok.Text = "确定";
			this.sb_ok.Click += new System.EventHandler(this.sb_ok_Click);
			// 
			// Frm_FinStat_select
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(382, 150);
			this.Controls.Add(this.sb_cancel);
			this.Controls.Add(this.sb_ok);
			this.Controls.Add(this.radioGroup1);
			this.Name = "Frm_FinStat_select";
			this.Text = "选择导出数据";
			this.Load += new System.EventHandler(this.Frm_FinStat_select_Load);
			((System.ComponentModel.ISupportInitialize)(this.radioGroup1.Properties)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private DevExpress.XtraEditors.RadioGroup radioGroup1;
		private DevExpress.XtraEditors.SimpleButton sb_cancel;
		private DevExpress.XtraEditors.SimpleButton sb_ok;
	}
}