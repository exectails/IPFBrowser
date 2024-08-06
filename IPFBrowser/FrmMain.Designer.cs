namespace IPFBrowser
{
	partial class FrmMain
	{
		/// <summary>
		/// Erforderliche Designervariable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Verwendete Ressourcen bereinigen.
		/// </summary>
		/// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Vom Windows Form-Designer generierter Code

		/// <summary>
		/// Erforderliche Methode für die Designerunterstützung.
		/// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMain));
            this.SplMain = new System.Windows.Forms.SplitContainer();
            this.TreeFolders = new System.Windows.Forms.TreeView();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.SplFiles = new System.Windows.Forms.SplitContainer();
            this.LstFiles = new System.Windows.Forms.ListView();
            this.GridPreview = new System.Windows.Forms.DataGridView();
            this.PnlImagePreview = new System.Windows.Forms.Panel();
            this.ImgPreview = new System.Windows.Forms.PictureBox();
            this.TxtPreview = new ScintillaNET.Scintilla();
            this.LblPreview = new System.Windows.Forms.Label();
            this.OfdIpfFile = new System.Windows.Forms.OpenFileDialog();
            this.SfdIpfFile = new System.Windows.Forms.SaveFileDialog();
            this.StatusStrip = new System.Windows.Forms.StatusStrip();
            this.LblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.LblFileName = new System.Windows.Forms.ToolStripStatusLabel();
            this.mainMenu1 = new System.Windows.Forms.MainMenu(this.components);
            this.menuItem1 = new System.Windows.Forms.MenuItem();
            this.BtnMenuOpen = new System.Windows.Forms.MenuItem();
            this.BtnMenuSave = new System.Windows.Forms.MenuItem();
            this.BtnExit = new System.Windows.Forms.MenuItem();
            this.menuItem3 = new System.Windows.Forms.MenuItem();
            this.BtnAbout = new System.Windows.Forms.MenuItem();
            this.SavExtractFile = new System.Windows.Forms.SaveFileDialog();
            this.FbdExtractPack = new Ookii.Dialogs.VistaFolderBrowserDialog();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.BtnOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnExtractClient = new System.Windows.Forms.ToolStripButton();
            this.BtnExtractPack = new System.Windows.Forms.ToolStripButton();
            this.BtnExtractFile = new System.Windows.Forms.ToolStripButton();
            this.BtnSavePack = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.BtnPreview = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.SplMain)).BeginInit();
            this.SplMain.Panel1.SuspendLayout();
            this.SplMain.Panel2.SuspendLayout();
            this.SplMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplFiles)).BeginInit();
            this.SplFiles.Panel1.SuspendLayout();
            this.SplFiles.Panel2.SuspendLayout();
            this.SplFiles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.GridPreview)).BeginInit();
            this.PnlImagePreview.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ImgPreview)).BeginInit();
            this.StatusStrip.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SplMain
            // 
            this.SplMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.SplMain.IsSplitterFixed = true;
            this.SplMain.Location = new System.Drawing.Point(0, 41);
            this.SplMain.Name = "SplMain";
            // 
            // SplMain.Panel1
            // 
            this.SplMain.Panel1.Controls.Add(this.TreeFolders);
            this.SplMain.Panel1MinSize = 250;
            // 
            // SplMain.Panel2
            // 
            this.SplMain.Panel2.Controls.Add(this.SplFiles);
            this.SplMain.Size = new System.Drawing.Size(964, 535);
            this.SplMain.SplitterDistance = 250;
            this.SplMain.TabIndex = 0;
            // 
            // TreeFolders
            // 
            this.TreeFolders.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TreeFolders.HideSelection = false;
            this.TreeFolders.ImageIndex = 2;
            this.TreeFolders.ImageList = this.imageList1;
            this.TreeFolders.Location = new System.Drawing.Point(0, 0);
            this.TreeFolders.Name = "TreeFolders";
            this.TreeFolders.PathSeparator = "/";
            this.TreeFolders.SelectedImageIndex = 2;
            this.TreeFolders.Size = new System.Drawing.Size(250, 535);
            this.TreeFolders.TabIndex = 0;
            this.TreeFolders.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeFolders_AfterSelect);
            this.TreeFolders.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TreeFolders_KeyDown);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "folder_page_white.png");
            this.imageList1.Images.SetKeyName(1, "folder_page.png");
            this.imageList1.Images.SetKeyName(2, "folder.png");
            this.imageList1.Images.SetKeyName(3, "compress.png");
            this.imageList1.Images.SetKeyName(4, "page_white_text.png");
            this.imageList1.Images.SetKeyName(5, "page_white_code.png");
            this.imageList1.Images.SetKeyName(6, "page_white.png");
            this.imageList1.Images.SetKeyName(7, "image.png");
            this.imageList1.Images.SetKeyName(8, "table.png");
            // 
            // SplFiles
            // 
            this.SplFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplFiles.Location = new System.Drawing.Point(0, 0);
            this.SplFiles.Name = "SplFiles";
            this.SplFiles.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplFiles.Panel1
            // 
            this.SplFiles.Panel1.Controls.Add(this.LstFiles);
            // 
            // SplFiles.Panel2
            // 
            this.SplFiles.Panel2.Controls.Add(this.GridPreview);
            this.SplFiles.Panel2.Controls.Add(this.PnlImagePreview);
            this.SplFiles.Panel2.Controls.Add(this.TxtPreview);
            this.SplFiles.Panel2.Controls.Add(this.LblPreview);
            this.SplFiles.Size = new System.Drawing.Size(710, 535);
            this.SplFiles.SplitterDistance = 231;
            this.SplFiles.TabIndex = 0;
            // 
            // LstFiles
            // 
            this.LstFiles.AllowDrop = true;
            this.LstFiles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LstFiles.HideSelection = false;
            this.LstFiles.Location = new System.Drawing.Point(0, 0);
            this.LstFiles.MultiSelect = false;
            this.LstFiles.Name = "LstFiles";
            this.LstFiles.Size = new System.Drawing.Size(710, 231);
            this.LstFiles.SmallImageList = this.imageList1;
            this.LstFiles.TabIndex = 0;
            this.LstFiles.UseCompatibleStateImageBehavior = false;
            this.LstFiles.View = System.Windows.Forms.View.List;
            this.LstFiles.SelectedIndexChanged += new System.EventHandler(this.LstFiles_SelectedIndexChanged);
            this.LstFiles.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstFiles_DropFile);
            this.LstFiles.DragEnter += new System.Windows.Forms.DragEventHandler(this.LstFiles_DragEnter);
            this.LstFiles.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LstFiles_KeyDown);
            // 
            // GridPreview
            // 
            this.GridPreview.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.GridPreview.Location = new System.Drawing.Point(456, 25);
            this.GridPreview.Name = "GridPreview";
            this.GridPreview.Size = new System.Drawing.Size(240, 150);
            this.GridPreview.TabIndex = 6;
            this.GridPreview.RowHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridPreview_InsertRow);
            this.GridPreview.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridPreview_ValueChanged);
            this.GridPreview.UserDeletedRow += new System.Windows.Forms.DataGridViewRowEventHandler(this.GridPreview_DeleteRow);
            // 
            // PnlImagePreview
            // 
            this.PnlImagePreview.AutoScroll = true;
            this.PnlImagePreview.Controls.Add(this.ImgPreview);
            this.PnlImagePreview.Location = new System.Drawing.Point(239, 11);
            this.PnlImagePreview.Name = "PnlImagePreview";
            this.PnlImagePreview.Size = new System.Drawing.Size(211, 164);
            this.PnlImagePreview.TabIndex = 5;
            // 
            // ImgPreview
            // 
            this.ImgPreview.Location = new System.Drawing.Point(0, 0);
            this.ImgPreview.Name = "ImgPreview";
            this.ImgPreview.Size = new System.Drawing.Size(156, 100);
            this.ImgPreview.TabIndex = 4;
            this.ImgPreview.TabStop = false;
            // 
            // TxtPreview
            // 
            this.TxtPreview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TxtPreview.Location = new System.Drawing.Point(33, 75);
            this.TxtPreview.Name = "TxtPreview";
            this.TxtPreview.ScrollWidth = 100;
            this.TxtPreview.Size = new System.Drawing.Size(200, 100);
            this.TxtPreview.TabIndex = 2;
            this.TxtPreview.UseTabs = false;
            this.TxtPreview.TextChanged += new System.EventHandler(this.TxtPreview_TextChanged);
            // 
            // LblPreview
            // 
            this.LblPreview.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LblPreview.ForeColor = System.Drawing.Color.Silver;
            this.LblPreview.Location = new System.Drawing.Point(27, 11);
            this.LblPreview.Name = "LblPreview";
            this.LblPreview.Size = new System.Drawing.Size(122, 61);
            this.LblPreview.TabIndex = 1;
            this.LblPreview.Text = "Preview";
            this.LblPreview.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // OfdIpfFile
            // 
            this.OfdIpfFile.Filter = "IPF-files|*.ipf";
            // 
            // SfdIpfFile
            // 
            this.SfdIpfFile.Filter = "IPF-files|*.ipf";
            // 
            // StatusStrip
            // 
            this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.LblVersion,
            this.LblFileName});
            this.StatusStrip.Location = new System.Drawing.Point(0, 576);
            this.StatusStrip.Name = "StatusStrip";
            this.StatusStrip.Size = new System.Drawing.Size(964, 24);
            this.StatusStrip.TabIndex = 3;
            // 
            // LblVersion
            // 
            this.LblVersion.AutoSize = false;
            this.LblVersion.Name = "LblVersion";
            this.LblVersion.Size = new System.Drawing.Size(118, 19);
            this.LblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.LblVersion.Click += new System.EventHandler(this.LblVersion_Click);
            // 
            // LblFileName
            // 
            this.LblFileName.BorderSides = System.Windows.Forms.ToolStripStatusLabelBorderSides.Left;
            this.LblFileName.BorderStyle = System.Windows.Forms.Border3DStyle.Etched;
            this.LblFileName.Name = "LblFileName";
            this.LblFileName.Size = new System.Drawing.Size(4, 19);
            this.LblFileName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mainMenu1
            // 
            this.mainMenu1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.menuItem1,
            this.menuItem3});
            // 
            // menuItem1
            // 
            this.menuItem1.Index = 0;
            this.menuItem1.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.BtnMenuOpen,
            this.BtnMenuSave,
            this.BtnExit});
            this.menuItem1.Text = "File";
            // 
            // BtnMenuOpen
            // 
            this.BtnMenuOpen.Index = 0;
            this.BtnMenuOpen.Text = "Open...";
            this.BtnMenuOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // BtnMenuSave
            // 
            this.BtnMenuSave.Index = 1;
            this.BtnMenuSave.Text = "Save As...";
            this.BtnMenuSave.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // BtnExit
            // 
            this.BtnExit.Index = 2;
            this.BtnExit.Text = "Exit";
            this.BtnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // menuItem3
            // 
            this.menuItem3.Index = 1;
            this.menuItem3.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.BtnAbout});
            this.menuItem3.Text = "Help";
            // 
            // BtnAbout
            // 
            this.BtnAbout.Index = 0;
            this.BtnAbout.Text = "About";
            this.BtnAbout.Click += new System.EventHandler(this.BtnAbout_Click);
            // 
            // SavExtractFile
            // 
            this.SavExtractFile.Filter = "All files|*.*";
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.BtnOpen,
            this.toolStripSeparator1,
            this.BtnExtractClient,
            this.BtnExtractPack,
            this.BtnExtractFile,
            this.BtnSavePack,
            this.toolStripSeparator2,
            this.BtnPreview});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(964, 41);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // BtnOpen
            // 
            this.BtnOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnOpen.Image = ((System.Drawing.Image)(resources.GetObject("BtnOpen.Image")));
            this.BtnOpen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnOpen.Name = "BtnOpen";
            this.BtnOpen.Size = new System.Drawing.Size(36, 38);
            this.BtnOpen.Text = "Open IPF file";
            this.BtnOpen.Click += new System.EventHandler(this.BtnOpen_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 41);
            // 
            // BtnExtractClient
            // 
            this.BtnExtractClient.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExtractClient.Image = ((System.Drawing.Image)(resources.GetObject("BtnExtractClient.Image")));
            this.BtnExtractClient.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExtractClient.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExtractClient.Name = "BtnExtractClient";
            this.BtnExtractClient.Size = new System.Drawing.Size(36, 38);
            this.BtnExtractClient.Text = "Extract all client IPFs";
            this.BtnExtractClient.Click += new System.EventHandler(this.BtnExtractClient_Click);
            // 
            // BtnExtractPack
            // 
            this.BtnExtractPack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExtractPack.Image = ((System.Drawing.Image)(resources.GetObject("BtnExtractPack.Image")));
            this.BtnExtractPack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExtractPack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExtractPack.Name = "BtnExtractPack";
            this.BtnExtractPack.Size = new System.Drawing.Size(36, 38);
            this.BtnExtractPack.Text = "Extract open IPF file";
            this.BtnExtractPack.Click += new System.EventHandler(this.BtnExtractPack_Click);
            // 
            // BtnExtractFile
            // 
            this.BtnExtractFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnExtractFile.Image = ((System.Drawing.Image)(resources.GetObject("BtnExtractFile.Image")));
            this.BtnExtractFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnExtractFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnExtractFile.Name = "BtnExtractFile";
            this.BtnExtractFile.Size = new System.Drawing.Size(36, 38);
            this.BtnExtractFile.Text = "Extract selected file";
            this.BtnExtractFile.Click += new System.EventHandler(this.BtnExtractFile_Click);
            // 
            // BtnSavePack
            // 
            this.BtnSavePack.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnSavePack.Image = ((System.Drawing.Image)(resources.GetObject("BtnSavePack.Image")));
            this.BtnSavePack.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnSavePack.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnSavePack.Name = "BtnSavePack";
            this.BtnSavePack.Size = new System.Drawing.Size(36, 38);
            this.BtnSavePack.Text = "Save Modified IPF File";
            this.BtnSavePack.ToolTipText = "Save Modified IPF File";
            this.BtnSavePack.Click += new System.EventHandler(this.BtnSave_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 41);
            // 
            // BtnPreview
            // 
            this.BtnPreview.Checked = true;
            this.BtnPreview.CheckOnClick = true;
            this.BtnPreview.CheckState = System.Windows.Forms.CheckState.Checked;
            this.BtnPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.BtnPreview.Image = ((System.Drawing.Image)(resources.GetObject("BtnPreview.Image")));
            this.BtnPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.BtnPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.BtnPreview.Name = "BtnPreview";
            this.BtnPreview.Size = new System.Drawing.Size(36, 38);
            this.BtnPreview.Text = "Show Preview";
            this.BtnPreview.Click += new System.EventHandler(this.BtnPreview_Click);
            // 
            // FrmMain
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(964, 600);
            this.Controls.Add(this.SplMain);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.StatusStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Menu = this.mainMenu1;
            this.Name = "FrmMain";
            this.Text = "IPF Browser";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMain_FormClosing);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.FrmMain_DragEnter);
            this.SplMain.Panel1.ResumeLayout(false);
            this.SplMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplMain)).EndInit();
            this.SplMain.ResumeLayout(false);
            this.SplFiles.Panel1.ResumeLayout(false);
            this.SplFiles.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplFiles)).EndInit();
            this.SplFiles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.GridPreview)).EndInit();
            this.PnlImagePreview.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ImgPreview)).EndInit();
            this.StatusStrip.ResumeLayout(false);
            this.StatusStrip.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

        #endregion

        private System.Windows.Forms.SplitContainer SplMain;
		private System.Windows.Forms.TreeView TreeFolders;
		private System.Windows.Forms.SplitContainer SplFiles;
		private System.Windows.Forms.ListView LstFiles;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton BtnOpen;
		private System.Windows.Forms.OpenFileDialog OfdIpfFile;
        private System.Windows.Forms.SaveFileDialog SfdIpfFile;
        private System.Windows.Forms.ImageList imageList1;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.ToolStripStatusLabel LblVersion;
		private System.Windows.Forms.ToolStripStatusLabel LblFileName;
		private System.Windows.Forms.MainMenu mainMenu1;
		private System.Windows.Forms.MenuItem menuItem1;
		private System.Windows.Forms.MenuItem BtnExit;
		private System.Windows.Forms.MenuItem menuItem3;
		private System.Windows.Forms.MenuItem BtnAbout;
		private System.Windows.Forms.Label LblPreview;
		private ScintillaNET.Scintilla TxtPreview;
		private System.Windows.Forms.Panel PnlImagePreview;
		private System.Windows.Forms.PictureBox ImgPreview;
		private System.Windows.Forms.MenuItem BtnMenuOpen;
		private System.Windows.Forms.DataGridView GridPreview;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton BtnExtractPack;
        private System.Windows.Forms.ToolStripButton BtnExtractFile;
        private System.Windows.Forms.ToolStripButton BtnSavePack;
		private System.Windows.Forms.SaveFileDialog SavExtractFile;
		private Ookii.Dialogs.VistaFolderBrowserDialog FbdExtractPack;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton BtnExtractClient;
		private System.Windows.Forms.ToolStripButton BtnPreview;
        private System.Windows.Forms.MenuItem BtnMenuSave;
    }
}

