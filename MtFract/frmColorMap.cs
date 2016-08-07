using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;

namespace MtFract
{
	public partial class frmColorMap : Form
	{
		#region Private

			Coloring _coloring;
			bool bLoading = false;

		#endregion //Private

		#region Constructors

			public frmColorMap()
			{
				InitializeComponent();
			}

			public frmColorMap(Coloring aColoring)
			{
				InitializeComponent();
				_coloring = aColoring;
				SetColoring();
			}

		#endregion //Constructors

		#region Controls

			private void grdColor_CellValueChanged(object sender, DataGridViewCellEventArgs e)
			{
				int Row, Col;
				int r, g, b;
	
				if (bLoading)
					return;

				try
				{
					Row = e.RowIndex;
					Col = e.ColumnIndex;

					//Row tests
					if (Row <= 0)
						return;
					if (Row >= _coloring.ColorMap.Length)
						return;

					//col test
					if ((Col > 0) && (Col < 4))
					{
						//get rgb values
						if (grdColor[1, Row].Value is int)
							r = (int)grdColor[1, Row].Value;
						else
							int.TryParse(grdColor[1, Row].Value.ToString(), out r);

						if (grdColor[2, Row].Value is int)
							g = (int)grdColor[1, Row].Value;
						else
							int.TryParse(grdColor[3, Row].Value.ToString(), out g);

						if (grdColor[3, Row].Value is int)
							b = (int)grdColor[1, Row].Value;
						else
							int.TryParse(grdColor[3, Row].Value.ToString(), out b);

						//set color
						_coloring.ColorMap[Row - 1] = 
							System.Drawing.Color.FromArgb(r, g, b);
						grdColor[4, Row].Style.BackColor = _coloring.ColorMap[Row - 1];
						grdColor[4, Row].Value = _coloring.ColorMap[Row - 1].Name;
					}

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void grdColor_DoubleClick(object sender, System.Windows.Forms.DataGridViewCellEventArgs e)
			{
				//Open ColorDialog to edit this row
				int row;
				System.Drawing.Color newColor;
				ColorDialog cDialog;
				

				try
				{
					row = e.RowIndex;
					cDialog = new ColorDialog();
					cDialog.Color = _coloring.ColorMap[row - 1];
					cDialog.FullOpen = true;
					cDialog.AnyColor = true;
					DialogResult result = cDialog.ShowDialog();

					if (result == System.Windows.Forms.DialogResult.OK)
					{
						//save new color
						newColor = cDialog.Color;
						_coloring.ColorMap[row] = newColor;

						//update grdColor
						grdColor[1, row].Value = newColor.R;
						grdColor[2, row].Value = newColor.G;
						grdColor[3, row].Value = newColor.B;
						grdColor[4, row].Value = newColor.Name;
						grdColor[4, row].Style.BackColor = newColor;
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void cmdSmooth_Click(object sender, EventArgs e)
			{	//smooth colors in selected rows between top and bottom color
				int rowStart, rowEnd, row, col;
				int cnt, numSteps;
				int rStart, gStart, bStart, rEnd, gEnd, bEnd;
				float rStep, gStep, bStep;

				try
				{
					//get start and end selected rows
					rowStart = -1;
					rowEnd = -1;
					for(row = 0; row < grdColor.Rows.Count; row++)
					{
						for (col = 0; col < grdColor.Columns.Count; col++)
						{
							if (grdColor[col, row].Selected)
							{	//this cell is selected
								if (rowStart < 0)
								{
									rowStart = row;
									rowEnd = row;
								}
								else if (row < rowStart)
									rowStart = row;
								if (row > rowEnd)
									rowEnd = row;
							}
						}
					}
					
					//exit if nothing selected
					if ((rowStart < 0) || (rowEnd < 0))
						return;

					numSteps = rowEnd - rowStart;
					if (numSteps <= 1)
						return;		//no smothing needed

					//Get Start color
					if (grdColor[1, rowStart].Value is int)
						rStart = (int)grdColor[1, rowStart].Value;
					else
						int.TryParse(grdColor[1, rowStart].Value.ToString(), out rStart);

					if (grdColor[2, rowStart].Value is int)
						gStart = (int)grdColor[2, rowStart].Value;
					else
						int.TryParse(grdColor[2, rowStart].Value.ToString(), out gStart);

					if (grdColor[1, rowStart].Value is int)
						bStart = (int)grdColor[3, rowStart].Value;
					else
						int.TryParse(grdColor[3, rowStart].Value.ToString(), out bStart);

					//Get end color
					if (grdColor[1, rowEnd].Value is int)
						rEnd = (int)grdColor[1, rowEnd].Value;
					else
						int.TryParse(grdColor[1, rowEnd].Value.ToString(), out rEnd);

					if (grdColor[2, rowEnd].Value is int)
						gEnd = (int)grdColor[2, rowEnd].Value;
					else
						int.TryParse(grdColor[2, rowEnd].Value.ToString(), out gEnd);

					if (grdColor[1, rowEnd].Value is int)
						bEnd = (int)grdColor[3, rowEnd].Value;
					else
						int.TryParse(grdColor[3, rowEnd].Value.ToString(), out bEnd);

					//get color iterations
					rStep = (float)(rEnd - rStart) / (float)numSteps;
					gStep = (float)(gEnd - gStart) / (float)numSteps;
					bStep = (float)(bEnd - bStart) / (float)numSteps;

					//write smoothed colors to grid and ColorMap
					for (cnt = 0; cnt < numSteps; cnt++)
					{
						int r,g,b;
						r = rStart + (int)(cnt * rStep);
						g = gStart + (int)(cnt * gStep);
						b = bStart + (int)(cnt * bStep);
						grdColor[1, rowStart + cnt].Value = r;
						grdColor[2, rowStart + cnt].Value = g;
						grdColor[3, rowStart + cnt].Value = b;
						_coloring.ColorMap[rowStart + cnt] = 
							System.Drawing.Color.FromArgb(r, g, b);
						grdColor[4, rowStart + cnt].Value = 
							_coloring.ColorMap[rowStart + cnt].Name;
						grdColor[4, rowStart + cnt].Style.BackColor = 
							_coloring.ColorMap[rowStart + cnt];
					}

					
				}
				catch (Exception ex) { cErrlog.Log(ex); }	
			}

			private void cmdAdd_Click(object sender, EventArgs e)
			{
				//Add a color
				try
				{
					int newrow = grdColor.Rows.Add(1);
					//int newrow = grdColor.Rows.Count;
					_coloring.ChangeNumColors(newrow);
					
					//set to black
					grdColor[0, newrow].Value = newrow;
					grdColor[1, newrow].Value = 0;
					grdColor[2, newrow].Value = 0;
					grdColor[3, newrow].Value = 0;
					grdColor[4, newrow].Value = System.Drawing.Color.Black.Name;
				}
				catch(Exception ex){cErrlog.Log(ex);}
			}

			private void cmdDel_Click(object sender, EventArgs e)
			{
				//Delete a color
				int curRow;
				try
				{
					curRow = grdColor.SelectedCells[0].RowIndex;

					//reorder other colors
					for (int i = curRow; i < _coloring.NumColors - 1; i++)
					{
						_coloring.ColorMap[i] = _coloring.ColorMap[i + 1];
					}

					_coloring.ChangeNumColors(_coloring.NumColors - 1);

					//remove from grid
					grdColor.Rows.RemoveAt(curRow);
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void cmdLoad_Click(object sender, EventArgs e)
			{
				//load current file as colormap
				if ((txtFile.Text != null) &&
					(txtFile.Text != "") &&
					System.IO.File.Exists(txtFile.Text))
				{
					_coloring.LoadColorMap(txtFile.Text);
				}
			}

			private void cmdBrowse_Click(object sender, EventArgs e)
			{
				OpenFileDialog openDialog = new OpenFileDialog();
				try
				{
					openDialog.FileName = System.IO.Path.GetDirectoryName( Assembly.GetExecutingAssembly().Location)
						+ 	"\\ColorMaps\\";
					openDialog.Title = "Color Map Location";
					openDialog.Filter = "Color maps(*.map|*.map;*.MAP|All Files (*.*)|*.*";
					openDialog.FilterIndex = 1;
					DialogResult result = openDialog.ShowDialog();
					if ((result == System.Windows.Forms.DialogResult.OK) &&
						(System.IO.File.Exists(openDialog.FileName)))
					{
						txtFile.Text = openDialog.FileName;
						_coloring.ColorFile = openDialog.FileName;
						_coloring.LoadColorMap(openDialog.FileName);

						//load colors to frmColorMap
						SetColoring();
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void optLinear_CheckedChanged(object sender, EventArgs e)
			{
				//select Linear color = n/factor
				if (optLinear.Checked)
					ChangeCycleType(Coloring.eCycleType.Linear);
			}

			private void optGeometric_CheckedChanged(object sender, EventArgs e)
			{
				//select Geometric color = n ^ (1/Fact)
				if (optGeometric.Checked)
					ChangeCycleType(Coloring.eCycleType.Geometric);
			}

			private void optExponential_CheckedChanged(object sender, EventArgs e)
			{
				//select exponential color = Log        n
				//											factor
				if (optExponential.Checked)
					ChangeCycleType(Coloring.eCycleType.Exponential);
			}

			private void txtCycleStart_Validating(object sender, CancelEventArgs e)
			{
				int CycleStart;
				try
				{
					if (int.TryParse(txtCycleStart.Text, out CycleStart))
					{
						_coloring.CycleStart = CycleStart;
					}
					else
						e.Cancel = true;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void txtFactor_Validating(object sender, CancelEventArgs e)
			{
				double factor;
				try
				{
					if (double.TryParse(txtFactor.Text, out factor))
					{
						switch (_coloring.CycleType)
						{
							case Coloring.eCycleType.Linear:
								if (factor > 0)
								{ _coloring.CycleFactor = factor; }
								else
								{ e.Cancel = true; }
								break;

							case Coloring.eCycleType.Exponential:
								if (factor > 1d)
									_coloring.CycleFactor = factor;
								else
									e.Cancel = true;
								break;

							default:
								if (factor >= 1d)
									_coloring.CycleFactor = factor;
								else
									e.Cancel = true;
								break;
						}
					}
					else
						e.Cancel = true;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void cmdHide_Click(object sender, EventArgs e)
			{
				this.Hide();
			}

			private void cmdSave_Click(object sender, EventArgs e)
			{	//Save Coloring to a file
				this.Hide();
				if ((txtFile.Text != null) && (txtFile.Text != ""))
					SaveColors(txtFile.Text);
			}

		#endregion //Controls

		#region Methods

			private void SetColoring()
			{
				//Set form Components based on Coloring
				try
				{
					bLoading = true;

					//Title
					this.Text = _coloring.Name;

					//grdColor
					grdColor.RowCount = _coloring.NumColors;
					for (int row = 0; row < _coloring.NumColors; row++)
					{
						//Fill Grid with RGB values
						grdColor[0, row].Value = row;
						grdColor[1, row].Value = _coloring.ColorMap[row].R;
						grdColor[2, row].Value = _coloring.ColorMap[row].G;
						grdColor[3, row].Value = _coloring.ColorMap[row].B;
						grdColor[4, row].Value = _coloring.ColorMap[row].Name;
						
						//Set color for this row
						grdColor[4, row].Style.BackColor = _coloring.ColorMap[row];
					}

					//txtFile
					txtFile.Text = _coloring.ColorFile;

					//fraColors
					if (_coloring.CycleStart < 1)
						_coloring.CycleStart = 1;
					txtCycleStart.Text = _coloring.CycleStart.ToString();
					if (_coloring.CycleFactor < 1d)
						_coloring.CycleFactor = 1d;
					txtFactor.Text = _coloring.CycleFactor.ToString();
					ChangeCycleType(_coloring.CycleType);
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				bLoading = false;
			}

			private void ChangeCycleType(Coloring.eCycleType newType)
			{
				//change Cycle type on interface and Coloring
				try
				{
					//turn on selected type
					switch (newType)
					{
						case Coloring.eCycleType.Linear:
							optLinear.Checked = true;
						break;

						case Coloring.eCycleType.Geometric:
							optGeometric.Checked = true;
							break;

						case Coloring.eCycleType.Exponential:
							optExponential.Checked = true;
							if (_coloring.CycleFactor <= 1.0d)
								_coloring.CycleFactor = 1.01d;
							txtFactor.Text = _coloring.CycleFactor.ToString();
							break;
					}

					//set Coloring
					_coloring.CycleType = newType;

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void SaveColors(string sFile)
			{	//save current colors to a file
				int cnt;
				StringBuilder sOut = new StringBuilder(1000);

				try
				{
					//save type
					sOut.AppendLine("[CycleType]");
					sOut.AppendLine(((int)_coloring.CycleType).ToString() + "," +
						_coloring.CycleStart.ToString() + "," +
						_coloring.CycleFactor.ToString("0.000"));

					//save Colors
					for (cnt = 0; cnt < _coloring.NumColors; cnt++)
					{
						sOut.AppendLine(_coloring.ColorMap[cnt].R.ToString("000") + " " +
							_coloring.ColorMap[cnt].G.ToString("000") + " " +
							_coloring.ColorMap[cnt].B.ToString("000"));
					}

					//write file
					System.IO.StreamWriter myWriter = new System.IO.StreamWriter(sFile);
					myWriter.Write(sOut.ToString());
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

		#endregion //Methods




	}
}
