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
	public class MultiRect
	{
		//Rectangle - supports both fixed point decimal and double fp

		//private
		double _x0, _y0;
		decimal _x0D, _y0D;
		double _width, _height;
		decimal _widthD, _heightD;

		#region //Properties
		
		public double x0
		{
			get { return _x0; }
			set
			{
				_x0 = value;
				_x0D = (decimal)value;
			}
		}

		public double y0
		{
			get { return _y0; }
			set
			{
				_y0 = value;
				_y0D = (decimal)value;
			}
		}

		public decimal x0D
		{
			get { return _x0D; }
			set
			{
				_x0D = value;
				_x0 = (double)value;
			}
		}

		public decimal y0D
		{
			get { return _y0D; }
			set
			{
				_y0D = value;
				_y0 = (double)value;
			}
		}

		public double height
		{
			get { return _height; }
			set { 
				_height = value;
				_heightD = (decimal)value;
			}
		}

		public double width
		{
			get { return _width; }
			set { 
				_width = value;
				_widthD = (decimal)value;
			}
		}

		public decimal heightD
		{
			get { return _heightD; }
			set
			{
				_heightD = value;
				_height = (double)_heightD;
			}
		}

		public decimal widthD
		{
			get { return _widthD; }
			set
			{
				_widthD = value;
				_width = (double)_widthD;
			}
		}

		//calculated readonly
		public DoublePt topLeft
		{
			get { return new DoublePt(_x0, _y0); }
		}

		public DoublePt bottomRight
		{
			get
			{
				return new DoublePt(_x0 + _width, _y0 - _height);
			}
		}

		public DecPt topLeftD
		{
			get { return new DecPt(_x0D, _y0D); }
		}

		public DecPt bottomRightD
		{
			get {
				return new DecPt(_x0D + _widthD, _y0D - _heightD);
			}
		}

		#endregion //Properties

		//constructors
		public MultiRect()
		{
			x0 = 0d;
			y0 = 0d;
			width = 0d;
			height = 0d;
		}

		public MultiRect(double x, double y, double width, double height)
		{
			this.x0 = x;
			this.y0 = y;
			this.width = width;
			this.height = height;
		}

        public MultiRect(Decimal x, Decimal y, Decimal width, Decimal height)
        {
            this.x0D = x;
            this.y0D = y;
            this.widthD = width;
            this.heightD = height;
        }


		#region Methods

		public bool InRect(DoublePt testPt)
		{
			bool bOut = false;
			
			//test point >= topleft, <= bottomright
			if ( (testPt.x >= _x0) &&
					(testPt.x <= bottomRight.x) &&
					(testPt.y <= y0) &&
					(testPt.y >= bottomRight.y) )
			{ bOut = true;}
			return bOut;
		}

		public void Copy(MultiRect newRect)
		{
			//deep copy of newRect into this
			try
			{
				this.x0D = newRect.x0D;
				this.y0D = newRect.y0D;
				this.widthD = newRect.widthD;
				this.heightD = newRect.heightD;
			}
			catch (Exception ex) { cErrlog.Log(ex); }
		}

		/// <summary>
		/// Add Point to Rectangle
		/// </summary>
		/// <param name="pt"></param>
		public void AddPoint(System.Drawing.Point pt)
		{
			_x0 = _x0 + pt.X;
			_x0D = _x0D + pt.X;
			_y0 = _y0 + pt.Y;
			_y0D = _y0D + pt.Y;
		}

		public override string ToString()
		{
			string sOut = string.Format("({0},{1}) - ({2},{3})",
                x0D, y0D, bottomRightD.x, bottomRightD.y);
			return sOut;
		}

		#endregion //Methods
	}
}
