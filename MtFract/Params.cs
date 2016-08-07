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

namespace MtFract
{
	
	public class Params
	{
		#region Fields

			//Current Plot
			private System.Drawing.Size _windowSize;
			private int _pixels;
			private int _maxiter;
			private System.TimeSpan _rendertime;

			//next Plot
			private MultiRect _panRect;
			private double _zoomVal;
			private int _nextMaxIter;

			//selected Pixel
			private string _mousePt;
			private MultiPt _complexPt;
			private int _numIter;

		#endregion //Fields


		#region Properties

			//Current Plot
			public System.Drawing.Size WindowSize
			{
				get { return _windowSize; }
				set { _windowSize = value; }
			}

			public int Pixels
			{
				get { return _pixels; }
				set { _pixels = value; }
			}

			public int MaxIter
			{
				get { return _maxiter; }
				set {  _maxiter = value; }
			}

			public System.TimeSpan RenderTime
			{
				get { return _rendertime; }
				set { _rendertime = value; }
			}

			//next Plot
			public MultiRect PanRect
			{
				get { return _panRect; }
				set {   _panRect = value; }
			}

			public double ZoomVal
			{
				get { return _zoomVal; }
				set { _zoomVal = value; }
			}

			public int NextMaxIter
			{
				get { return _nextMaxIter; }
				set {  _nextMaxIter = value;  }
			}

			//Selected Pixel 
			public string MousePt
			{
				get { return _mousePt; }
				set { _mousePt = value; }
			}

			public MultiPt ComplexPt
			{
				get { return _complexPt; }
				set { _complexPt = value; }
			}

			public int NumIter
			{
				get { return _numIter; }
				set {  _numIter = value; }
			}

		#endregion

		#region Constructors

			public Params()
		{
			_windowSize = new System.Drawing.Size(640, 480);
			_pixels = _windowSize.Width * _windowSize.Height;
			_maxiter = 256;
			_rendertime = new TimeSpan(0, 0, 0);

			_panRect = new MultiRect(0, 0, 640.0, 480.0);
			_zoomVal = 1.0;
			
			_mousePt = "";
			_complexPt = new MultiPt();
			_numIter = 0;
		}

		#endregion //Constructors

	}
}
