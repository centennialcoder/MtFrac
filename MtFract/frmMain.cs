using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Resources;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace MtFract
{
	public partial class frmMain : Form
	{
		#region Public Variables

			const string GUI_THREAD = "GUI Thread";
			const string FLOAT_FORMAT = "0.000000000000000000000000000e000";

		#endregion //Public Variables

		#region Properties

			public SeriesData Series
			{
				get { return _series; }
			}

			public Params Params
			{
				get { return _params; }
			}

			public Plot Plot 
			{ 
				get {return _plot;}
			}

		#endregion

			#region Events

			#endregion //Events

			#region Private variables

			//Selection and Mouse
			private Point _mousePt;		//position of mouse
			private Point _down;		//location of Mouse Down
			private Point _move;		//distance mouse moved (pixels) since Mouse Down

			//forms
			private frmParameters _frmParams;
			private frmSeries _frmSeries;
			private frmColorMap _frmColorMap;

			//Data and objects
			private Coloring _colors;
			private Plot _plot;
			private SeriesData _series;
			private Params _params;
			private ToolTip _toolTip;

			//Screen Bitmap
			private Bitmap bmpScreen;		//what is displayed on screen
			private Bitmap bmpDouble;		//local copy of myPlot's bmpScreen

			//flags
			private bool _isAa;
			private bool _recalcAll;

		#endregion //Private Variables

		#region Controls

			#region frmMain

				public frmMain()
				{
					InitializeComponent();
				}

				private void frmMain_Load(object sender, EventArgs e)
				{
					try
					{
						//Threading
						Thread.CurrentThread.Name = GUI_THREAD;

						//Load Toolstrip images
						toolStrip1.ImageList = GetResourceImages();	
						btnAA.Image = toolStrip1.ImageList.Images[toolStrip1.ImageList.Images.IndexOfKey("notAA")];

						//Colors
						string sExeFile = Assembly.GetExecutingAssembly().Location;
						string sPath = Path.GetDirectoryName(sExeFile);
						string sColorFile = sPath + "\\ColorMaps\\MarkS.map";
						_colors = new Coloring(Coloring.eCycleType.Linear, 1, 1.0, sColorFile);

						//tooltip
						_toolTip = new ToolTip();
						//_toolTip.ShowAlways = true;
						_toolTip.AutomaticDelay = 1000;

						//initialize
						this.Show();
						_isAa = false;
						_recalcAll = true;
						_series = new SeriesData();
						_params = new Params();
						_params.PanRect = new MultiRect(0, 0, (double)picFractal.Width, (double)picFractal.Height);
						_params.ZoomVal = 1.0d;

						//forms
						_frmSeries = new frmSeries(_series);
						_frmSeries.Hide();
						_frmParams = new frmParameters(_params, this);
						_frmParams.Hide();
						_frmColorMap = new frmColorMap(_colors);
						_frmColorMap.Hide();
						this.Text = string.Format("MTFrac ({0},{1})", picFractal.Width, picFractal.Height);
						lblFileName.Text = _series.NextSaveFile;

						//create plot
						_plot = new Plot(picFractal.Width, picFractal.Height, 256, _colors);

						//Create Screen Bitmap
						bmpScreen = new Bitmap(picFractal.Width, picFractal.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
						
						//Connect to Plot Events
						_plot.RenderFinishedEvent += new EventHandler(plotFinished);

						//Rendering
						for (int exp = 4; exp <= 12; exp++)
						{
							int val = (int) Math.Pow(4d, (double)exp);
							cboDepth.Items.Add(val.ToString("#,0"));
						}
						cboDepth.SelectedIndex = 0;
						setDataType(eDataType.Double);

						//Plot initial Mandelbrot
						_plot.CalcPlot(_recalcAll);
						_recalcAll = false;

					}
					catch (Exception ex) { cErrlog.Log(ex); }
				}

				private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
				{
					//cleanup
					Log.IsShutdown = true;
					_frmParams.Close();
					_frmSeries.Close();
					_frmColorMap.Close();
				}

				private void frmMain_Resize(object sender, EventArgs e)
				{
					//resize picFractal
					int picWd = 0;
					int picHt = 0;

					try
					{
						if (_plot == null) return;

						//How much did picFractal increase in size
						picWd = picFractal.Width;
						picHt = picFractal.Height;
						string sSize = string.Format("({0},{1})", picWd, picHt);
						this.Text = "MTFrac " + sSize;
						Log.Info("picFractal size now " + sSize);
						if (picWd < 10)
							picWd = 10;
						if (picHt < 10)
							picHt = 10;

						resetPanRect();

						//change plot size
						_plot.ResetPlotSize(picWd, picHt);

						//change bmpScreen size
						bmpScreen = new Bitmap(picWd, picHt, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
						picFractal.Invalidate();
					}
					catch (Exception ex) 
					{
						string sParam = string.Format("Wd={0}, Ht={1}", picWd, picHt);
						cErrlog.Log(ex, sParam); 
					}
				}

				private void frmMain_MouseWheel(object sender, MouseEventArgs e)
				{
					//mouse wheel moved - only used for picFractal
					const double zoomTick = 1.1;
					double zoom;
					int downTicks;

					try
					{
						//only zoom if in manual zoom mode
						if ((_plot == null) || (_series.ZoomType != eZoomType.Manual))
						{ return; }

						//only zoom if not calculating
						if (_plot.RenderStatus != eRenderStatus.Render_Finished)
							return;

						//mouse position in picFractal
						Point zoomPt = new Point(e.X, e.Y);

						//Wheel up = 120, Wheel down = -120
						downTicks = e.Delta / 120;
						zoom = Math.Pow(zoomTick, downTicks);
												
						//maximum zoom out to 0.005
						if (_params.ZoomVal < 0.005)
						{
							_params.ZoomVal = 0.005;
						}
						if (zoom * _params.ZoomVal < 0.005)
						{
							zoom = 0.005 / _params.ZoomVal;
						}

						//maximum zoom in to 200
						if (_params.ZoomVal > 200)
							_params.ZoomVal = 200;
						if (zoom * _params.ZoomVal > 200)
						{
							zoom = 200 / _params.ZoomVal;
						}

						AdjustPanRectForZoom(zoom, zoomPt);

						//redraw bmp
						picFractal.Invalidate();
					}
					catch (Exception ex) { cErrlog.Log(ex); }

				}

				/// <summary>
				/// Modify PanRect using this ZoomVal
				/// and current zoom center point
				/// </summary>
				/// <param name="zoomVal"></param>
				public void AdjustPanRectForZoom(double zoomVal, Point center)
				{

					//multiply zoom by new value
					_params.ZoomVal = _params.ZoomVal * zoomVal;

					//adjust panRect
					_params.PanRect.x0 = _params.PanRect.x0 +
						(1 - zoomVal) * (center.X - _params.PanRect.x0);
					_params.PanRect.y0 = _params.PanRect.y0 +
						(1 - zoomVal) * (center.Y - _params.PanRect.y0);
					_params.PanRect.width = _params.PanRect.width * zoomVal;
					_params.PanRect.height = _params.PanRect.height * zoomVal;

				}

			#endregion //frmMain

			#region picFractal

				private void picFractal_MouseDown(object sender, MouseEventArgs e)
				{
					try
					{
						//Start of drag operation
						_down.X = e.X;
						_down.Y = e.Y;
						Console.WriteLine("Mouse Down({0}, {1})", e.X, e.Y);

						//tooltip
						//MultiPt IndexPt = plotPt(_mousePt);
						//_params.ComplexPt = _plot.ComplexPt(IndexPt);
						//_toolTip.SetToolTip(picFractal, string.Format("{0} \r\n {1}i", _params.ComplexPt.x, _params.ComplexPt.y));

						//reset PanZoom on right-click
						if (e.Button == System.Windows.Forms.MouseButtons.Right)
						{
							resetPanRect();
							//undo any moves
							_move = new Point(0, 0);
						}
					}
					catch (Exception ex) { cErrlog.Log(ex); }
				}

				private void picFractal_MouseUp(object sender, MouseEventArgs e)
				{
					MultiPt complexPt;

					try
					{
						if (_plot == null) return;

						//Get Point
						Console.WriteLine("Mouse Up({0}, {1})", e.X, e.Y);

						//Get CenterPoint if needed
						if (_series.bWaitingForCenter)
						{
							//calculate position of mouse location on Complex plane
							MultiPt PlotIdx = plotPt(e.Location);
							complexPt = _plot.ComplexPt(PlotIdx);

							//Set CenterPoint for mySeries
							_series.ZoomCenter = complexPt;
							_series.bWaitingForCenter = false;

							//redisplay frmSeries
							_frmSeries.ReturnCenterPoint();
						}

						//only mouse pan/zoom in set to Manual
						if (_series.ZoomType != eZoomType.Manual)
							return;

						//only change if not calculating
						if (_plot.RenderStatus != eRenderStatus.Render_Finished)
							return;

						//Calculate change to panRect
						_params.PanRect.x0 = _params.PanRect.x0 + e.X - _down.X;
						_params.PanRect.y0 = _params.PanRect.y0 + e.Y - _down.Y;

						//reset movement
						_move = new Point(0, 0);

						//invalidate picture
						picFractal.Invalidate();
					}
					catch (Exception ex) { cErrlog.Log(ex); }
				}

				private void picFractal_MouseMove(object sender, MouseEventArgs e)
				{	//refresh screen for drag operation
					try
					{
						if (_plot == null) return;
						if (e.Location.Equals(_mousePt)) { return; }

						//Get Point Mouse is over
						_mousePt = e.Location;
						MultiPt IndexPt = plotPt(_mousePt);
						_params.ComplexPt = _plot.ComplexPt(IndexPt);
						_params.MousePt = "(" + IndexPt.x.ToString("#") + "," +
							IndexPt.x.ToString("#") + ") = " +
							_plot.PlotValue((int)IndexPt.x, (int)IndexPt.y).ToString();
				
						//only mouse move if manually pan/zoom
						if (_series.ZoomType != eZoomType.Manual)
							return;

						//only move if not calculating
						if (_plot.RenderStatus != eRenderStatus.Render_Finished)
							return;

						//drag bitmap if left mouse button pressed
						if (e.Button == System.Windows.Forms.MouseButtons.Left)
						{
							_move.X = e.X - _down.X;
							_move.Y = e.Y - _down.Y;
							Console.WriteLine("Mouse Move({0},{1})" ,e.X , e.Y );
							this.picFractal.Invalidate(); 
						}
					}
					catch (Exception ex) { cErrlog.Log(ex); }
				}

				//private void picFractal_MouseHover(object sender, EventArgs e)
				//{
				//    //set tooltip
				//    MultiPt ComplexPt = _plot.ComplexPt(plotPt(_mousePt));
				//    _toolTip.SetToolTip(picFractal, string.Format("{0} \r\n {1}i", ComplexPt.x, ComplexPt.y));
				//}

				private void picFractal_Paint(object sender, PaintEventArgs e)
				{
					//repaint Mandelbrot
					updateBmpScreen();				//update bmpScreen (from pan or myPlot)
					e.Graphics.DrawImage(bmpScreen, 0, 0);	//drew bmpScreen
					Log.Info("picFractal painted");
				}


			#endregion //picFractal

			#region toolbar

				private void btnRender_Click(object sender, EventArgs e)
				{
					RenderNextPlot();
				}

				private void btnRestart_Click(object sender, EventArgs e)
				{
					if (_plot == null) return;

					resetPanRect();
					_plot.ResetHomeCoordinates();
				}

				private void btnRecolor_Click(object sender, EventArgs e)
				{
					_plot.Recolor();
				}

				private void btnAA_Click(object sender, EventArgs e)
				{
					_isAa = !_isAa;
					setAA(_isAa);
					_recalcAll = true;
				}

				private void cboDepth_TextChanged(object sender, EventArgs e)
				{	//set number of iteration to calculate Mandelbrot
					int maxIterations = 256;
					try
					{
						if (cboDepth.SelectedIndex >= 0)
							maxIterations = (int)Math.Pow(4.0, (double)(4 + cboDepth.SelectedIndex));
						//myPlot.numIterations = maxIterations;
						_params.NextMaxIter = maxIterations;
						_recalcAll = true;
						picFractal.Focus();
					}
					catch(Exception ex){cErrlog.Log(ex);}
				}

				private ImageList GetResourceImages()
				{
					//load images from resources into ImageList
					ImageList ilOut = new ImageList();
					Bitmap bm;

					try
					{
						//setup size
						ilOut.ImageSize = new Size(16, 16);

						//Load Images
						bm = Properties.Resources.AA;
						ilOut.Images.Add("AA", bm);

						bm = Properties.Resources.notAA;
						ilOut.Images.Add("notAA", bm);
					}
					catch (Exception ex) { cErrlog.Log(ex); }
					return ilOut;
				}

			#endregion

			#region Menu

				#region File Menu
				private void openToolStripMenuItem_Click(object sender, EventArgs e)
				{
					loadFractalDimensions();
				}

				private void saveFractalToolStripMenuItem_Click(object sender, EventArgs e)
				{
					saveFractalDimensions();
				}

				private void copyToClipboardToolStripMenuItem_Click(object sender, EventArgs e)
				{
					//Copy bmpScreen to clipboard
					try
					{
						if (bmpScreen != null)
							Clipboard.SetDataObject(bmpScreen, true);
					}
					catch (Exception ex) { cErrlog.Log(ex); }

				}

				private void saveNextPicToolStripMenuItem_Click(object sender, EventArgs e)
				{
					saveNextPic();
				}

				private void savePicAsToolStripMenuItem_Click(object sender, EventArgs e)
				{
					savePicAs();
				}

				private void exitToolStripMenuItem_Click(object sender, EventArgs e)
				{
					this.Close();
				}
				#endregion //File Menu

				#region CalculateMenu
				private void startToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_plot == null) return;
					if (_plot.RenderStatus != eRenderStatus.Render_InProgress)
					{
						_plot.CalcPlot(_recalcAll);
						_recalcAll = false;
					}
				}

				private void pauseToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_plot == null) return;
					if (_plot.RenderStatus == eRenderStatus.Render_InProgress)
						_plot.RenderStatus = eRenderStatus.Render_Paused;
				}

				private void stopToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_plot == null) return;
					_plot.RenderStatus = eRenderStatus.Render_Finished;
				}
				#endregion //Calculate Menu

				#region Zoom Menu
				private void resetToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_plot == null) return;

					resetPanRect();
					_plot.ResetHomeCoordinates();
				}

				private void manualToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_series == null) return;
					_series.ZoomType = eZoomType.Manual;
				}

				private void seriesInToolStripMenuItem_Click(object sender, EventArgs e)
				{
					if (_series == null) _series = new SeriesData();
					if ((_frmSeries == null)||(_frmSeries.IsDisposed))
					{
						_frmSeries = new frmSeries();
					}
					
					_frmSeries.RefreshData(_series);
				}
				#endregion //Zoom Menu

				#region Options Menu
				private void parametersToolStripMenuItem_Click(object sender, EventArgs e)
				{
					try
					{
						//show parameters form
						if ((_frmParams == null)||(_frmParams.IsDisposed))
							_frmParams = new frmParameters(_params, this);

						_frmParams.ShowParams();
					}
					catch (Exception ex) { cErrlog.Log(ex); }
				}

				private void colorsToolStripMenuItem_Click(object sender, EventArgs e)
				{
					//show colors form
					if ((_frmColorMap == null) || (_frmColorMap.IsDisposed))
					{
						_frmColorMap = new frmColorMap(_colors);
					}
					_frmColorMap.Show();
				}

				private void doubleToolStripMenuItem_Click(object sender, EventArgs e)
				{
					setDataType(eDataType.Double);
				}

				private void decimalToolStripMenuItem_Click(object sender, EventArgs e)
				{
					setDataType(eDataType.Decimal);
				}

				#endregion //Options Menu

			#endregion

			private void tmrSeries_Tick(object sender, EventArgs e)
			{
				try
				{
					if (_plot == null) return;

					//repaint if rendering
					if (_plot.RenderStatus == eRenderStatus.Render_InProgress)
					{
						picFractal.Invalidate();
					}

					//start next member of series if in series
					if ((_plot.RenderStatus == eRenderStatus.Render_Finished) &&
						(_series.ZoomType != eZoomType.Manual))
					{
						//Log.LogWrite("tmrSeries Starting Next Series");
						RenderNextInSeries();
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

		#endregion //Controls

		#region Methods
			/// <summary>
			/// reset Params.PanRect and ZoomVal, Invalidate picFractal
			/// </summary>
			private void resetPanRect()
			{
				//resets panRect
				_params.PanRect.x0 = 0d;
				_params.PanRect.y0 = 0d;
				_params.PanRect.width = picFractal.Width;
				_params.PanRect.height = picFractal.Height;
				_params.ZoomVal = 1d;
				picFractal.Invalidate();
			}

			private MultiPt plotPt(System.Drawing.Point mousePt)
			{
				//converts mouse location to index of current Plot/Bitmap
				//Adjust for pan and zoom of last rendered bitmap

				MultiPt ptOut = new MultiPt();
				int aaFact = 1;
				if (_isAa) { aaFact = 2; }	//2 indexes for each pixel in AA
				try
				{
					ptOut.x = (aaFact * mousePt.X - _params.PanRect.x0) / _params.ZoomVal;
					ptOut.y = (aaFact * mousePt.Y - _params.PanRect.y0) / _params.ZoomVal;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return ptOut;
			}

			private void RenderNextInSeries()
			{   //calculate and render next plot in series
				try
				{
					//exit if we are not set for a series
					if (_series.ZoomType == eZoomType.Manual)
						return;

					//Calculate next plot in series
					resetPanRect();

					switch (_series.ZoomType)
					{
						case eZoomType.SeriesIn:
							_params.ZoomVal = _series.ZoomVal;						
							break;

						case eZoomType.SeriesOut:
							_params.ZoomVal = 1 / _series.ZoomVal;
						   break;

						case eZoomType.SeriesPan:
							_params.PanRect = GetSeriesPanRect();
							break;
					}

					//Adjust for current zoom
					AdjustPanRectForZoom(_params.ZoomVal, ZoomCenterScreenPt());

					//start rendering next plot in series
					RenderNextPlot();
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private Point ZoomCenterScreenPt()
			{
				Point screenPt = new Point();
				MultiPt IndexPt = _plot.IndexPt(_series.ZoomCenter);
				if (_isAa)
				{
					IndexPt.x = IndexPt.x / 2.0;
					IndexPt.y = IndexPt.y / 2.0;
				}
				screenPt.X = Math.Max(0, (int)IndexPt.x);
				screenPt.Y = Math.Max(0, (int)IndexPt.y);
				return screenPt;
			}

			private MultiPt ZoomCenterIndexPt()
			{
				MultiPt IndexPt = _plot.IndexPt(_series.ZoomCenter);
				if (_isAa)
				{
					IndexPt.x = IndexPt.x / 2.0;
					IndexPt.y = IndexPt.y / 2.0;
				}
				return IndexPt;
			}

			private MultiRect GetSeriesPanRect()
			{
				//for a Series.Pan - set panRect for next plot
				resetPanRect();
				MultiRect panOut = _params.PanRect;
				double deltaX = 0d;
				double deltaY = 0d;
				double absDx, absDy;

				try
				{
					//get Complex CenterPt - convert to picFract index
					MultiPt indexPt = ZoomCenterIndexPt();

					//get center of picFrac
					MultiPt CenterPt = new MultiPt(picFractal.Width / 2.0, picFractal.Height / 2.0);

					//how far is ZoomCenter from center of picFractal
					deltaX = CenterPt.x - indexPt.x;
					deltaY = CenterPt.y - indexPt.y;
					absDx = Math.Abs(deltaX);
					absDy = Math.Abs(deltaY);

					//adjust panRect if we move > 0.5 pixels
					if (absDx < 0.5)
					{
						deltaX = 0d;
						absDx = 0d;
					}
					if (Math.Abs(deltaY) < 0.5)
					{
						deltaY = 0d;
						absDy = 0d;
					}
					//pan Maximum of (delta, mySeries.pan)
					panOut.x0 = Math.Sign(deltaX) * Math.Min(absDx, (double)_series.PanPix);
					panOut.y0 = Math.Sign(deltaY) * Math.Min(absDy, (double)_series.PanPix);
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return panOut;
			}

			private void RenderNextPlot()
			{
				//Calculates next plot using panZoom, and tells Plot to render it
				try
				{
					//create Plot if necessary
					if (_plot == null)
					{
						_plot = new Plot(picFractal.Width, picFractal.Height, 256, _colors);
					}

					//check if anything changed
					if ((_params.ZoomVal == 1.0) &&
						(_params.PanRect.topLeft.x == 0d) &&
						(_params.PanRect.topLeft.y == 0d) &&
						(_params.PanRect.width == picFractal.Width) &&
						(_params.PanRect.height == picFractal.Height) &&
						(_params.NextMaxIter == _params.NumIter) &&
						_move.X == 0d && _move.Y == 0d && 
						!_recalcAll)
					{   //no change
						return;
					}

					//copy then reset PanRect 
					_plot.numIterations = _params.NextMaxIter;
					_params.NumIter = _params.NextMaxIter;
					//add any movement to panRect (to allow non-zoom panning)
					_params.PanRect.AddPoint(_move);
					_move = new Point(0, 0);
					//copy PanRect to plot and reset it
					_plot.panRect = _params.PanRect;
					resetPanRect();

					//Calculate
					_plot.CalcPlot(_recalcAll);
					_recalcAll = false;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			//Misc
			private string renderTimeString()
			{
				//formatted string with last time to render Mandelbrot
				StringBuilder sOut = new StringBuilder(80);
				try
				{
					//Time to render
					sOut.Append(_plot.RenderTimeString);
					sOut.Append(" ");
					//Number of Pixels
					sOut.Append(_plot.NumPixelString);
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return sOut.ToString();
			}

			

			//GUI Rendering
			private void updateBmpScreen()
			{
				//Copies myPlot.bmpPlot to bmpScreen if necesary
				//draws bmpScreen	adjusting for current pan-zoom
				Graphics scrGraphics;

				try
				{
					if (_plot == null) return;

					if (_plot.bmpChanged)
					{
						//copy bmpPlot to bmpDouble
						//			if AA, then when we mouse zoom - we see extra detail up to 2x
						//Get bmpPlot
						if (_isAa)
						{
							//already oversampled - just use plot bmp
							bmpDouble = _plot.BmpPlot;
						}
						else
						{//1:1 fast anti-aliasing by doubling bitmap
							bmpDouble = new Bitmap(_plot.BmpPlot, 2 * picFractal.Width, 2 * picFractal.Height);
						}
					}


					//Get graphics object to draw on bmpScreen
					scrGraphics = Graphics.FromImage(bmpScreen);
					scrGraphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;

					//modify by mouse selected panRect if we are not rendering
					if (_plot.RenderStatus == eRenderStatus.Render_Finished)
					{
						//all black
						scrGraphics.FillRectangle(Brushes.Black, 0, 0, picFractal.Width, picFractal.Height);

						//Get int rectangle from panRect
						System.Drawing.Rectangle intRect = new Rectangle(
							(int)(_params.PanRect.x0 + _move.X), (int)(_params.PanRect.y0 + _move.Y),
							(int)_params.PanRect.width, (int)_params.PanRect.height);

						//do we need to test panRect boundaries to be within plotRec?
						//((myParams.panRect.x0 + m_Move.X + myParams.panRect.width < 0) ||		//left of old plot
						//   (myParams.panRect.x0 + m_Move.X > myPlot.PlotRect.Width) ||	//right of old plot
						//   (myParams.panRect.y0 + m_Move.Y + myParams.panRect.height < 0) ||		//above old plot
						//   (myParams.panRect.y0 + m_Move.Y > myPlot.PlotRect.Height))		//below old plot

						//Draw oversampled plot bmp into our rectangle
						scrGraphics.DrawImage(bmpDouble, intRect);
					}
					else
					{	//draw from bmpDouble without pan/zoom
						scrGraphics.DrawImage(bmpDouble, 0, 0, picFractal.Width, picFractal.Height);
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

		  //GUI ViewModel
			private void setAA(bool aaOn)
			{
				//Set AA for myPlot, and Screen
				if (aaOn)
				{
					//Anti-aliasing
					btnAA.Image = toolStrip1.ImageList.Images[toolStrip1.ImageList.Images.IndexOfKey("AA")];
				}
				else
				{
					//no Anti-aliasing
					btnAA.Image = toolStrip1.ImageList.Images[toolStrip1.ImageList.Images.IndexOfKey("notAA")];
				}
				if (_plot != null)
					_plot.IsAa = aaOn;
			}

			private void setDataType(eDataType newType)
			{
				//sets data type used in Mandelbrot calculation
				//and configures GUI to match

				//set Menu
				doubleToolStripMenuItem.Checked = (newType == eDataType.Double);
				decimalToolStripMenuItem.Checked = (newType == eDataType.Decimal);

				//set Plot DataType
				if (_plot != null)
				{
					_plot.DataType = newType;
				}
			}


			//File Operations
			private void savePicAs()
			{
				SaveFileDialog SaveDialog1 = new SaveFileDialog();
				string SaveName;
				int Idx = 0;
				string sPrefix = "";

				try
				{
					if (_series == null)
						_series = new SeriesData();

					//setup Save Dialog
					SaveDialog1.Title = "Save Current Image as";
					SaveDialog1.Filter = "Bitmap (*.bmp)|*.bmp|PNG (*.png)|*.png";
					SaveDialog1.InitialDirectory = _series.SavePath;
					SaveDialog1.FileName = _series.NextSaveFile;
					if (_series.Extension == "png")
					{
						SaveDialog1.DefaultExt = "png";
					}
					else
					{
						SaveDialog1.DefaultExt = "bmp";
					}
					SaveDialog1.CheckFileExists = false;
					SaveDialog1.AddExtension = true;

					//get new filename
					DialogResult result= SaveDialog1.ShowDialog();
					if (result != System.Windows.Forms.DialogResult.OK)
						return; // don't save

					//Save new Filename in Series object
					SaveName = SaveDialog1.FileName;
					string sExt, sFile, sNumber, sPath;
					sPath = Path.GetDirectoryName(SaveName);
					sExt = Path.GetExtension(SaveName).ToLower();
					sFile = Path.GetFileNameWithoutExtension(SaveName);

					//Get Prefix and number
					if (sFile.Length >= 4)
					{
						Idx = sFile.Length - 1;
						sNumber = "";
						while (sFile[Idx] >= '0' && sFile[Idx] <= '9')
						{   //This char is a number
							sNumber = sFile[Idx] + sNumber;
							sPrefix = sFile.Substring(0, Idx);
							Idx -= 1;
						}
						int.TryParse(sNumber, out Idx);

					}

					//update Series object
					_series.SavePath = sPath;
					_series.SavePrefix = sPrefix;
					_series.NextSaveNum = Idx + 1;
					_series.Extension = sExt;

					//Save picture
					System.Drawing.Imaging.ImageFormat format = _series.Format;
					bmpScreen.Save(SaveName, format);

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void saveNextPic()
			{
				try
				{
					Log.Info("Saving " + _series.NextSaveFile);
					bmpScreen.Save(_series.NextSaveFile, _series.Format);
					Log.Info("{0} saved.", _series.NextSaveFile);
					_series.IncrementSaveNum();
				}
				catch (Exception ex) 
				{ 
					cErrlog.Log(ex); 
				}
			}

			private void saveFractalDimensions()
			{
				SaveFileDialog saveDialog1 = new SaveFileDialog();
				string sFilename;
				StringBuilder sbOutput = new StringBuilder(80);
				try
				{

					saveDialog1.Title = "Save Fractal Dimensions";
					saveDialog1.Filter = "Text Files (*.txt)|*.txt";
					DialogResult result = saveDialog1.ShowDialog();
					if (result == System.Windows.Forms.DialogResult.OK)
					{
						//create output string
						sbOutput.AppendLine(this.Width.ToString());
						sbOutput.AppendLine(this.Height.ToString());
						MultiPt topLeft = new MultiPt(0.0, 0.0);
						MultiPt bottomRight = new MultiPt((double)_plot.PlotRect.Width, (double)_plot.PlotRect.Height);
						//left = x
						sbOutput.AppendLine(_plot.ComplexPt(topLeft).xD.ToString(FLOAT_FORMAT));
						//top = y
						sbOutput.AppendLine(_plot.ComplexPt(topLeft).yD.ToString(FLOAT_FORMAT));
						//right
						sbOutput.AppendLine(_plot.ComplexPt(bottomRight).xD.ToString(FLOAT_FORMAT));
						//bottom
						sbOutput.AppendLine(_plot.ComplexPt(bottomRight).yD.ToString(FLOAT_FORMAT));
						
						//Series Data
						sbOutput.AppendLine(_series.SavePath);
						sbOutput.AppendLine(_series.SavePrefix);
						sbOutput.AppendLine(_series.Extension);
						sbOutput.AppendLine(_series.NextSaveNum.ToString());
						sbOutput.AppendLine(_series.ZoomVal.ToString());
						sbOutput.AppendLine(_series.ZoomCenter.xD.ToString());
						sbOutput.AppendLine(_series.ZoomCenter.yD.ToString());

						//write data 
						sFilename = saveDialog1.FileName;
						using (StreamWriter outfile = new StreamWriter(sFilename))
						{
							outfile.Write(sbOutput.ToString());
						}

					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void loadFractalDimensions()
			{
				//loads size and fractal dimensions from previous save file
				OpenFileDialog openDialog;
				int wd, ht;
				Decimal top, left, bottom, right;
				string sLine;

				try
				{
					if (_plot.RenderStatus == eRenderStatus.Render_InProgress)
						return; //don't interrupt current render

					openDialog = new OpenFileDialog();
					openDialog.Title = "Open Fractal Data";
					openDialog.Filter = "Text Files (*.txt)|*.txt";
					if (openDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
					{   //user selected file to open
						using (StreamReader sr = new StreamReader(openDialog.FileName))
						{
							//width
							sLine = sr.ReadLine();
							int.TryParse(sLine, out wd);
							if (wd <= 0) return;

							//height
							sLine = sr.ReadLine();
							int.TryParse(sLine, out ht);
							if (ht <= 0) return;

							//get style to read -0.000000000000000000000000000e000
							System.Globalization.NumberStyles style = System.Globalization.NumberStyles.Number |
								System.Globalization.NumberStyles.AllowExponent;
							System.Globalization.CultureInfo culture = System.Globalization.CultureInfo.CurrentCulture;

							//left
							sLine = sr.ReadLine();
							if (sLine == null) return;
							Decimal.TryParse(sLine, style, culture, out left);

							//top
							sLine = sr.ReadLine();
							if (sLine == null) return;
							Decimal.TryParse(sLine, style, culture, out top);

							//right
							sLine = sr.ReadLine();
							if (sLine == null) return;
							Decimal.TryParse(sLine, style, culture, out right);

							//bottom
							sLine = sr.ReadLine();
							if (sLine == null) return;
							Decimal.TryParse(sLine, style, culture, out bottom);

							//Set frmMain dimensions
							Log.Info(string.Format("Setting frmMain to ({0},{1})", wd, ht));
							this.Size = new Size(wd, ht);
							_plot.ResetPlotSize(picFractal.Width, picFractal.Height);

							//Set bounds of plot
							Decimal width = right - left;
							Decimal height = top - bottom;
							Log.Info(string.Format("Setting Complex Boundary to TopLeft = ({0},{1}), Size({2},{3})",
								top, left, width, height));
							MultiRect newBoundary = new MultiRect(left, top, width, height);
							_plot.ResetPlotBoundary(newBoundary);

							//Series Data
							//SavePath
							sLine = sr.ReadLine();
							if (sLine == null) return;
							_series.SavePath = sLine;

							//SavePrefix
							sLine = sr.ReadLine();
							if (sLine == null) return;
							_series.SavePrefix = sLine;

							//Extension
							sLine = sr.ReadLine();
							if (sLine == null) return;
							_series.Extension = sLine;

							//NextSaveNum
							sLine = sr.ReadLine();
							if (sLine == null) return;
							int nextNum;
							int.TryParse(sLine, out nextNum);
							_series.NextSaveNum = nextNum;

							//ZoomVal
							sLine = sr.ReadLine();
							if (sLine == null) return;
							double zoom;
							double.TryParse(sLine, out zoom);
							_series.ZoomVal = zoom;

							//ZoomCenter.xD
							sLine = sr.ReadLine();
							if (sLine == null) return;
							decimal x;
							Decimal.TryParse(sLine, style, culture, out x);
							_series.ZoomCenter.xD = x;

							//ZoomCenter.yD
							sLine = sr.ReadLine();
							if (sLine == null) return;
							decimal y;
							Decimal.TryParse(sLine, style, culture, out y);
							_series.ZoomCenter.yD = y;

						}
					}

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			//Event Handlers
			private void plotFinished(object sender, EventArgs e)
			{
				//Plot has finished rendering Mandelbrot set
				//invalidate picture to draw it
				resetPanRect();

				//wait for display update before saving image
				Application.DoEvents();
				System.Threading.Thread.Sleep(250);

				updatePlotInfoInUiThread();

				//change status
				_plot.RenderStatus = eRenderStatus.Render_Finished;
			}

			/// <summary>
			/// Plotting has finished: update time, set title, save file
			/// </summary>
			private void updatePlotInfoInUiThread()
			{
				if (this.InvokeRequired)
				{
					this.BeginInvoke(new MethodInvoker(() => updatePlotInfoInUiThread()));
				}
				else
				{
					lblRenderTime.Text = renderTimeString();
					Log.Info("Render took {0}", lblRenderTime.Text);
					lblFileName.Text = _series.NextSaveFileShort;

					//save file if we are rendering Series
					if (_series.ZoomType != eZoomType.Manual)
					{
						saveNextPic();
					}
				}
			}

		#endregion //Methods


	}
}
