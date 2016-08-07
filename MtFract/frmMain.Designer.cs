namespace MtFract
{
	partial class frmMain
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.tmrSeries = new System.Windows.Forms.Timer(this.components);
			this.picFractal = new System.Windows.Forms.PictureBox();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.ContentPanel = new System.Windows.Forms.ToolStripContentPanel();
			this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			this.toolStripPanel1 = new System.Windows.Forms.ToolStripPanel();
			this.menuMain = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveFractalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.copyToClipboardToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.saveNextPicToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.savePicAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.calculateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.pauseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.zoomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.resetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.seriesInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.parametersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.colorsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.precisionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.doubleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.decimalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.btnRender = new System.Windows.Forms.ToolStripButton();
			this.btnRestart = new System.Windows.Forms.ToolStripButton();
			this.btnRecolor = new System.Windows.Forms.ToolStripButton();
			this.btnAA = new System.Windows.Forms.ToolStripButton();
			this.cboDepth = new System.Windows.Forms.ToolStripComboBox();
			this.lblRenderTime = new System.Windows.Forms.ToolStripLabel();
			this.lblFileName = new System.Windows.Forms.ToolStripLabel();
			((System.ComponentModel.ISupportInitialize)(this.picFractal)).BeginInit();
			this.toolStripPanel1.SuspendLayout();
			this.menuMain.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tmrSeries
			// 
			this.tmrSeries.Enabled = true;
			this.tmrSeries.Interval = 1000;
			this.tmrSeries.Tick += new System.EventHandler(this.tmrSeries_Tick);
			// 
			// picFractal
			// 
			this.picFractal.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.picFractal.BackColor = System.Drawing.Color.Black;
			this.picFractal.Cursor = System.Windows.Forms.Cursors.Default;
			this.picFractal.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.picFractal.ForeColor = System.Drawing.SystemColors.ControlText;
			this.picFractal.Location = new System.Drawing.Point(0, 26);
			this.picFractal.Name = "picFractal";
			this.picFractal.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.picFractal.Size = new System.Drawing.Size(640, 480);
			this.picFractal.TabIndex = 2;
			this.picFractal.TabStop = false;
			this.picFractal.Paint += new System.Windows.Forms.PaintEventHandler(this.picFractal_Paint);
			this.picFractal.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picFractal_MouseDown);
			this.picFractal.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picFractal_MouseMove);
			this.picFractal.MouseUp += new System.Windows.Forms.MouseEventHandler(this.picFractal_MouseUp);
			// 
			// ContentPanel
			// 
			this.ContentPanel.Size = new System.Drawing.Size(640, 32);
			// 
			// openFileDialog1
			// 
			this.openFileDialog1.FileName = "openFileDialog1";
			// 
			// toolStripPanel1
			// 
			this.toolStripPanel1.Controls.Add(this.menuMain);
			this.toolStripPanel1.Controls.Add(this.toolStrip1);
			this.toolStripPanel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.toolStripPanel1.Location = new System.Drawing.Point(0, 0);
			this.toolStripPanel1.Name = "toolStripPanel1";
			this.toolStripPanel1.Orientation = System.Windows.Forms.Orientation.Horizontal;
			this.toolStripPanel1.RowMargin = new System.Windows.Forms.Padding(3, 0, 0, 0);
			this.toolStripPanel1.Size = new System.Drawing.Size(640, 25);

			// 
			// menuMain
			// 
			this.menuMain.CanOverflow = true;
			this.menuMain.Dock = System.Windows.Forms.DockStyle.None;
			this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.fileToolStripMenuItem,
			this.calculateToolStripMenuItem,
			this.zoomToolStripMenuItem,
			this.optionsToolStripMenuItem});
			this.menuMain.Location = new System.Drawing.Point(3, 0);
			this.menuMain.Name = "menuMain";
			this.menuMain.Size = new System.Drawing.Size(225, 24);
			this.menuMain.Stretch = false;
			this.menuMain.TabIndex = 10;
			this.menuMain.TabStop = true;
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.openToolStripMenuItem,
			this.saveFractalToolStripMenuItem,
			this.copyToClipboardToolStripMenuItem,
			this.saveNextPicToolStripMenuItem,
			this.savePicAsToolStripMenuItem,
			this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// openToolStripMenuItem
			// 
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
			// 
			// saveFractalToolStripMenuItem
			// 
			this.saveFractalToolStripMenuItem.Name = "saveFractalToolStripMenuItem";
			this.saveFractalToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.saveFractalToolStripMenuItem.Text = "Save Fractal";
			this.saveFractalToolStripMenuItem.Click += new System.EventHandler(this.saveFractalToolStripMenuItem_Click);
			// 
			// copyToClipboardToolStripMenuItem
			// 
			this.copyToClipboardToolStripMenuItem.Name = "copyToClipboardToolStripMenuItem";
			this.copyToClipboardToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.copyToClipboardToolStripMenuItem.Text = "Copy to Clipboard";
			this.copyToClipboardToolStripMenuItem.Click += new System.EventHandler(this.copyToClipboardToolStripMenuItem_Click);
			// 
			// saveNextPicToolStripMenuItem
			// 
			this.saveNextPicToolStripMenuItem.Name = "saveNextPicToolStripMenuItem";
			this.saveNextPicToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.saveNextPicToolStripMenuItem.Text = "SaveNext Pic";
			this.saveNextPicToolStripMenuItem.Click += new System.EventHandler(this.saveNextPicToolStripMenuItem_Click);
			// 
			// savePicAsToolStripMenuItem
			// 
			this.savePicAsToolStripMenuItem.Name = "savePicAsToolStripMenuItem";
			this.savePicAsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.savePicAsToolStripMenuItem.Text = "Save Pic As";
			this.savePicAsToolStripMenuItem.Click += new System.EventHandler(this.savePicAsToolStripMenuItem_Click);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// calculateToolStripMenuItem
			// 
			this.calculateToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.startToolStripMenuItem,
			this.pauseToolStripMenuItem,
			this.stopToolStripMenuItem});
			this.calculateToolStripMenuItem.Name = "calculateToolStripMenuItem";
			this.calculateToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
			this.calculateToolStripMenuItem.Size = new System.Drawing.Size(68, 20);
			this.calculateToolStripMenuItem.Text = "Calculate";
			// 
			// startToolStripMenuItem
			// 
			this.startToolStripMenuItem.Name = "startToolStripMenuItem";
			this.startToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
			this.startToolStripMenuItem.Text = "Start";
			this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
			// 
			// pauseToolStripMenuItem
			// 
			this.pauseToolStripMenuItem.Name = "pauseToolStripMenuItem";
			this.pauseToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
			this.pauseToolStripMenuItem.Text = "Pause";
			this.pauseToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// stopToolStripMenuItem
			// 
			this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
			this.stopToolStripMenuItem.Size = new System.Drawing.Size(105, 22);
			this.stopToolStripMenuItem.Text = "Stop";
			this.stopToolStripMenuItem.Click += new System.EventHandler(this.pauseToolStripMenuItem_Click);
			// 
			// zoomToolStripMenuItem
			// 
			this.zoomToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.resetToolStripMenuItem,
			this.manualToolStripMenuItem,
			this.seriesInToolStripMenuItem});
			this.zoomToolStripMenuItem.Name = "zoomToolStripMenuItem";
			this.zoomToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
			this.zoomToolStripMenuItem.Size = new System.Drawing.Size(51, 20);
			this.zoomToolStripMenuItem.Text = "Zoom";
			// 
			// resetToolStripMenuItem
			// 
			this.resetToolStripMenuItem.Name = "resetToolStripMenuItem";
			this.resetToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.resetToolStripMenuItem.Text = "Reset to Beginning";
			this.resetToolStripMenuItem.Click += new System.EventHandler(this.resetToolStripMenuItem_Click);
			// 
			// manualToolStripMenuItem
			// 
			this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
			this.manualToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.manualToolStripMenuItem.Text = "Manual";
			this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
			// 
			// seriesInToolStripMenuItem
			// 
			this.seriesInToolStripMenuItem.Name = "seriesInToolStripMenuItem";
			this.seriesInToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
			this.seriesInToolStripMenuItem.Text = "Series In (Auto)";
			this.seriesInToolStripMenuItem.Click += new System.EventHandler(this.seriesInToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.parametersToolStripMenuItem,
			this.colorsToolStripMenuItem,
			this.precisionToolStripMenuItem});
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Overflow = System.Windows.Forms.ToolStripItemOverflow.AsNeeded;
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// parametersToolStripMenuItem
			// 
			this.parametersToolStripMenuItem.Name = "parametersToolStripMenuItem";
			this.parametersToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.parametersToolStripMenuItem.Text = "Parameters";
			this.parametersToolStripMenuItem.Click += new System.EventHandler(this.parametersToolStripMenuItem_Click);
			// 
			// colorsToolStripMenuItem
			// 
			this.colorsToolStripMenuItem.Name = "colorsToolStripMenuItem";
			this.colorsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.colorsToolStripMenuItem.Text = "Colors";
			this.colorsToolStripMenuItem.Click += new System.EventHandler(this.colorsToolStripMenuItem_Click);
			// 
			// precisionToolStripMenuItem
			// 
			this.precisionToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.doubleToolStripMenuItem,
			this.decimalToolStripMenuItem});
			this.precisionToolStripMenuItem.Name = "precisionToolStripMenuItem";
			this.precisionToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
			this.precisionToolStripMenuItem.Text = "Precision";
			// 
			// doubleToolStripMenuItem
			// 
			this.doubleToolStripMenuItem.Name = "doubleToolStripMenuItem";
			this.doubleToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.doubleToolStripMenuItem.Text = "Double";
			this.doubleToolStripMenuItem.Click += new System.EventHandler(this.doubleToolStripMenuItem_Click);
			// 
			// decimalToolStripMenuItem
			// 
			this.decimalToolStripMenuItem.Name = "decimalToolStripMenuItem";
			this.decimalToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
			this.decimalToolStripMenuItem.Text = "Decimal";
			this.decimalToolStripMenuItem.Click += new System.EventHandler(this.decimalToolStripMenuItem_Click);
			// 
			// toolStrip1
			// 
			this.toolStrip1.Dock = System.Windows.Forms.DockStyle.None;
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.btnRender,
			this.btnRestart,
			this.btnRecolor,
			this.btnAA,
			this.cboDepth,
			this.lblRenderTime,
			this.lblFileName});
			this.toolStrip1.Location = new System.Drawing.Point(228, 0);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(344, 25);
			this.toolStrip1.TabIndex = 9;
			// 
			// btnRender
			// 
			this.btnRender.AutoSize = false;
			this.btnRender.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
			this.btnRender.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.btnRender.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnRender.Image = ((System.Drawing.Image)(resources.GetObject("btnRender.Image")));
			this.btnRender.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRender.Name = "btnRender";
			this.btnRender.Size = new System.Drawing.Size(63, 22);
			this.btnRender.Text = "Render";
			this.btnRender.Click += new System.EventHandler(this.btnRender_Click);
			// 
			// btnRestart
			// 
			this.btnRestart.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRestart.Image = global::MtFract.Properties.Resources.SmallMandel;
			this.btnRestart.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRestart.Name = "btnRestart";
			this.btnRestart.Size = new System.Drawing.Size(23, 22);
			this.btnRestart.Text = "Restart";
			this.btnRestart.ToolTipText = " Zoom to entire Mandelbrot";
			this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
			// 
			// btnRecolor
			// 
			this.btnRecolor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnRecolor.Image = global::MtFract.Properties.Resources.color2;
			this.btnRecolor.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnRecolor.Name = "btnRecolor";
			this.btnRecolor.Size = new System.Drawing.Size(23, 22);
			this.btnRecolor.Text = "Refresh";
			this.btnRecolor.ToolTipText = "Refresh Colors";
			this.btnRecolor.Click += new System.EventHandler(this.btnRecolor_Click);
			// 
			// btnAA
			// 
			this.btnAA.CheckOnClick = true;
			this.btnAA.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.btnAA.Image = global::MtFract.Properties.Resources.notAA;
			this.btnAA.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.btnAA.Name = "btnAA";
			this.btnAA.Size = new System.Drawing.Size(23, 22);
			this.btnAA.Text = "Restart";
			this.btnAA.ToolTipText = " Anti-aliasing";
			this.btnAA.Click += new System.EventHandler(this.btnAA_Click);
			// 
			// cboDepth
			// 
			this.cboDepth.Name = "cboDepth";
			this.cboDepth.Size = new System.Drawing.Size(80, 25);
			this.cboDepth.TextChanged += new System.EventHandler(this.cboDepth_TextChanged);
			// 
			// lblRenderTime
			// 
			this.lblRenderTime.Name = "lblRenderTime";
			this.lblRenderTime.Size = new System.Drawing.Size(47, 22);
			this.lblRenderTime.Text = "0.5 secs";
			// 
			// lblFileName
			// 
			this.lblFileName.Name = "lblFileName";
			this.lblFileName.Size = new System.Drawing.Size(71, 22);
			this.lblFileName.Text = "nextSaveFile";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(640, 507);
			this.Controls.Add(this.toolStripPanel1);
			this.Controls.Add(this.picFractal);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmMain";
			this.Text = "MtFract";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmMain_FormClosed);
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.frmMain_MouseWheel);
			this.Resize += new System.EventHandler(this.frmMain_Resize);
			((System.ComponentModel.ISupportInitialize)(this.picFractal)).EndInit();
			this.toolStripPanel1.ResumeLayout(false);
			this.toolStripPanel1.PerformLayout();
			this.menuMain.ResumeLayout(false);
			this.menuMain.PerformLayout();
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Timer tmrSeries;
		public System.Windows.Forms.PictureBox picFractal;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.ToolStripContentPanel ContentPanel;
		private System.Windows.Forms.ToolStripPanel toolStripPanel1;
		private System.Windows.Forms.MenuStrip menuMain;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveFractalToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem copyToClipboardToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem saveNextPicToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem savePicAsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem calculateToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem pauseToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem zoomToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem resetToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem seriesInToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem parametersToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem colorsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem precisionToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem doubleToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem decimalToolStripMenuItem;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripButton btnRender;
		internal System.Windows.Forms.ToolStripButton btnRestart;
		internal System.Windows.Forms.ToolStripButton btnRecolor;
		internal System.Windows.Forms.ToolStripButton btnAA;
		private System.Windows.Forms.ToolStripComboBox cboDepth;
		private System.Windows.Forms.ToolStripLabel lblRenderTime;
		private System.Windows.Forms.ToolStripLabel lblFileName;

	}
}

