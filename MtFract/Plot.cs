//The MIT License(MIT)
//Copyright(c) 2016 Mark Scherer

//Permission is hereby granted, free of charge, to any person obtaining a copy of this
//software and associated documentation files (the "Software"), to deal in the Software
//without restriction, including without limitation the rights to use, copy, modify, merge, 
//publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons
//to whom the Software is furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in all copies or
//substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, 
//INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR
//PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE
//FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR
//OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR
//OTHER DEALINGS IN THE SOFTWARE. 


using System;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Remoting.Messaging;


namespace MtFract
{

	#region enums


		public enum eRenderStatus
		{
			Render_Finished = 0,
			Render_InProgress = 1,
			Render_Paused = 2,
		}

		public enum eShortCut
		{
			RecalcAll = 0,
			Pan = 1,
			ZoomOut = 2,
		}

		public enum eDataType
		{
			Double = 1,
			Decimal = 2,
			BigInteger = 3,
		}


	#endregion //enums

	#region delegates

		public delegate void boolDelegate(bool bParam);

	#endregion //delegates


	public class Plot
	{
		//All the data of a single Mandlebrot image 
		//		Rectangular array of the complex plane
		//		that has number of iterations fnMandelbrot while |fnMandelbrot| < 2

		#region Private Data

			//iteration array
			private int _numIterations;				//Max number iterations to try
			private Rectangle _plotRect;				//dimensions of plot = screen window dimension unless AA
			private Rectangle _newPlotRect;			//resize size - delayed resize
			private int[][] plot;						//array of measured iterations
			private int[][] nextPlot;					//values from next plot
			private bool _isAa;							//2x anti-aliasing

			//Wait for next recalc flags
			private bool _mustResize;					//resize m_plot after completing next calculation
			private bool _mustRecolor;				    //recolor plot after completing next calculation
			private bool _mustReset;					//reset coordinates next recalc

			//locks
			private object plotLock = new object();
			private object bitmapLock = new object();

			//Complex representation
			private MultiRect _complexRect;			    //complex boundaries
			private MultiPt iterStep;					//iteration distance for plot

			//Next Plot
			private MultiRect m_panRect;				//previous plot location in screen
			private double zoom;						//zoom from last plot = old width / new width

			//bitmaps
			private Bitmap _bmpPlot;					//calculated bitmap from m_plot
			private bool _bmpChanged;					//bmpPlot has changed
			private Coloring _colorLookup;				//coloring scheme

			//flags and status
			private eRenderStatus _renderStatus;		//status of current calculation
			private eDataType _dataType;				//the current data type
			private eShortCut shortcut;					//possible shortcut method to reuse previous values
			private bool forceTotalRecalc;				//skip rendering shortcuts
			private TimeSpan _lastRenderTime;			//time to render last plot
			private DateTime _renderTimeStart;			//time started rending next plot

			//restart rendering flags
			private int continueCol;					//last col calculated
			private TimeSpan RenderTimePartial;			//ongoing time to render next plot (if paused)
			

		#endregion //Private Data

		#region Properties

			//Fractal Data
			public int numIterations
			{
				get { return _numIterations; }
				set { 
					_numIterations = value;
					_bmpChanged = true;
				}
			}

			public Rectangle PlotRect
			{
				get { return _plotRect; }
			}

			public MultiRect ComplexRect
			{
				get { return _complexRect; }
			}

			public bool IsAa
			{
				get { return _isAa; }
				set
				{
					//must be stopped to change this
					if (_renderStatus == eRenderStatus.Render_Finished)
					{
						bool origAA = _isAa;
						if (value != _isAa)
						{
							_isAa = value;
							if (_isAa)
							{	//switch to AA
								ResizePlotNow(PlotRect.Width, PlotRect.Height, false);
							}
							else
							{	//switch to non-AA
								ResizePlotNow(PlotRect.Width / 2, PlotRect.Height / 2, false);
							}
						}
					}
				}
			}

			//Next Plot 
			public MultiRect panRect
			{
				get {return m_panRect;}
				set { 
					//must be stopped to change this
					if (_renderStatus == eRenderStatus.Render_Finished)
					{	//deep copy of panRect - do not use original object
						m_panRect.Copy(value);	//copy so we don't depend upon value not changing in other classes

						//get zoom = panRect.width / plot width
						if (_isAa)
						{
							zoom = m_panRect.width * 2.0 / _plotRect.Width;
						}
						else
						{
							zoom = m_panRect.width / _plotRect.Width;
						}				
					}
				}
			}

			//Bitmap
			public Bitmap BmpPlot
			{	//readonly  returns copy
				get {
					lock (bitmapLock)
					{
						_bmpChanged = false;		//we got it, now it hasn't changed
						//this assumes only one subscriber to m_bmpPlot
						return new Bitmap(_bmpPlot);
					}
				}
			}

			public bool bmpChanged
			{	//readonly
				get { return _bmpChanged; }
			}

			public Coloring ColorLookup
			{
				get { return _colorLookup; }
				set { _colorLookup = value; }
			}

			public int NumPixels
			{
				get { return _plotRect.Width * _plotRect.Height; }
			}

			public string NumPixelString
			{
				get
				{
					string sPix;
					//Number of Pixels
					double NumPix = (double)(NumPixels) / 1024d;
					if (NumPix > 1024d)
					{
						NumPix = NumPix / 1024d;
						sPix = (NumPix.ToString("#,0.0") + " MPix");
					}
					else
					{
						sPix = (NumPix.ToString("#,0") + " KPix");
					}
					return sPix;
				}
			}

			//flags and status
			public eRenderStatus RenderStatus
			{
				get { return _renderStatus; }
				//use CalcNextPlot to (re)start rendering
				set {
					if (value != eRenderStatus.Render_InProgress)
						SetRenderStatus(value);
					//only CalcNextPlot can start rendering
					//user can stop or pause
				}
			}

			public eDataType DataType
			{
				get { return _dataType; }
				set { _dataType = value; }
			}

			public TimeSpan LastRenderTime
			{	//readonly
				get { return _lastRenderTime; }
			}

			public string RenderTimeString
			{
				get
				{
					StringBuilder sOut = new StringBuilder(80);
					if (LastRenderTime.Days > 0)
						sOut.Append(LastRenderTime.Days.ToString("#,0") + "d ");

					if (LastRenderTime.Hours > 0)
						sOut.Append(LastRenderTime.Hours.ToString("#,0") + "h ");

					if (LastRenderTime.Minutes > 0)
						sOut.Append(LastRenderTime.Minutes.ToString("#,0") + "m ");

					sOut.Append(LastRenderTime.Seconds.ToString("#,0"));
					if (LastRenderTime.Minutes == 0)
					{
						sOut.AppendFormat(".{0:0s}", LastRenderTime.Milliseconds / 100);
					}
					else { sOut.Append("s"); }

					return sOut.ToString();
				}
			}

			public DateTime renderTimeStart
			{	//readonly
				get { return _renderTimeStart; }
			}

		#endregion //Properties

		#region Events

			//Rendering current plot is finished
			public event EventHandler RenderFinishedEvent;

			protected virtual void OnRenderFinishedEvent(EventArgs e)
			{
				//check if anyone is handling the event
				if (RenderFinishedEvent != null)
				{
					RenderFinishedEvent(this, e);
				}
			}

		#endregion //Events

		#region Public Methods

			public void ResetHomeCoordinates()
			{
				try
				{
					//resets mandelbrot to home position
					if (_renderStatus == eRenderStatus.Render_Finished)
					{
						ResetCoordinatesNow();
						//calculate
						CalcNextPlot(false);
					}
					else
					{
						_mustReset = true;
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			public int PlotValue(int xIdx, int yIdx)
			{   //returns number of iterations for this index Pt in Plot
				
				int iOut = -1;
				try
				{
					if (	(xIdx >= 0) && (xIdx < _plotRect.Width) &&
							(yIdx >= 0) && (yIdx < _plotRect.Height) )
					{
						iOut = plot[xIdx][yIdx];
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return iOut;
			}

			public MultiPt ComplexPt(MultiPt indexPt)
			{
				//converts Plot Index Pt to point on complex plane
				decimal rStep, iStep;
				MultiPt ptOut = new MultiPt();

				try
				{
					rStep = _complexRect.widthD / _plotRect.Width;
					iStep = _complexRect.heightD/ _plotRect.Height;
					ptOut.xD = _complexRect.x0D + rStep * indexPt.xD;
					ptOut.yD = _complexRect.y0D - iStep * indexPt.yD;
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return ptOut;
			}

			public MultiPt IndexPt(MultiPt complexPt)
			{
				//converts Complex Plane point to (x,y) index point of plot
				decimal rStep, iStep;
				MultiPt ptOut = new MultiPt();

				try
				{
					rStep = _complexRect.widthD / _plotRect.Width;
					iStep = _complexRect.heightD / _plotRect.Height;

					ptOut.xD = (complexPt.xD - _complexRect.x0D) / rStep;
					ptOut.yD = (_complexRect.y0D - complexPt.yD) / iStep; //flip this because y0 is high value
				 }
				catch (Exception ex) { cErrlog.Log(ex); }
				return ptOut;
			}

			public void ResetPlotSize(int width, int height)
			{
				//resets plot unless we are currently calculating, then do it later
				if (_renderStatus == eRenderStatus.Render_Finished)
				{
					ResizePlotNow(width, height, true);
				}
				else
				{	//wait until we are finished
					_newPlotRect = new Rectangle(0, 0, width, height);
					_mustResize = true;
					Log.Info(string.Format("Waiting to set Plot to ({0}, {1})", width, height));
				}
			}

			public void ResetPlotBoundary(MultiRect newComplexRect)
			{
				//forces reset of plot boundary
				//also resets nextPlotTrans
				try
				{
					if (_renderStatus == eRenderStatus.Render_InProgress)
						return;

					//reset complex rectangle
					_complexRect = newComplexRect;

					//reset Pan/zoom
					ResetPanZoom();

					//Recalculate
					CalcNextPlot(false);
					
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			public void Recolor()
			{
				//go through current plot, recoloring with current Coloring
				if ((_renderStatus == eRenderStatus.Render_Finished) ||
					(_renderStatus == eRenderStatus.Render_Paused))
				{
					RecolorNow();
				}
				else
				{
					_mustRecolor = true;
				}
			}

			public void CalcPlot(bool bRecalcAll)
			{	//Calculate next Mandelbrot based on current plot and nextPlotTrans
				try
				{
					if (bRecalcAll)
						forceTotalRecalc = true;

					switch (_renderStatus)
					{
						case eRenderStatus.Render_Finished:
							CalcNextPlot(false);	//start a new calculation
							break;

						case eRenderStatus.Render_InProgress:
							//cannot start - we are already calculating
							break;

						case eRenderStatus.Render_Paused:
							CalcNextPlot(true);	//continue
							break;
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}
	
		#endregion //Publc Methods

		#region constructors

			public Plot() :this(640,480, 256, new Coloring())
			{
				//default constructor
			}

			public Plot(int width, int height, int depth, Coloring colorMap)
			{
				//setup plot
				_plotRect = new Rectangle(0, 0, width, height);
				plot = new int[width][];
				_numIterations = depth;
				_complexRect = new MultiRect();
				_isAa = false;

				ResetCoordinatesNow();
				ResizePlotNow(width, height, false);
				ResetPanZoom();

				_bmpPlot = new Bitmap(PlotRect.Width, PlotRect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
				_bmpChanged = true;
				_colorLookup = colorMap;
				_dataType = eDataType.Double;
				_renderStatus = eRenderStatus.Render_Finished;
				_lastRenderTime = System.TimeSpan.FromSeconds(0d);
				forceTotalRecalc = true;
				_mustReset = false;
				_mustRecolor = false;
				_mustResize = false;

				//set default Complex coordinates and plot them

			}

		#endregion

		#region Private Methods

			private void SetRenderStatus(eRenderStatus newStatus)
			{
				//Sets renderStatus and sends an event
				bool bChanged;
				try
				{
					bChanged = false;

					switch (newStatus)
					{
						case eRenderStatus.Render_Finished:
							//cancel any render in progress
							if (_renderStatus != eRenderStatus.Render_Finished)
							{
								_renderStatus = newStatus;
								bChanged = true;
							}
							break;

						case eRenderStatus.Render_Paused:
							//must be calculating/paused to pause
							if (_renderStatus == eRenderStatus.Render_InProgress)
							{
								_renderStatus = eRenderStatus.Render_Paused;
								bChanged = true;
							}
							//pause - stay paused
							//finished - stay finished
							break;
						case eRenderStatus.Render_InProgress:
							//should only be called by CalcNextPlot
							_renderStatus = eRenderStatus.Render_InProgress;
							bChanged = true;
							break;
					}

					if (bChanged)
					{
						Log.Info("Render status now {0}", _renderStatus);
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void ResetCoordinatesNow()
			{
				try
				{
					//resets mandelbrot to home position
					Log.Info("Entering ResetCoordinatesNow");

					_mustReset = false;
					_complexRect.x0 = -2.0;
					_complexRect.y0 = 1.3;
					_complexRect.width = 2.6 * _plotRect.Width / _plotRect.Height;
					_complexRect.height = 2.6;
					//force complete recalculation
					forceTotalRecalc = true;
					ResetPanZoom();
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void RecolorNow()
			{
				//Recolor all pixels in bmpPlot using current Coloring
				try
				{
					_mustRecolor = false;
					_renderStatus = eRenderStatus.Render_InProgress;

					for (int col = 0; col < plot.Length; col++)
					{
						//for each column in plot
						SetColPixelsInBmp(col, plot[col]);
					}

					//let GUI know plot has changed
					_bmpChanged = true;
					OnRenderFinishedEvent(new EventArgs());
					//SetRenderStatus(eRenderStatus.Render_Finished);

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void ResizePlotNow(int width, int height, bool bSave)
			{
				//Resize the plot array, (opt.)saving any data possible
				try
				{
					int[][] newplot;			//temp new Plot - will copy back to m_plot
					int maxRow, maxCol;		//endpoints of data to copy from old plot
					string sLog;

					//reset resize flag
					_mustResize = false;

					if (_isAa)
					{
						width = 2 * width;
						height = 2 * height;
					}

					//bounds checking on size
					if (width < 4) width = 4;
					if (height < 4) height = 4;
					
					sLog =string.Format("Setting Plot to ({0}, {1})", width, height);
					Log.Info(sLog);
					Console.WriteLine(sLog);

					//get endpoints of data we will save
					if (height > _plotRect.Height)
					{
						maxRow = _plotRect.Height;
					}
					else
					{
						maxRow = height;
					}

					if (width > _plotRect.Width)
					{	
						maxCol = _plotRect.Width; 
					}
					else
					{ 
						maxCol = width; 
					}

					//initialize newplot & m_nextplot
					newplot = new int[width][];
					nextPlot = new int[width][];

					//create column arrays
					for (int x = 0; x < width; x++)
					{

						//initialize a newplot column
						newplot[x] = new int[height];
						nextPlot[x] = new int[height];

						//optionally copy data
						if (bSave && (x < maxCol))
						{
							for (int y = 0; y < maxRow; y++)
							{
								newplot[x][y] = plot[x][y];
							}
						}

					}

					//copy back
					plot = newplot;

					//resize PlotRect
					_plotRect.Width = width;
					_plotRect.Height = height;

					//normalize
					NormalizeGraph();

					//recreate bitmaps
					if (_bmpPlot != null)
					{
						lock (bitmapLock)
						{
							//copy original reference
							Bitmap origBmp = _bmpPlot;
							//make new bitmap with new size
							_bmpPlot = new Bitmap(width, height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
							//draw original back on new at top left
							Graphics gr = Graphics.FromImage(_bmpPlot);
							gr.DrawImage(origBmp, 0, 0);
							_bmpChanged = true;
						}
					}

					//reset flag
					_mustResize = false;
					forceTotalRecalc = true;
					Console.WriteLine("Finished ResizePlotNow (" + width + "," + height + ")");
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void NormalizeGraph()
			{
				//normalize aspect ratio after resizing and prevent zooming in too far
				try
				{
					Console.WriteLine("NormalizeGraph");
					//Minimum Size of pixels = minimum  floating point difference
					switch (DataType)
					{
						case eDataType.Double:
							if (_complexRect.height < 5.0d * System.Double.Epsilon)
							{
								_complexRect.height = 5.0d * System.Double.Epsilon;
							}
							break;

						case eDataType.Decimal:
							Decimal minIncrement = 0.000000000000000000000000001m;
							if (_complexRect.heightD < 5m * minIncrement)
							{
								_complexRect.heightD = 5m * minIncrement;
							}
							break;
					}

					//aspect ratio
					if (_complexRect.widthD < _complexRect.heightD * PlotRect.Width / PlotRect.Height)
					{ _complexRect.widthD = _complexRect.heightD * PlotRect.Width / PlotRect.Height; }
					if (_complexRect.heightD < _complexRect.widthD * PlotRect.Height / PlotRect.Width)
					{ _complexRect.heightD = _complexRect.widthD * PlotRect.Height / PlotRect.Width; }
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}
			
			private void DetermineShortcutType()
			{
				//determine possible shortcuttype
				shortcut = eShortCut.RecalcAll;		//default - recalculate every point
				if (forceTotalRecalc)
					return;

				//if (zoom == 1.0)
				//{	//no zoom - just pan
				//    shortcut = eShortCut.Pan;
				//}

				//if (zoom < 1.0)
				//{
				//    shortcut = eShortCut.ZoomOut;
				//}
			}

			private void SetNextComplexCoordinates()
			{	//Set the complexRect values for the next Mandelbrot calculation
				double aa = 1d;
				decimal aaD = 1m;
				Rectangle oldRect;

				try
				{
					//redraw bmpPlot for next image
					Bitmap bmpNew = new Bitmap(PlotRect.Width, PlotRect.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
					Graphics gr = Graphics.FromImage(bmpNew);
					gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
					//set oldRect to copy image to
					if (_isAa)
					{
						aa = 2d;
						aaD = 2m;
						oldRect = new Rectangle(
							(int)(panRect.x0 * 2), (int)(panRect.y0 * 2), 
							(int)(panRect.width * 2), (int)(panRect.height * 2));
					}
					else
					{
						oldRect = new Rectangle(
							(int)panRect.x0, (int)panRect.y0, (int)panRect.width, (int)panRect.height);
					}
					
					//replace bitMap
					lock (bitmapLock)
					{
						gr.DrawImage(_bmpPlot, oldRect);
						_bmpPlot = bmpNew;
						_bmpChanged = true;
					}

					//recalc Complex coordinates
					switch (DataType)
					{
						// dx = -panleft.x * aa * complexRect.width / panRect.width
						// dy = +panleft.y * aa * complexRect.height / panRect.height

						case eDataType.Double:
							_complexRect.x0 = _complexRect.x0 
								- panRect.x0 * aa * _complexRect.width / ((double)PlotRect.Width * zoom);
							_complexRect.y0 = _complexRect.y0 
								+ panRect.y0 * aa * _complexRect.height / ((double)PlotRect.Height * zoom);
							_complexRect.width = _complexRect.width / zoom;
							_complexRect.height = _complexRect.height / zoom;
							break;

						case eDataType.Decimal:
							_complexRect.x0D = _complexRect.x0D
								- panRect.x0D * aaD * _complexRect.widthD / (decimal)PlotRect.Width;
							_complexRect.y0D = _complexRect.y0D
								+ panRect.y0D * aaD * _complexRect.heightD / (decimal)PlotRect.Height;
							_complexRect.widthD = _complexRect.widthD / (decimal)zoom;
							_complexRect.heightD = _complexRect.heightD / (decimal)zoom;
							break;

						case eDataType.BigInteger:
							//not implemented yet
							break;
					}
					NormalizeGraph();
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void CalcNextPlot(bool bContinue)
			{
				//calculates next Mandelbrot Plot - using nextPlotTrans
				try
				{
					//Set Status and timing
					SetRenderStatus(eRenderStatus.Render_InProgress);
					_renderTimeStart = DateTime.Now;
					if (!bContinue)
					{
						RenderTimePartial = System.TimeSpan.Zero;	//Reset partials
					}

					//change settings if neccessary
					if (_mustResize)
					{
						ResizePlotNow(_newPlotRect.Width, _newPlotRect.Height, true);
					}
					if (_mustReset)
					{
						ResetCoordinatesNow();
					}
					if (_mustRecolor)
					{
						RecolorNow();
					}

					if (!bContinue)
					{
						//Calculate new Complex coordinates
						DetermineShortcutType();
						SetNextComplexCoordinates();
					}

					//Log Calculation
					string sMsg = string.Format("CalcNextPlot ({0},{1})={2}",
					PlotRect.Width, PlotRect.Height, _complexRect.ToString());
					Log.Info(sMsg);
					Console.WriteLine(sMsg);

					//Asynchronously call outer loop - calculating Columns
					//create delegate to method
					boolDelegate CalcColumnDelegate = new boolDelegate(CalcMandelbrotColumns);
					//invoke method asynchronously
					IAsyncResult result = CalcColumnDelegate.BeginInvoke(bContinue, new AsyncCallback(CalcColumnsEndInvoke), null);

				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void CalcColumnsEndInvoke(IAsyncResult ar)
			{

				AsyncResult result = (AsyncResult)ar;
				boolDelegate del = (boolDelegate)result.AsyncDelegate;
				del.EndInvoke(ar);
			}

			private void ResetPanZoom()
			{
				//resets PanZoom to unzoomed
				try
				{
					zoom = 1.0;
					if (_isAa)
					{
						m_panRect = new MultiRect(0, 0, _plotRect.Width / 2.0, _plotRect.Width / 2.0);
					}
					else
					{
						m_panRect = new MultiRect(0, 0, (double)_plotRect.Width, (double)_plotRect.Height);
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void GetIterationSteps()
			{
				//calculates iteration steps for Mandelbrot calculation loop
				const decimal MININCR = 0.000000000000000000000000001M;

				try
				{
					iterStep = new MultiPt();
					iterStep.x = (_complexRect.width / (double)PlotRect.Width);
					iterStep.xD = (_complexRect.widthD / (decimal)PlotRect.Width);
					if (iterStep.xD == 0M)
						iterStep.xD = MININCR;
					iterStep.y = (_complexRect.height / (double)PlotRect.Height);
					iterStep.yD = (_complexRect.heightD / (decimal)PlotRect.Height);
					if (iterStep.yD == 0M)
						iterStep.yD = MININCR;

				}
				catch (ArithmeticException ex) { cErrlog.Log(ex); }
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private void CalcMandelbrotColumns(bool bContinue)
			{
				//calculates each column of a Mandelbrot
				int startCol = 0;

				try
				{
					GetIterationSteps();

					if (bContinue)
					{
						startCol = continueCol;
					}

					Console.WriteLine(string.Format("Starting CalcMandelColumns from {0} to {1}", startCol, PlotRect.Width - 1));
					ParallelLoopResult result =
						Parallel.For(startCol, PlotRect.Width, (col, ColState) =>

					//for (col = startCol; col < PlotRect.Width; col++)			//for each column in plot
					{
						//Calc for each point in column
						MultiFloat colVal = new MultiFloat(_complexRect.x0D + iterStep.xD * col);
						nextPlot[col] = CalcSingleColumn(col, colVal);

						//set pixels for this col in bmpPlot
						SetColPixelsInBmp(col, nextPlot[col]);

						//test if we want to pause
						if (RenderStatus == eRenderStatus.Render_Paused)
						{
							continueCol = col + 1;
							ColState.Break();
						}

						//test if we want to quit early
						if (RenderStatus == eRenderStatus.Render_Finished)
						{
							ColState.Break();
						}

					}	//close lambda expression
					);	//close parallel For

					//now we are finished 
					int lastCol;

					//Set RenderTime
					_lastRenderTime = (DateTime.Now - _renderTimeStart) + RenderTimePartial;

					//did we finish?
					if (result.IsCompleted)
					{	//we finished
						lastCol = PlotRect.Width;
						//clear partials
						RenderTimePartial = System.TimeSpan.Zero;
						continueCol = 0;
						ResetPanZoom();
						forceTotalRecalc = false;
						OnRenderFinishedEvent(new EventArgs());
						//SetRenderStatus(eRenderStatus.Render_Finished);
						Log.Info("CalcNextPlot Finished");

						//copy from nextPlot to plot
						for (int col = startCol; col < lastCol; col++)
						{
							plot[col] = nextPlot[col];
						}
						Log.Info("Copied plot to nextPlot");
					}
					else
					{
						//we paused
						if (result.LowestBreakIteration != null)
						{
							lastCol = (int)result.LowestBreakIteration;
						}
						else
						{
							//we stopped - indeterminate # cols finished
							lastCol = 1;
						}

						RenderTimePartial = _lastRenderTime;
						Log.Info("CalcPlot Paused");
					}

					
				}
				catch (Exception ex) { cErrlog.Log(ex);}
			}

			private int[] CalcSingleColumn(int curCol, MultiFloat rVal)
			{
				//calculate all points (i axis) for a single column in Mandelbrot plot - returning that column
				eShortCut colShortcut;
				int[] colOut = new int[PlotRect.Height];	//output array

				//try no error trapping to increase speed - must trap inside thread however
				//every outside variable used here needs a lock
				//		ReadOnly? - curCol, complexRect, iterStep, m_PanRect, PlotRect, rVal
				//					 - m_numIterations, plot[], m_dataType
				//		ReadWrite - no outside variables
		

				try
				{
					//check shortcuts vs column < panRect.x									left of last plot
					//						  column >= (panRect.x + panRect.width)			right of last plot
					if ((curCol >= m_panRect.x0) &&
							(curCol < m_panRect.x0 + m_panRect.width))
					{
						colShortcut = shortcut;
					}
					else
						colShortcut = eShortCut.RecalcAll;	//outside of previously calc values
					
					for (int row = 0; row < colOut.Length; row++)
					{
						eShortCut ptShortcut;
						int origRow, origCol;

						//imaginary value of this point
						MultiFloat iVal = new MultiFloat(_complexRect.y0D - iterStep.yD * row);

						//check shortcuts vs. row   panRect.y < row < panRect.y + panRect.height
						if ((row >= m_panRect.y0) && (row < m_panRect.y0 + m_panRect.height))
						{
							//inside previously calculated values
							ptShortcut = colShortcut;
						}
						else
						{	//must recalc
							ptShortcut = eShortCut.RecalcAll;
						}

						switch (ptShortcut)
						{
							case eShortCut.Pan:
								//This doesn't work 
								origCol = curCol - (int)m_panRect.x0;
								origRow = row - (int)m_panRect.y0;
								if ((origCol < 0) || (origCol >= PlotRect.Width) ||
									(origRow < 0) || (origRow >= PlotRect.Height))
								{
									Console.WriteLine("invalid Pan copy ({0},{1}) to ({2},{3})", 
										origCol, origRow, curCol, row);
									//just try double
									colOut[row] = MandNumber(rVal.valD, iVal.valD, _numIterations);
								}
								else
									colOut[row] = plot[origCol][origRow];
								break;

							case eShortCut.ZoomOut:
								//this doesn't work - very messy result
								//TODO: fix it
								origCol = (int)((curCol - m_panRect.x0) / zoom);
								origRow = (int)((row - m_panRect.y0) / zoom);
								if ((origCol < 0) || (origCol >= PlotRect.Width) ||
									(origRow < 0) || (origRow >= PlotRect.Height))
								{
									Console.WriteLine("invalid ZoomOut copy ({0},{1}) to ({2},{3})", 
										origCol, origRow, curCol, row);
									colOut[row] = MandNumber(rVal.valD, iVal.valD, _numIterations);
								}
								else
									colOut[row] = plot[origCol][origRow];
								break;

							default:
								//calculate
								switch (_dataType)
								{
									case eDataType.Double:
										colOut[row] = MandNumber(rVal.val, iVal.val, _numIterations);
										break;

									case eDataType.Decimal:
										colOut[row] = MandNumber(rVal.valD, iVal.valD, _numIterations);
										break;
								}
								break;
						}
					}//next row
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return colOut;
			}

			private void SetColPixelsInBmp(int col, int[] MandVal)
			{
				//Set pixels in this col of bmpPlot using color mapping
				DateTime startTime;
				TimeSpan elapsed;

				try
				{
					startTime = DateTime.Now;

					//lock bmpPlot
					lock (bitmapLock)
					{
						elapsed = DateTime.Now - startTime;
						if (elapsed.TotalMilliseconds > 70)
						{
							string sTime = string.Format("SetColPixels bitmapLock took {0:#.000} sec", elapsed.TotalSeconds);
						}

						for (int row = 0; row < _plotRect.Height; row++)
						{
							_bmpPlot.SetPixel(col, row, _colorLookup.ColorLookup(MandVal[row], _numIterations));
						}
						_bmpChanged = true;
					}
				}
				catch (Exception ex) { cErrlog.Log(ex); }
			}

			private int MandNumber(double r0, double i0, int RenderDepth)
			{	//tests Complex number C to determine if it is in Mandelbrot set
				//limit n-> inf  Cn = Cn-1 ^ 2 + C0
				// if |Cn| >= 2, return n
				//otherwise return RenderDepth

				int cnt = 1;
				double rnew, inew, rsq, isq;

				try
				{
					rnew = r0;
					inew = i0;
					rsq = r0 * r0;
					isq = i0 * i0;

					while ((cnt < RenderDepth) &&
						((rsq + isq) < 4d))
					{
						//Cn+1 = Cn^2 + Cn
						//		= [x(n) + i y(n)]^2 + x(0) + iy(0)
						//		= x(n)^2 - y(n)^2 + 2i * x(n) * y(0) + x(n) + iy(0)
						//		= x(n)^2 - y(n)^2 + x(0)
						//										 + i [2x(n)y(n) + y(0)]
						//rlast = rnew;
						inew = 2 * rnew * inew + i0;
						rnew = rsq - isq + r0;
						rsq = rnew * rnew;
						isq = inew * inew;

						cnt++;
					}

					//smooth coloring - return double d = n - log2(log(abs(Cn)) / log(RenderDepth))
				}
				catch (Exception ex) { cErrlog.Log(ex); }
				return cnt;
			}

			private int MandNumber(decimal r0, decimal i0, int RenderDepth)
			{	//tests Complex number C to determine if it is in Mandelbrot set
				//limit n-> inf  Cn = Cn-1 ^ 2 + C0
				// if |Cn| >= 2, return n
				//otherwise return RenderDepth

				int cnt = 1;
				decimal rnew, inew, rlast;

				rnew = r0;
				inew = i0;

				while ((cnt < RenderDepth) &&
					((rnew * rnew + inew * inew) < 4.0m))
				{
					//Cn+1 = Cn^2 + Cn
					//		= [x(n) + i y(n)]^2 + x(0) + iy(0)
					//		= x(n)^2 - y(n)^2 + 2i * x(n) * y(0) + x(n) + iy(0)
					//		= x(n)^2 - y(n)^2 + x(0)
					//										 + i [2x(n)y(n) + y(0)]
					rlast = rnew;
					rnew = rlast * rlast - inew * inew + r0;
					inew = 2 * rlast * inew + i0;
					cnt++;
				}

				//smooth coloring - return double d = n - log2(log(abs(Cn)) / log(RenderDepth))
				return cnt;
			}

		#endregion //Private Methods

		#region dll calls

			[System.Runtime.InteropServices.DllImport("MandelFunc.dll")]
			private static extern int fnMandelPoint(double r0, double i0, int maxiter);


		#endregion //dll calls
	}
}
