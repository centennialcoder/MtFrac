using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MtFract
{
	public class DecPt
	{
		//2d point using decimals

		//private
		private decimal m_x;
		private decimal m_y;

		//properties
		public decimal x
		{
			get { return m_x; }
			set { m_x = value; }
		}

		public decimal y
		{
			get { return m_y; }
			set { m_y = value; }
		}

		//constructors
		public DecPt(decimal x, decimal y)
		{
			m_x = x;
			m_y = y;
		}

		public DecPt(double x, double y)
		{
			m_x = (decimal)x;
			m_y = (decimal)y;
		}

		public DecPt()
			: this(0m, 0m)
		{
			//zero for both params
		}


		//methods
		public override string ToString()
		{
			string sOut = "";
			sOut = string.Format("({0:+0.0000000000000000000000000;-0.0000000000000000000000000},{1:+0.0000000000000000000000000;-0.0000000000000000000000000})", x, y);
			return sOut;
		}

	}
}
