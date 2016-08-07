namespace MtFract
{
	partial class frmParameters
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmParameters));
			this.grdStats = new System.Windows.Forms.DataGridView();
			this.Values = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Data = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.cmdOK = new System.Windows.Forms.Button();
			this.tmrParams = new System.Windows.Forms.Timer(this.components);
			((System.ComponentModel.ISupportInitialize)(this.grdStats)).BeginInit();
			this.SuspendLayout();
			// 
			// grdStats
			// 
			this.grdStats.AllowUserToAddRows = false;
			this.grdStats.AllowUserToDeleteRows = false;
			this.grdStats.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grdStats.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.grdStats.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Values,
            this.Data});
			this.grdStats.Location = new System.Drawing.Point(0, 2);
			this.grdStats.Name = "grdStats";
			this.grdStats.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
			this.grdStats.RowHeadersVisible = false;
			this.grdStats.RowHeadersWidth = 80;
			this.grdStats.Size = new System.Drawing.Size(402, 322);
			this.grdStats.TabIndex = 14;
			// 
			// Values
			// 
			this.Values.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.None;
			this.Values.HeaderText = "Label";
			this.Values.Name = "Values";
			// 
			// Data
			// 
			this.Data.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.Data.HeaderText = "Data";
			this.Data.Name = "Data";
			// 
			// cmdOK
			// 
			this.cmdOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.cmdOK.BackColor = System.Drawing.SystemColors.Control;
			this.cmdOK.Cursor = System.Windows.Forms.Cursors.Default;
			this.cmdOK.Font = new System.Drawing.Font("Arial", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.cmdOK.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cmdOK.Location = new System.Drawing.Point(315, 330);
			this.cmdOK.Name = "cmdOK";
			this.cmdOK.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.cmdOK.Size = new System.Drawing.Size(77, 29);
			this.cmdOK.TabIndex = 15;
			this.cmdOK.Text = "OK";
			this.cmdOK.UseVisualStyleBackColor = false;
			this.cmdOK.Click += new System.EventHandler(this.cmdOK_Click);
			// 
			// tmrParams
			// 
			this.tmrParams.Interval = 1000;
			this.tmrParams.Tick += new System.EventHandler(this.tmrParams_Tick);
			// 
			// frmParameters
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(404, 365);
			this.Controls.Add(this.cmdOK);
			this.Controls.Add(this.grdStats);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "frmParameters";
			this.Text = "frmParameters";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmParameters_FormClosed);
			this.Load += new System.EventHandler(this.frmParameters_Load);
			((System.ComponentModel.ISupportInitialize)(this.grdStats)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		internal System.Windows.Forms.DataGridView grdStats;
		internal System.Windows.Forms.DataGridViewTextBoxColumn Values;
		internal System.Windows.Forms.DataGridViewTextBoxColumn Data;
		public System.Windows.Forms.Button cmdOK;
		private System.Windows.Forms.Timer tmrParams;
	}
}