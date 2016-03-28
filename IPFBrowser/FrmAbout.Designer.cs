namespace IPFBrowser
{
	partial class FrmAbout
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAbout));
			this.LblName = new System.Windows.Forms.Label();
			this.pictureBox1 = new System.Windows.Forms.PictureBox();
			this.LblVersion = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.GrpLicense = new System.Windows.Forms.GroupBox();
			this.TxtGpl = new System.Windows.Forms.TextBox();
			this.LblLink = new System.Windows.Forms.LinkLabel();
			this.BtnOK = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
			this.panel1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.GrpLicense.SuspendLayout();
			this.SuspendLayout();
			// 
			// LblName
			// 
			this.LblName.AutoSize = true;
			this.LblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LblName.ForeColor = System.Drawing.Color.Gray;
			this.LblName.Location = new System.Drawing.Point(50, 13);
			this.LblName.Name = "LblName";
			this.LblName.Size = new System.Drawing.Size(164, 31);
			this.LblName.TabIndex = 2;
			this.LblName.Text = "IPF Browser";
			// 
			// pictureBox1
			// 
			this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
			this.pictureBox1.Location = new System.Drawing.Point(12, 12);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new System.Drawing.Size(32, 32);
			this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pictureBox1.TabIndex = 3;
			this.pictureBox1.TabStop = false;
			// 
			// LblVersion
			// 
			this.LblVersion.AutoSize = true;
			this.LblVersion.ForeColor = System.Drawing.Color.Gray;
			this.LblVersion.Location = new System.Drawing.Point(209, 28);
			this.LblVersion.Name = "LblVersion";
			this.LblVersion.Size = new System.Drawing.Size(37, 13);
			this.LblVersion.TabIndex = 4;
			this.LblVersion.Text = "v1.0.0";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.BackColor = System.Drawing.SystemColors.Control;
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Controls.Add(this.GrpLicense);
			this.panel1.Controls.Add(this.LblLink);
			this.panel1.Controls.Add(this.BtnOK);
			this.panel1.Location = new System.Drawing.Point(0, 59);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(366, 417);
			this.panel1.TabIndex = 7;
			// 
			// groupBox1
			// 
			this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.groupBox1.BackColor = System.Drawing.SystemColors.Control;
			this.groupBox1.Controls.Add(this.textBox1);
			this.groupBox1.Location = new System.Drawing.Point(12, 211);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(342, 151);
			this.groupBox1.TabIndex = 11;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Credit";
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.BackColor = System.Drawing.SystemColors.Control;
			this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.textBox1.Location = new System.Drawing.Point(6, 19);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ReadOnly = true;
			this.textBox1.Size = new System.Drawing.Size(330, 126);
			this.textBox1.TabIndex = 3;
			this.textBox1.Text = resources.GetString("textBox1.Text");
			// 
			// GrpLicense
			// 
			this.GrpLicense.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.GrpLicense.BackColor = System.Drawing.SystemColors.Control;
			this.GrpLicense.Controls.Add(this.TxtGpl);
			this.GrpLicense.Location = new System.Drawing.Point(12, 12);
			this.GrpLicense.Name = "GrpLicense";
			this.GrpLicense.Size = new System.Drawing.Size(342, 193);
			this.GrpLicense.TabIndex = 9;
			this.GrpLicense.TabStop = false;
			this.GrpLicense.Text = "License";
			// 
			// TxtGpl
			// 
			this.TxtGpl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.TxtGpl.BackColor = System.Drawing.SystemColors.Control;
			this.TxtGpl.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.TxtGpl.Location = new System.Drawing.Point(6, 19);
			this.TxtGpl.Multiline = true;
			this.TxtGpl.Name = "TxtGpl";
			this.TxtGpl.ReadOnly = true;
			this.TxtGpl.Size = new System.Drawing.Size(330, 168);
			this.TxtGpl.TabIndex = 3;
			this.TxtGpl.Text = resources.GetString("TxtGpl.Text");
			// 
			// LblLink
			// 
			this.LblLink.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.LblLink.AutoSize = true;
			this.LblLink.BackColor = System.Drawing.Color.Transparent;
			this.LblLink.Location = new System.Drawing.Point(12, 391);
			this.LblLink.Name = "LblLink";
			this.LblLink.Size = new System.Drawing.Size(112, 13);
			this.LblLink.TabIndex = 8;
			this.LblLink.TabStop = true;
			this.LblLink.Text = "http://aura-project.org";
			this.LblLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.LblLink_LinkClicked);
			// 
			// BtnOK
			// 
			this.BtnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.BtnOK.Location = new System.Drawing.Point(283, 386);
			this.BtnOK.Name = "BtnOK";
			this.BtnOK.Size = new System.Drawing.Size(75, 23);
			this.BtnOK.TabIndex = 7;
			this.BtnOK.Text = "OK";
			this.BtnOK.UseVisualStyleBackColor = true;
			this.BtnOK.Click += new System.EventHandler(this.BtnOK_Click);
			// 
			// FrmAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.White;
			this.ClientSize = new System.Drawing.Size(366, 476);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.LblVersion);
			this.Controls.Add(this.pictureBox1);
			this.Controls.Add(this.LblName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmAbout";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About - IPF Browser";
			((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.GrpLicense.ResumeLayout(false);
			this.GrpLicense.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label LblName;
		private System.Windows.Forms.PictureBox pictureBox1;
		private System.Windows.Forms.Label LblVersion;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.LinkLabel LblLink;
		private System.Windows.Forms.Button BtnOK;
		private System.Windows.Forms.GroupBox GrpLicense;
		private System.Windows.Forms.TextBox TxtGpl;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.TextBox textBox1;
	}
}