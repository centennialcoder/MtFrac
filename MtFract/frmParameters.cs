using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MtFract
{
	public partial class frmParameters : Form
	{

		public const int NAME_ROW = 0;
		public const int WINDOW_SIZE_ROW = 1;
		public const int PIXELS_ROW = 2;
		public const int RENDERTIME_ROW = 3;
		public const int MOUSEPT_ROW = 4;
		public const int COMPLEXPT_ROW = 5;
		public const int REAL_ROW = 6;
		public const int IMAGINARY_ROW = 7;
		public const int WIDTH_ROW = 8;
		public const int HEIGHT_ROW = 9;
		public const int SERIES_CENTER_X_ROW = 10;
		public const int SERIES_CENTER_Y_ROW = 11;
		public const int SERIES_PAN_ROW =12;
		public const int ZOOMVAL_ROW = 13;
		public const int PAN_ROW = 14;

		Params _params;
		frmMain _frmMain;

		#region Components

		public frmParameters()
		{
			InitializeComponent();
		}

		//constructor
		public frmParameters(Params myParams, frmMain Caller)
		{
			InitializeComponent();

			//store parameters
			_params = myParams;
			_frmMain = Caller;
			RefreshStats();
		}

		private void frmParameters_FormClosed(object sender, FormClosedEventArgs e)
		{
			tmrParams.Enabled = false;
		}

		private void frmParameters_Load(object sender, EventArgs e)
		{
			tmrParams.Enabled = true;
		}

			private void cmdOK_Click(object sender, EventArgs e)
			{
				tmrParams.Enabled = false;
				this.Hide();
			}

			private void tmrParams_Tick(object sender, EventArgs e)
			{
				RefreshStats();
			}

		#endregion //Components

		#region Methods

			//Show form
			public void ShowParams()
			{
				this.Show();
				tmrParams.Enabled = true;
			}


			private void RefreshStats()
			{
				try
				{
					//add rows
					while (grdStats.RowCount < 15)
					{
						grdStats.Rows.Add();
					}
					grdStats[0, NAME_ROW].Value = "Name";
					grdStats[0, WINDOW_SIZE_ROW].Value = "Picture Size";
					grdStats[0, PIXELS_ROW].Value = "Pixels";
					grdStats[0, RENDERTIME_ROW].Value = "Render Time";
					grdStats[0, MOUSEPT_ROW].Value = "Mouse Pt";
					grdStats[0, COMPLEXPT_ROW].Value = "Complex Pt";
					grdStats[0, REAL_ROW].Value = "Real";
					grdStats[0, IMAGINARY_ROW].Value = "Imaginary";
					grdStats[0, WIDTH_ROW].Value = "Width";
					grdStats[0, HEIGHT_ROW].Value = "Height";
					grdStats[0, SERIES_CENTER_X_ROW].Value = "Series Center Real";
					grdStats[0, SERIES_CENTER_Y_ROW].Value = "Series Center Imaginary";
					grdStats[0, SERIES_PAN_ROW].Value = "Series Pan Pixels";
					grdStats[0, ZOOMVAL_ROW].Value = "Zoom";
					grdStats[0, PAN_ROW].Value = "Pan Offset";

					grdStats[1, NAME_ROW].Value = _frmMain.Series.NextSaveFileShort;
					grdStats[1, WINDOW_SIZE_ROW].Value = string.Format("{0}, {1}", _params.WindowSize.Width, _params.WindowSize.Height);
					if (_frmMain.Plot != null)
					{
						grdStats[1, PIXELS_ROW].Value = _frmMain.Plot.NumPixelString;
						grdStats[1, RENDERTIME_ROW].Value = _frmMain.Plot.RenderTimeString;
						grdStats[1, REAL_ROW].Value = RangeDisplay(_frmMain.Plot.ComplexRect.x0D, _frmMain.Plot.ComplexRect.bottomRightD.x);
						grdStats[1, IMAGINARY_ROW].Value = RangeDisplay(_frmMain.Plot.ComplexRect.y0D, _frmMain.Plot.ComplexRect.bottomRightD.y);
						grdStats[1, WIDTH_ROW].Value = NumDisplay(_frmMain.Plot.ComplexRect.widthD);
						grdStats[1, HEIGHT_ROW].Value = NumDisplay(_frmMain.Plot.ComplexRect.heightD);
					}
					grdStats[1, MOUSEPT_ROW].Value = _params.MousePt.ToString();
					grdStats[1, COMPLEXPT_ROW].Value = _params.ComplexPt.ToString();
					grdStats[1, SERIES_CENTER_X_ROW].Value = NumDisplay(_frmMain.Series.ZoomCenter.xD);
					grdStats[1, SERIES_CENTER_Y_ROW].Value = NumDisplay(_frmMain.Series.ZoomCenter.yD);
					grdStats[1, SERIES_PAN_ROW].Value = _frmMain.Series.PanPix;
					grdStats[1, ZOOMVAL_ROW].Value = string.Format("{0:0.00}",_frmMain.Series.ZoomVal);
					grdStats[1, PAN_ROW].Value = _frmMain.Series.PanPix;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			public string NumDisplay(decimal DecNum)
			{
				string sNum = null;
				string sOut = null;

				try
				{
					sNum = String.Format("{0:+0.0000000000000000000000000e00;-0.0000000000000000000000000e00}", DecNum);
					sOut = sNum.Substring(0, 8) + " " +
						sNum.Substring(8, 5) + " " +
						sNum.Substring(14, 5) + " " +
						sNum.Substring(19, 5) + " " +
						sNum.Substring(24, 5) + " " +
						sNum.Substring(29);
					return sOut;

				}
				catch (Exception ex)
				{
					cErrlog.Log(ex);
					return DecNum.ToString();
				}
			}

			public string RangeDisplay(decimal from, decimal to)
			{
				return string.Format("{0} to {1}", NumDisplay(from), NumDisplay(to));
			}

		#endregion //Methods

		#region Event Handlers

			private void ParamsChanged(object sender, EventArgs e)
			{
				//Params have changed, time to update display
				RefreshStats();
			}

		#endregion //Event Handlers

	}
}
