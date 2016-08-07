namespace MtFract
{
	partial class frmSeries
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSeries));
            this.txtZoomValue = new System.Windows.Forms.TextBox();
            this.txtImaginary = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtReal = new System.Windows.Forms.TextBox();
            this.cmdGetCenter = new System.Windows.Forms.Button();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.cmdBrowse = new System.Windows.Forms.Button();
            this.Label1 = new System.Windows.Forms.Label();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.cmdOK = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.txtFilePrefix = new System.Windows.Forms.TextBox();
            this.txtNextNumber = new System.Windows.Forms.TextBox();
            this.pnlZoomType = new System.Windows.Forms.Panel();
            this.optManual = new System.Windows.Forms.RadioButton();
            this.optSeriesZoomPan = new System.Windows.Forms.RadioButton();
            this.optSeriesZoomOut = new System.Windows.Forms.RadioButton();
            this.optSeriesZoomIn = new System.Windows.Forms.RadioButton();
            this.Label6 = new System.Windows.Forms.Label();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtPanPixels = new System.Windows.Forms.TextBox();
            this.pnlPicType = new System.Windows.Forms.Panel();
            this.optBmp = new System.Windows.Forms.RadioButton();
            this.optPng = new System.Windows.Forms.RadioButton();
            this.pnlZoomType.SuspendLayout();
            this.pnlPicType.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtZoomValue
            // 
            this.txtZoomValue.Location = new System.Drawing.Point(230, 53);
            this.txtZoomValue.Name = "txtZoomValue";
            this.txtZoomValue.Size = new System.Drawing.Size(52, 20);
            this.txtZoomValue.TabIndex = 35;
            this.txtZoomValue.Validating += new System.ComponentModel.CancelEventHandler(this.txtZoomValue_Validating);
            // 
            // txtImaginary
            // 
            this.txtImaginary.Location = new System.Drawing.Point(58, 163);
            this.txtImaginary.Name = "txtImaginary";
            this.txtImaginary.Size = new System.Drawing.Size(234, 20);
            this.txtImaginary.TabIndex = 31;
            // 
            // Label4
            // 
            this.Label4.AutoSize = true;
            this.Label4.Location = new System.Drawing.Point(4, 166);
            this.Label4.Name = "Label4";
            this.Label4.Size = new System.Drawing.Size(52, 13);
            this.Label4.TabIndex = 30;
            this.Label4.Text = "Imaginary";
            // 
            // Label3
            // 
            this.Label3.AutoSize = true;
            this.Label3.Location = new System.Drawing.Point(4, 139);
            this.Label3.Name = "Label3";
            this.Label3.Size = new System.Drawing.Size(29, 13);
            this.Label3.TabIndex = 29;
            this.Label3.Text = "Real";
            // 
            // txtReal
            // 
            this.txtReal.Location = new System.Drawing.Point(58, 137);
            this.txtReal.Name = "txtReal";
            this.txtReal.Size = new System.Drawing.Size(234, 20);
            this.txtReal.TabIndex = 28;
            // 
            // cmdGetCenter
            // 
            this.cmdGetCenter.Location = new System.Drawing.Point(134, 104);
            this.cmdGetCenter.Name = "cmdGetCenter";
            this.cmdGetCenter.Size = new System.Drawing.Size(142, 27);
            this.cmdGetCenter.TabIndex = 27;
            this.cmdGetCenter.Text = "Get Center from Picture";
            this.cmdGetCenter.UseVisualStyleBackColor = true;
            this.cmdGetCenter.Click += new System.EventHandler(this.cmdGetCenter_Click);
            // 
            // Label2
            // 
            this.Label2.AutoSize = true;
            this.Label2.Location = new System.Drawing.Point(7, 191);
            this.Label2.Name = "Label2";
            this.Label2.Size = new System.Drawing.Size(193, 13);
            this.Label2.TabIndex = 26;
            this.Label2.Text = "Mandelbrot files will be save in this path";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(10, 207);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(256, 20);
            this.txtFilePath.TabIndex = 25;
            // 
            // cmdBrowse
            // 
            this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
            this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdBrowse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
            this.cmdBrowse.Location = new System.Drawing.Point(269, 205);
            this.cmdBrowse.Name = "cmdBrowse";
            this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdBrowse.Size = new System.Drawing.Size(23, 22);
            this.cmdBrowse.TabIndex = 24;
            this.cmdBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdBrowse.UseVisualStyleBackColor = false;
            this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
            // 
            // Label1
            // 
            this.Label1.AutoSize = true;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.Location = new System.Drawing.Point(12, 9);
            this.Label1.Name = "Label1";
            this.Label1.Size = new System.Drawing.Size(161, 16);
            this.Label1.TabIndex = 21;
            this.Label1.Text = "Save a Series of bmp.";
            // 
            // cmdCancel
            // 
            this.cmdCancel.Location = new System.Drawing.Point(134, 329);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.Size = new System.Drawing.Size(77, 27);
            this.cmdCancel.TabIndex = 19;
            this.cmdCancel.Text = "Cancel";
            this.cmdCancel.UseVisualStyleBackColor = true;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // cmdOK
            // 
            this.cmdOK.Location = new System.Drawing.Point(217, 329);
            this.cmdOK.Name = "cmdOK";
            this.cmdOK.Size = new System.Drawing.Size(77, 27);
            this.cmdOK.TabIndex = 18;
            this.cmdOK.Text = "OK";
            this.cmdOK.UseVisualStyleBackColor = true;
            this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(71, 235);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(52, 13);
            this.label7.TabIndex = 37;
            this.label7.Text = "File Prefix";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(12, 260);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(111, 13);
            this.label8.TabIndex = 38;
            this.label8.Text = "Next Number (0-9999)";
            // 
            // txtFilePrefix
            // 
            this.txtFilePrefix.Location = new System.Drawing.Point(134, 232);
            this.txtFilePrefix.Name = "txtFilePrefix";
            this.txtFilePrefix.Size = new System.Drawing.Size(132, 20);
            this.txtFilePrefix.TabIndex = 39;
            // 
            // txtNextNumber
            // 
            this.txtNextNumber.Location = new System.Drawing.Point(134, 257);
            this.txtNextNumber.Name = "txtNextNumber";
            this.txtNextNumber.Size = new System.Drawing.Size(132, 20);
            this.txtNextNumber.TabIndex = 40;
            this.txtNextNumber.Validating += new System.ComponentModel.CancelEventHandler(this.txtNextNumber_Validating);
            // 
            // pnlZoomType
            // 
            this.pnlZoomType.Controls.Add(this.optManual);
            this.pnlZoomType.Controls.Add(this.optSeriesZoomPan);
            this.pnlZoomType.Controls.Add(this.optSeriesZoomOut);
            this.pnlZoomType.Controls.Add(this.optSeriesZoomIn);
            this.pnlZoomType.Location = new System.Drawing.Point(15, 28);
            this.pnlZoomType.Name = "pnlZoomType";
            this.pnlZoomType.Size = new System.Drawing.Size(108, 75);
            this.pnlZoomType.TabIndex = 43;
            // 
            // optManual
            // 
            this.optManual.AutoSize = true;
            this.optManual.Location = new System.Drawing.Point(3, 3);
            this.optManual.Name = "optManual";
            this.optManual.Size = new System.Drawing.Size(60, 17);
            this.optManual.TabIndex = 40;
            this.optManual.TabStop = true;
            this.optManual.Text = "Manual";
            this.optManual.UseVisualStyleBackColor = true;
            // 
            // optSeriesZoomPan
            // 
            this.optSeriesZoomPan.AutoSize = true;
            this.optSeriesZoomPan.Location = new System.Drawing.Point(3, 57);
            this.optSeriesZoomPan.Name = "optSeriesZoomPan";
            this.optSeriesZoomPan.Size = new System.Drawing.Size(44, 17);
            this.optSeriesZoomPan.TabIndex = 39;
            this.optSeriesZoomPan.TabStop = true;
            this.optSeriesZoomPan.Text = "Pan";
            this.optSeriesZoomPan.UseVisualStyleBackColor = true;
            // 
            // optSeriesZoomOut
            // 
            this.optSeriesZoomOut.AutoSize = true;
            this.optSeriesZoomOut.Location = new System.Drawing.Point(3, 39);
            this.optSeriesZoomOut.Name = "optSeriesZoomOut";
            this.optSeriesZoomOut.Size = new System.Drawing.Size(72, 17);
            this.optSeriesZoomOut.TabIndex = 38;
            this.optSeriesZoomOut.TabStop = true;
            this.optSeriesZoomOut.Text = "Zoom Out";
            this.optSeriesZoomOut.UseVisualStyleBackColor = true;
            // 
            // optSeriesZoomIn
            // 
            this.optSeriesZoomIn.AutoSize = true;
            this.optSeriesZoomIn.Location = new System.Drawing.Point(3, 21);
            this.optSeriesZoomIn.Name = "optSeriesZoomIn";
            this.optSeriesZoomIn.Size = new System.Drawing.Size(64, 17);
            this.optSeriesZoomIn.TabIndex = 37;
            this.optSeriesZoomIn.TabStop = true;
            this.optSeriesZoomIn.Text = "Zoom In";
            this.optSeriesZoomIn.UseVisualStyleBackColor = true;
            // 
            // Label6
            // 
            this.Label6.AutoSize = true;
            this.Label6.Location = new System.Drawing.Point(157, 56);
            this.Label6.Name = "Label6";
            this.Label6.Size = new System.Drawing.Size(67, 13);
            this.Label6.TabIndex = 46;
            this.Label6.Text = "Zoom Value:";
            // 
            // Label5
            // 
            this.Label5.AutoSize = true;
            this.Label5.Location = new System.Drawing.Point(165, 33);
            this.Label5.Name = "Label5";
            this.Label5.Size = new System.Drawing.Size(59, 13);
            this.Label5.TabIndex = 45;
            this.Label5.Text = "Pan Pixels:";
            // 
            // txtPanPixels
            // 
            this.txtPanPixels.Location = new System.Drawing.Point(230, 30);
            this.txtPanPixels.MaxLength = 4;
            this.txtPanPixels.Name = "txtPanPixels";
            this.txtPanPixels.Size = new System.Drawing.Size(52, 20);
            this.txtPanPixels.TabIndex = 44;
            // 
            // pnlPicType
            // 
            this.pnlPicType.Controls.Add(this.optPng);
            this.pnlPicType.Controls.Add(this.optBmp);
            this.pnlPicType.Location = new System.Drawing.Point(134, 280);
            this.pnlPicType.Name = "pnlPicType";
            this.pnlPicType.Size = new System.Drawing.Size(72, 43);
            this.pnlPicType.TabIndex = 47;
            // 
            // optBmp
            // 
            this.optBmp.AutoSize = true;
            this.optBmp.Location = new System.Drawing.Point(3, 3);
            this.optBmp.Name = "optBmp";
            this.optBmp.Size = new System.Drawing.Size(45, 17);
            this.optBmp.TabIndex = 42;
            this.optBmp.TabStop = true;
            this.optBmp.Text = "bmp";
            this.optBmp.UseVisualStyleBackColor = true;
            // 
            // optPng
            // 
            this.optPng.AutoSize = true;
            this.optPng.Location = new System.Drawing.Point(3, 22);
            this.optPng.Name = "optPng";
            this.optPng.Size = new System.Drawing.Size(43, 17);
            this.optPng.TabIndex = 43;
            this.optPng.TabStop = true;
            this.optPng.Text = "png";
            this.optPng.UseVisualStyleBackColor = true;
            // 
            // frmSeries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 358);
            this.Controls.Add(this.pnlPicType);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtPanPixels);
            this.Controls.Add(this.pnlZoomType);
            this.Controls.Add(this.txtNextNumber);
            this.Controls.Add(this.txtFilePrefix);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtZoomValue);
            this.Controls.Add(this.txtImaginary);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtReal);
            this.Controls.Add(this.cmdGetCenter);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtFilePath);
            this.Controls.Add(this.cmdBrowse);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.cmdCancel);
            this.Controls.Add(this.cmdOK);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmSeries";
            this.Text = "Render a Series";
            this.pnlZoomType.ResumeLayout(false);
            this.pnlZoomType.PerformLayout();
            this.pnlPicType.ResumeLayout(false);
            this.pnlPicType.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        internal System.Windows.Forms.TextBox txtZoomValue;
		internal System.Windows.Forms.TextBox txtImaginary;
		internal System.Windows.Forms.Label Label4;
		internal System.Windows.Forms.Label Label3;
		internal System.Windows.Forms.TextBox txtReal;
		internal System.Windows.Forms.Button cmdGetCenter;
		internal System.Windows.Forms.Label Label2;
		internal System.Windows.Forms.TextBox txtFilePath;
        public System.Windows.Forms.Button cmdBrowse;
        internal System.Windows.Forms.Label Label1;
		internal System.Windows.Forms.Button cmdCancel;
        internal System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Label label8;
		internal System.Windows.Forms.TextBox txtFilePrefix;
        internal System.Windows.Forms.TextBox txtNextNumber;
        private System.Windows.Forms.Panel pnlZoomType;
        internal System.Windows.Forms.RadioButton optManual;
        internal System.Windows.Forms.RadioButton optSeriesZoomPan;
        internal System.Windows.Forms.RadioButton optSeriesZoomOut;
        internal System.Windows.Forms.RadioButton optSeriesZoomIn;
        internal System.Windows.Forms.Label Label6;
        internal System.Windows.Forms.Label Label5;
        internal System.Windows.Forms.TextBox txtPanPixels;
        private System.Windows.Forms.Panel pnlPicType;
        private System.Windows.Forms.RadioButton optPng;
        private System.Windows.Forms.RadioButton optBmp;
	}
}