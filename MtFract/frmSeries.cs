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
	public partial class frmSeries : Form
	{
		private SeriesData m_data;
		private const string DEC_FORMAT = "0.0000000000000000000000000000E+00";

		#region frmSeries

			public frmSeries()
			{
				InitializeComponent();
			}

			public frmSeries(SeriesData data): base()
			{
				InitializeComponent();
                m_data = data;
				RefreshData(data);
			}

			private void cmdGetCenter_Click(object sender, EventArgs e)
			{
				//return control to frmMain, modify SeriesData to indicate that we are waiting for centerpoint
				m_data.bWaitingForCenter = true;
				this.Hide();
			}

			private void cmdBrowse_Click(object sender, EventArgs e)
			{
				FolderBrowserDialog folderPicker = new FolderBrowserDialog();

				try
				{
					//setup folderPicker	
					folderPicker.ShowNewFolderButton = true;
					folderPicker.Description = "Select the path to save the Mandelbrot image files";

					//set directory
					if (System.IO.Directory.Exists(m_data.SavePath))
					{
						folderPicker.SelectedPath = m_data.SavePath;
					}
					else
					{
						folderPicker.SelectedPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
					}

					//open FolderBrowserdialog
					DialogResult result = folderPicker.ShowDialog();
					if (result == DialogResult.OK)
					{
						txtFilePath.Text = folderPicker.SelectedPath;
					}
					
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void txtPanPixels_Validating(object sender, CancelEventArgs e)
			{
				int iTest;
				try
				{
					if (!int.TryParse(txtPanPixels.Text, out iTest))
					{
						//not an integer
						MessageBox.Show("Invalid Pan value", "Must be an non-negative integer (0-1000)", MessageBoxButtons.OK);
						txtPanPixels.Text = "0";
						e.Cancel = true;
					}
					else if (iTest < 0)
					{
						MessageBox.Show("Invalid Pan value", "Must be an non-negative integer (0-1000)", MessageBoxButtons.OK);
						txtPanPixels.Text = "0";
						e.Cancel = true;
					}
				}
				catch(Exception ex){cErrlog.Log(ex);}
			}

			private void txtZoomValue_Validating(object sender, CancelEventArgs e)
			{
				double dTest = 1.0;

				try
				{
					if (!double.TryParse(txtZoomValue.Text, out dTest))
					{
						//not a float
						MessageBox.Show("Invalid Zoom Value", "Must be non-zero positive real number (0.001 - 1000)", MessageBoxButtons.OK);
						txtZoomValue.Text = "1.0";
						e.Cancel = true;
					}
					else if (dTest == 0.0)
					{
						MessageBox.Show("Invalid Zoom value", "Must be non-zero positive real number (0.001 - 1000)", MessageBoxButtons.OK);
						txtZoomValue.Text = "1.0";
						e.Cancel = true;
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void txtNextNumber_Validating(object sender, CancelEventArgs e)
			{
				int iTest = 1;
				try
				{
					if (!int.TryParse(txtNextNumber.Text, out iTest))
					{
						MessageBox.Show("Invalid Next Number", "Must be non-zero integer (0 - 10000)", MessageBoxButtons.OK);
						txtNextNumber.Text = "1";
						e.Cancel = true;
					}
				}
				catch(Exception ex){cErrlog.Log(ex);}
			}

			private void cmdCancel_Click(object sender, EventArgs e)
			{
				this.Close();
			}

			private void cmdOK_Click(object sender, EventArgs e)
			{
				//Save all Data and save in m_data
				try
				{
					//ZoomType
					if (optManual.Checked)
						m_data.ZoomType = eZoomType.Manual;
					else if (optSeriesZoomIn.Checked)
						m_data.ZoomType = eZoomType.SeriesIn;
					else if (optSeriesZoomOut.Checked)
						m_data.ZoomType = eZoomType.SeriesOut;
					else if (optSeriesZoomPan.Checked)
						m_data.ZoomType = eZoomType.SeriesPan;

					//Pan Zoom
					int iTest;
					if (int.TryParse(txtPanPixels.Text, out iTest))
					{
						if (iTest > 0)
							m_data.PanPix = iTest;
					}
					double dTest;
					if (double.TryParse(txtZoomValue.Text, out dTest))
					{
						if (dTest != 0d)
						{
							m_data.ZoomVal = dTest;
						}
					}

					//Center
					decimal decTest;
					if (decimal.TryParse(txtReal.Text, out decTest))
					{
						m_data.ZoomCenter.xD = decTest;
					}
					if (decimal.TryParse(txtImaginary.Text, out decTest))
					{
						m_data.ZoomCenter.yD = decTest;
					}

					//Save Files
					m_data.SavePath = txtFilePath.Text;
					m_data.SavePrefix = txtFilePrefix.Text;
					if (int.TryParse(txtNextNumber.Text, out iTest))
					{
						m_data.NextSaveNum = iTest;
					}
                    if (optPng.Checked)
                        m_data.Extension = ".png";
                    else
                        m_data.Extension = ".bmp";

					this.Hide();
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}


		#endregion //frmSeries

		#region //Methods

			public void RefreshData(SeriesData formData)
			{
				//constructor with initial Data
				m_data = formData;
				try
				{
					//load form from the data

					//Zoomtype
					switch (m_data.ZoomType)
					{
						case eZoomType.Manual:
							optManual.Checked = true;
							//txtZoomValue.Text = "1.0";
							break;

						case eZoomType.SeriesIn:
							optSeriesZoomIn.Checked = true;
							txtZoomValue.Text = m_data.ZoomVal.ToString();
							break;

						case eZoomType.SeriesOut:
							optSeriesZoomOut.Checked = true;
							txtZoomValue.Text = m_data.ZoomVal.ToString();
							break;

						case eZoomType.SeriesPan:
							optSeriesZoomPan.Checked = true;
							txtZoomValue.Text = "1.0";
							break;

						default:
							optManual.Checked = true;
							txtZoomValue.Text = "1.0";
							break;
					}

					//Pan Pixels
					txtPanPixels.Text = m_data.PanPix.ToString();

					//Center
					txtReal.Text = m_data.ZoomCenter.xD.ToString(DEC_FORMAT);
					txtImaginary.Text = m_data.ZoomCenter.yD.ToString(DEC_FORMAT);

					//Save File
					txtFilePath.Text = m_data.SavePath;
					txtFilePrefix.Text = m_data.SavePrefix;
                    txtNextNumber.Text = m_data.NextSaveNum.ToString("0000");
                    if (m_data.Extension.ToLower() == ".png")
                        optPng.Checked = true;
                    else
                        optBmp.Checked = true;

					this.Show();
					this.TopMost = true;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			public void ReturnCenterPoint()
			{
				//frmMain modified it's copy of m_data
				try
				{
					//get CenterPoint
					txtReal.Text = m_data.ZoomCenter.xD.ToString(DEC_FORMAT);
					txtImaginary.Text = m_data.ZoomCenter.yD.ToString(DEC_FORMAT);

					//show form again
					this.Show();
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

		#endregion //Methods

 

	}
}
