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


namespace MtFract
{
	public class MultiPt
	{
		//a point with representation in several underlying data types
				//2d point using decimals, double

		//private
		private double _x;
		private double _y;
		private decimal _xD;
		private decimal _yD;

		//properties
		public double x
		{
			get { return _x; }
			set { 
				_x = value; 
				_xD = (decimal)_x;
			}
		}
		public decimal xD
		{
			get { return _xD; }
			set { 
				_xD = value; 
				_x = (double)_xD;
			}
		}

		public double y
		{
			get { return _y; }
			set { 
				_y = value;
 				_yD = (decimal)_y;
			}
		}
		public decimal yD
		{
			get { return _yD; }
			set { 
				_yD = value; 
				_y = (double)_yD;
			}
		}

		//constructors
		public MultiPt(decimal x, decimal y)
		{
			xD = x;
			yD = y;
		}

		public MultiPt(double x, double y)
		{
			this.x = x;
			this.y = y;
		}

		public MultiPt()
			: this(0m, 0m)
		{
			//zero for both params
		}


		//methods
		public override string ToString()
		{
			string sOut = "";
			sOut = string.Format("({0:#,0.0000000000000000000000000;-#,0.0000000000000000000000000},{1:#,0.0000000000000000000000000;-#,0.0000000000000000000000000})", x, y);
			return sOut;
		}

	}
}
