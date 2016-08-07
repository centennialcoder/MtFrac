namespace MtFract
{
	partial class frmColorMap
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
			System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmColorMap));
			this.grdColor = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Red = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Green = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Blue = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Color = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmdSmooth = new System.Windows.Forms.Button();
			this.cmdBrowse = new System.Windows.Forms.Button();
			this.txtFile = new System.Windows.Forms.TextBox();
			this.cmdSave = new System.Windows.Forms.Button();
			this.cmdLoad = new System.Windows.Forms.Button();
			this.fraColors = new System.Windows.Forms.GroupBox();
			this.txtCycleStart = new System.Windows.Forms.TextBox();
			this.txtFactor = new System.Windows.Forms.TextBox();
			this.optExponential = new System.Windows.Forms.RadioButton();
			this.optGeometric = new System.Windows.Forms.RadioButton();
			this.optLinear = new System.Windows.Forms.RadioButton();
			this.Label2 = new System.Windows.Forms.Label();
			this.Label1 = new System.Windows.Forms.Label();
			this.cmdHide = new System.Windows.Forms.Button();
			this.cmdAdd = new System.Windows.Forms.Button();
			this.cmdDel = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.grdColor)).BeginInit();
			this.fraColors.SuspendLayout();
			this.SuspendLayout();
			// 
			// grdColor
			// 
			this.grdColor.AllowUserToAddRows = false;
			this.grdColor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
							| System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.grdColor.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
			this.grdColor.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdColor.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Red,
            this.Green,
            this.Blue,
            this.Color});
			this.grdColor.Location = new System.Drawing.Point(2, -1);
			this.grdColor.Name = "grdColor";
			this.grdColor.RowHeadersVisible = false;
			this.grdColor.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.grdColor.Size = new System.Drawing.Size(286, 345);
			this.grdColor.TabIndex = 6;
			this.grdColor.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdColor_DoubleClick);
			this.grdColor.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.grdColor_CellValueChanged);
			// 
			// Column1
			// 
			this.Column1.FillWeight = 40F;
			this.Column1.HeaderText = "#";
			this.Column1.Name = "Column1";
			this.Column1.Width = 40;
			// 
			// Red
			// 
			this.Red.FillWeight = 40F;
			this.Red.HeaderText = "Red";
			this.Red.MaxInputLength = 3;
			this.Red.Name = "Red";
			this.Red.ToolTipText = "0-255";
			this.Red.Width = 40;
			// 
			// Green
			// 
			this.Green.FillWeight = 40F;
			this.Green.HeaderText = "Green";
			this.Green.MaxInputLength = 3;
			this.Green.Name = "Green";
			this.Green.ToolTipText = "0-255";
			this.Green.Width = 40;
			// 
			// Blue
			// 
			dataGridViewCellStyle1.NullValue = null;
			this.Blue.DefaultCellStyle = dataGridViewCellStyle1;
			this.Blue.FillWeight = 40F;
			this.Blue.HeaderText = "Blue";
			this.Blue.MaxInputLength = 3;
			this.Blue.Name = "Blue";
			this.Blue.ToolTipText = "0-255";
			this.Blue.Width = 40;
			// 
			// Color
			// 
			this.Color.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Color.HeaderText = "Color";
			this.Color.MaxInputLength = 8;
			this.Color.Name = "Color";
			this.Color.ToolTipText = "hex number ARGB";
			// 
			// cmdSmooth
			// 
			this.cmdSmooth.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdSmooth.Location = new System.Drawing.Point(137, 350);
			this.cmdSmooth.Name = "cmdSmooth";
			this.cmdSmooth.Size = new System.Drawing.Size(57, 23);
			this.cmdSmooth.TabIndex = 9;
			this.cmdSmooth.Text = "Smooth";
			this.cmdSmooth.UseVisualStyleBackColor = true;
			this.cmdSmooth.Click += new System.EventHandler(this.cmdSmooth_Click);
			// 
			// cmdBrowse
			// 
			this.cmdBrowse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdBrowse.BackColor = System.Drawing.SystemColors.Control;
			this.cmdBrowse.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdBrowse.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdBrowse.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdBrowse.Image = ((System.Drawing.Image)(resources.GetObject("cmdBrowse.Image")));
			this.cmdBrowse.Location = new System.Drawing.Point(265, 377);
			this.cmdBrowse.Name = "cmdBrowse";
			this.cmdBrowse.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdBrowse.Size = new System.Drawing.Size(23, 22);
			this.cmdBrowse.TabIndex = 8;
			this.cmdBrowse.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.cmdBrowse.UseVisualStyleBackColor = false;
			this.cmdBrowse.Click += new System.EventHandler(this.cmdBrowse_Click);
			// 
			// txtFile
			// 
			this.txtFile.AcceptsReturn = true;
			this.txtFile.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.txtFile.BackColor = System.Drawing.SystemColors.Window;
			this.txtFile.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtFile.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFile.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtFile.Location = new System.Drawing.Point(2, 379);
			this.txtFile.MaxLength = 0;
			this.txtFile.Name = "txtFile";
			this.txtFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtFile.Size = new System.Drawing.Size(257, 20);
			this.txtFile.TabIndex = 7;
			// 
			// cmdSave
			// 
			this.cmdSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdSave.BackColor = System.Drawing.SystemColors.Control;
			this.cmdSave.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdSave.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdSave.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdSave.Location = new System.Drawing.Point(95, 405);
			this.cmdSave.Name = "cmdSave";
			this.cmdSave.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdSave.Size = new System.Drawing.Size(96, 23);
			this.cmdSave.TabIndex = 11;
			this.cmdSave.Text = "Save to File";
			this.cmdSave.UseVisualStyleBackColor = false;
			this.cmdSave.Click += new System.EventHandler(this.cmdSave_Click);
			// 
			// cmdLoad
			// 
			this.cmdLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdLoad.BackColor = System.Drawing.SystemColors.Control;
			this.cmdLoad.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdLoad.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdLoad.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdLoad.Location = new System.Drawing.Point(192, 405);
			this.cmdLoad.Name = "cmdLoad";
			this.cmdLoad.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdLoad.Size = new System.Drawing.Size(96, 23);
			this.cmdLoad.TabIndex = 10;
			this.cmdLoad.Text = "Load Color Map";
			this.cmdLoad.UseVisualStyleBackColor = false;
			this.cmdLoad.Click += new System.EventHandler(this.cmdLoad_Click);
			// 
			// fraColors
			// 
			this.fraColors.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
							| System.Windows.Forms.AnchorStyles.Right)));
			this.fraColors.BackColor = System.Drawing.SystemColors.Control;
			this.fraColors.Controls.Add(this.txtCycleStart);
			this.fraColors.Controls.Add(this.txtFactor);
			this.fraColors.Controls.Add(this.optExponential);
			this.fraColors.Controls.Add(this.optGeometric);
			this.fraColors.Controls.Add(this.optLinear);
			this.fraColors.Controls.Add(this.Label2);
			this.fraColors.Controls.Add(this.Label1);
			this.fraColors.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.fraColors.ForeColor = System.Drawing.SystemColors.ControlText;
			this.fraColors.Location = new System.Drawing.Point(2, 428);
			this.fraColors.Name = "fraColors";
			this.fraColors.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.fraColors.Size = new System.Drawing.Size(286, 73);
			this.fraColors.TabIndex = 12;
			this.fraColors.TabStop = false;
			this.fraColors.Text = "Coloring";
			// 
			// txtCycleStart
			// 
			this.txtCycleStart.AcceptsReturn = true;
			this.txtCycleStart.BackColor = System.Drawing.SystemColors.Window;
			this.txtCycleStart.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtCycleStart.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtCycleStart.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtCycleStart.Location = new System.Drawing.Point(15, 42);
			this.txtCycleStart.MaxLength = 0;
			this.txtCycleStart.Name = "txtCycleStart";
			this.txtCycleStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtCycleStart.Size = new System.Drawing.Size(53, 20);
			this.txtCycleStart.TabIndex = 9;
			this.txtCycleStart.Text = "1";
			this.txtCycleStart.Validating += new System.ComponentModel.CancelEventHandler(this.txtCycleStart_Validating);
			// 
			// txtFactor
			// 
			this.txtFactor.AcceptsReturn = true;
			this.txtFactor.BackColor = System.Drawing.SystemColors.Window;
			this.txtFactor.Cursor = System.Windows.Forms.Cursors.IBeam;
			this.txtFactor.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtFactor.ForeColor = System.Drawing.SystemColors.WindowText;
			this.txtFactor.Location = new System.Drawing.Point(74, 42);
			this.txtFactor.MaxLength = 0;
			this.txtFactor.Name = "txtFactor";
			this.txtFactor.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.txtFactor.Size = new System.Drawing.Size(67, 20);
			this.txtFactor.TabIndex = 4;
			this.txtFactor.Text = "1";
			this.txtFactor.Validating += new System.ComponentModel.CancelEventHandler(this.txtFactor_Validating);
			// 
			// optExponential
			// 
			this.optExponential.AutoSize = true;
			this.optExponential.BackColor = System.Drawing.SystemColors.Control;
			this.optExponential.Cursor = System.Windows.Forms.Cursors.Default;
			this.optExponential.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optExponential.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optExponential.Location = new System.Drawing.Point(147, 43);
			this.optExponential.Name = "optExponential";
			this.optExponential.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optExponential.Size = new System.Drawing.Size(140, 18);
			this.optExponential.TabIndex = 3;
			this.optExponential.TabStop = true;
			this.optExponential.Text = "Exponential: log basef n";
			this.optExponential.UseVisualStyleBackColor = false;
			this.optExponential.CheckedChanged += new System.EventHandler(this.optExponential_CheckedChanged);
			// 
			// optGeometric
			// 
			this.optGeometric.AutoSize = true;
			this.optGeometric.BackColor = System.Drawing.SystemColors.Control;
			this.optGeometric.Cursor = System.Windows.Forms.Cursors.Default;
			this.optGeometric.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optGeometric.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optGeometric.Location = new System.Drawing.Point(147, 27);
			this.optGeometric.Name = "optGeometric";
			this.optGeometric.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optGeometric.Size = new System.Drawing.Size(118, 18);
			this.optGeometric.TabIndex = 2;
			this.optGeometric.TabStop = true;
			this.optGeometric.Text = "Geometric: n ^ (1/f)";
			this.optGeometric.UseVisualStyleBackColor = false;
			this.optGeometric.CheckedChanged += new System.EventHandler(this.optGeometric_CheckedChanged);
			// 
			// optLinear
			// 
			this.optLinear.AutoSize = true;
			this.optLinear.BackColor = System.Drawing.SystemColors.Control;
			this.optLinear.Cursor = System.Windows.Forms.Cursors.Default;
			this.optLinear.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.optLinear.ForeColor = System.Drawing.SystemColors.ControlText;
			this.optLinear.Location = new System.Drawing.Point(147, 11);
			this.optLinear.Name = "optLinear";
			this.optLinear.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.optLinear.Size = new System.Drawing.Size(80, 18);
			this.optLinear.TabIndex = 1;
			this.optLinear.TabStop = true;
			this.optLinear.Text = "Linear:   n/f";
			this.optLinear.UseVisualStyleBackColor = false;
			this.optLinear.CheckedChanged += new System.EventHandler(this.optLinear_CheckedChanged);
			// 
			// Label2
			// 
			this.Label2.AutoSize = true;
			this.Label2.BackColor = System.Drawing.SystemColors.Control;
			this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label2.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label2.Location = new System.Drawing.Point(6, 25);
			this.Label2.Name = "Label2";
			this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label2.Size = new System.Drawing.Size(62, 14);
			this.Label2.TabIndex = 8;
			this.Label2.Text = "Linear Thru";
			// 
			// Label1
			// 
			this.Label1.BackColor = System.Drawing.SystemColors.Control;
			this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
			this.Label1.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.Label1.Location = new System.Drawing.Point(74, 25);
			this.Label1.Name = "Label1";
			this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.Label1.Size = new System.Drawing.Size(41, 13);
			this.Label1.TabIndex = 5;
			this.Label1.Text = "Factor";
			// 
			// cmdHide
			// 
			this.cmdHide.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdHide.BackColor = System.Drawing.SystemColors.Control;
			this.cmdHide.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdHide.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdHide.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdHide.Location = new System.Drawing.Point(21, 405);
			this.cmdHide.Name = "cmdHide";
			this.cmdHide.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdHide.Size = new System.Drawing.Size(68, 23);
			this.cmdHide.TabIndex = 13;
			this.cmdHide.Text = "Hide";
			this.cmdHide.UseVisualStyleBackColor = false;
			this.cmdHide.Click += new System.EventHandler(this.cmdHide_Click);
			// 
			// cmdAdd
			// 
			this.cmdAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdAdd.Location = new System.Drawing.Point(2, 350);
			this.cmdAdd.Name = "cmdAdd";
			this.cmdAdd.Size = new System.Drawing.Size(43, 23);
			this.cmdAdd.TabIndex = 14;
			this.cmdAdd.Text = "Add";
			this.cmdAdd.UseVisualStyleBackColor = true;
			this.cmdAdd.Click += new System.EventHandler(this.cmdAdd_Click);
			// 
			// cmdDel
			// 
			this.cmdDel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.cmdDel.Location = new System.Drawing.Point(51, 350);
			this.cmdDel.Name = "cmdDel";
			this.cmdDel.Size = new System.Drawing.Size(43, 23);
			this.cmdDel.TabIndex = 15;
			this.cmdDel.Text = "Del";
			this.cmdDel.UseVisualStyleBackColor = true;
			this.cmdDel.Click += new System.EventHandler(this.cmdDel_Click);
			// 
			// frmColorMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(290, 501);
			this.Controls.Add(this.cmdDel);
			this.Controls.Add(this.cmdAdd);
			this.Controls.Add(this.cmdHide);
			this.Controls.Add(this.fraColors);
			this.Controls.Add(this.grdColor);
			this.Controls.Add(this.cmdSmooth);
			this.Controls.Add(this.cmdBrowse);
			this.Controls.Add(this.txtFile);
			this.Controls.Add(this.cmdSave);
			this.Controls.Add(this.cmdLoad);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmColorMap";
			this.Text = "frmColorMap";
			((System.ComponentModel.ISupportInitialize)(this.grdColor)).EndInit();
			this.fraColors.ResumeLayout(false);
			this.fraColors.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		internal System.Windows.Forms.DataGridView grdColor;
		internal System.Windows.Forms.Button cmdSmooth;
		public System.Windows.Forms.Button cmdBrowse;
		public System.Windows.Forms.TextBox txtFile;
		public System.Windows.Forms.Button cmdSave;
		public System.Windows.Forms.Button cmdLoad;
		public System.Windows.Forms.GroupBox fraColors;
		public System.Windows.Forms.TextBox txtCycleStart;
		public System.Windows.Forms.TextBox txtFactor;
		public System.Windows.Forms.RadioButton optExponential;
		public System.Windows.Forms.RadioButton optGeometric;
		public System.Windows.Forms.RadioButton optLinear;
		public System.Windows.Forms.Label Label2;
		public System.Windows.Forms.Label Label1;
		public System.Windows.Forms.Button cmdHide;
		internal System.Windows.Forms.Button cmdAdd;
		internal System.Windows.Forms.Button cmdDel;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Red;
		private System.Windows.Forms.DataGridViewTextBoxColumn Green;
		private System.Windows.Forms.DataGridViewTextBoxColumn Blue;
		private System.Windows.Forms.DataGridViewTextBoxColumn Color;
	}
}